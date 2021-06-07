using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Reflection;

public class View : MonoBehaviour
{
    public Toggle toggle;

    // dataContext는 실질적으로 INotifyPropertyChanged의 데이터만 가지고 있음
    private INotifyPropertyChanged dataContext = new ViewModel();

    // View Model의 Property들의 정보를 담고 있음
    private Dictionary<string, PropertyInfo> propertyInfos = new Dictionary<string, PropertyInfo>();

    //View Model의 Method들의 정보를 담고 있음
    private Dictionary<string, MethodInfo> methodInfos = new Dictionary<string, MethodInfo>();

    private void Awake()
    {
        InitUIEvent();
        SetPropertyInfo();
        SetMethodInfo();
        BindingPropertyChanged();
    }

    private void OnEnable() => LoadData();
    private void OnDisable() => SaveData();
    private void LoadData() => methodInfos[nameof(ViewModel.LoadData)]?.Invoke(dataContext, null);
    private void SaveData() => methodInfos[nameof(ViewModel.SaveData)]?.Invoke(dataContext, null);

    private void InitUIEvent()
    {
        toggle.onValueChanged.AddListener(LightToggleValueChanged);
    }

    private void LightToggleValueChanged(bool isOn)
    {
        PropertyInfo info = propertyInfos[nameof(ViewModel.IsNormalType)];
        info.SetValue(dataContext, isOn);
    }


    // viewModel가 INotifyPropertyChanged 타입이더라도 
    // 리플렉션을 통해 SettingsViewModel의 속성 값들을 가지고 올 수가 있다.
    // MVVM을 통해 View와 ViewModel의 의존성을 없애기 위해서
    // 인터페이스(INotifyPropertyChanged)와 리플렉션을 사용해야하는 이유이다.
    private void SetPropertyInfo()
    {
        PropertyInfo[] infos = dataContext.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var info in infos)
            propertyInfos.Add(info.Name, info);
    }

    private void SetMethodInfo()
    {
        MethodInfo[] infos = dataContext.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);

        foreach (var info in infos)
            methodInfos.Add(info.Name, info);
    }

    private void BindingPropertyChanged() => dataContext.PropertyChanged += PropertyChanged;

    // PropertyChanged에 바인딩을 하기 위한 메서드
    private void PropertyChanged(object value, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == PropertyName.IsNormalType.ToString())
        {
            bool isNormalType = (bool)propertyInfos[e.PropertyName].GetValue(value);
            if (toggle.isOn != isNormalType)
                toggle.isOn = isNormalType;
        }
    }

}
