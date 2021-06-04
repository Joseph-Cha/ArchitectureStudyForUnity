using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SettingsViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    private bool isNormalType = true;
    public bool IsNormalType
    {
        get => this.isNormalType;
        set
        {
            if (value != this.isNormalType)
            {
                this.isNormalType = value;
                NotifyPropertyChanged();
            }
        }   
    }
    private bool isAutoIntensity;
    public bool IsAutoIntensity
    {
        get => this.isAutoIntensity;
        set
        {
            if (value != isAutoIntensity)
            {
                this.isAutoIntensity = value;
                NotifyPropertyChanged();
            }
        }
    }
    private float lightIntensity;
    public float LightIntensity
    {
        get => this.lightIntensity;
        set
        {
            if (value != lightIntensity)
            {
                this.lightIntensity = value;
                NotifyPropertyChanged();
            }
        }
    }
    private bool hasShadow;
    public bool HasShadow
    {
        get => this.hasShadow;
        set
        {
            if (value != this.hasShadow)
            {
                this.hasShadow = value;
                NotifyPropertyChanged();
            }
        }
    }
    private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")  
    {  
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }  
}
 