using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Reflection;

[RequireComponent(typeof(ViewModelWrapper))]
public class View : MonoBehaviour
{
    [Header("UI Area")]
    public Toggle toggle;
    public Slider slider;

    // ViewModel 객체를 가지고 오기 위해서 사용된 Wrapper
    public ViewModelWrapper ViewModelWrapper { get; set; }

    // ViewModel은 실질적으로 INotifyPropertyChanged의 데이터만 가지고 있음
    private INotifyPropertyChanged viewModel;

    // View Model의 Property들의 정보를 담고 있음
    private Dictionary<string, PropertyInfo> propertyInfos = new Dictionary<string, PropertyInfo>();

    private void Start()
    {
        ViewModelWrapper = GetComponent<ViewModelWrapper>();
        InitUIEvent();
        SetPropertyInfo();
        Binding();
    }

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

    private void LightToggleValueChanged(bool isOn) => propertyInfos["IsNormalType"]?.SetValue(viewModel, isOn);

    private void InitUIEvent()
    {
        toggle.onValueChanged.AddListener(LightToggleValueChanged);
    }
    
    private void SetPropertyInfo()
    {
        // viewModel가 INotifyPropertyChanged 타입이더라도 
        // 리플렉션을 통해 SettingsViewModel의 속성 값들을 가지고 올 수가 있다.
        // MVVM을 통해 View와 ViewModel의 의존성을 없애기 위해서
        // 인터페이스(INotifyPropertyChanged)와 리플렉션을 사용해야하는 이유이다.
        viewModel = ViewModelWrapper.SettingsViewModel;
        PropertyInfo[] infos = viewModel.GetType().GetProperties();

        // viewModel의 PropertyInfo 정보를 사전에 저장
        foreach (var info in infos)
            propertyInfos.Add(info.Name, info);
    }

    private void Binding() => viewModel.PropertyChanged += PropertyChanged;
}
