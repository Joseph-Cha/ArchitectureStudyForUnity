using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class ViewModelWrapper : MonoBehaviour
{
    public INotifyPropertyChanged SettingsViewModel { get; set; }
    private void Awake() => SettingsViewModel = new SettingsViewModel();
    // toggle test
    [ContextMenu("ToggleValueChanged")]
    public void ValueChanged()
    {
        // 리플렉션은 타입이 아니라 객체 자체를 본다.
        SettingsViewModel.GetType().GetProperty("IsNormalType").SetValue(SettingsViewModel, false);
    }
}
