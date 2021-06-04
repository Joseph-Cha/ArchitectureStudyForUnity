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
    public Slider slider;
    public ViewModelWrapper ViewModelWrapper;
    private Dictionary<string, PropertyInfo> propertyInfoDictionary = new Dictionary<string, PropertyInfo>();
    private INotifyPropertyChanged viewModel;

    private void Start()
    {
        viewModel = ViewModelWrapper.SettingsViewModel;
        
        #region set property
        PropertyInfo pi = viewModel.GetType().GetProperty("IsNormalType");

        if (!propertyInfoDictionary.ContainsKey(pi.Name))
        {
            propertyInfoDictionary.Add(pi.Name, pi);
        }
        #endregion

        viewModel.PropertyChanged += PropertyChanged;
        
        toggle.onValueChanged.AddListener(LightToggleValueChanged);
    }
    
    private void PropertyChanged(object value, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "IsNormalType")
        {
            bool isNormalType = (bool)propertyInfoDictionary[e.PropertyName].GetValue(value);
            if (toggle.isOn != isNormalType)
            {
                toggle.isOn = isNormalType;
            }
        }
    }

    private void LightToggleValueChanged(bool isOn) => propertyInfoDictionary["IsNormalType"]?.SetValue(viewModel, isOn);
}
