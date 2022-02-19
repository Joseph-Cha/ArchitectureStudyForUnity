using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "HairColorModel", menuName = "Hair")]
public class HairColorModel : ScriptableObject
{
    public float RedValue;
    public float GreenValue;
    public float BlueValue;

    public UnityAction<float> OnRedValueChanged;
    public UnityAction<float> OnGreenValueChanged;
    public UnityAction<float> OnBlueValueChanged;
}