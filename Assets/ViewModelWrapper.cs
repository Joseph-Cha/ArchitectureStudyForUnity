using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class ViewModelWrapper : MonoBehaviour
{
    public INotifyPropertyChanged SettingsViewModel { get; set; }
    private void Awake() => SettingsViewModel = new ViewModel();
}
