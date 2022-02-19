using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class Hair : MonoBehaviour
{
    [SerializeField]
    private Image Image;

    private HairColorModel hairColor { get; set; }

    private Color color = new Color(r: 0, g: 0, b: 0, a: 1);

    private void Awake()
    {
        Addressables.LoadAssetAsync<HairColorModel>("HairModel").Completed += ao =>
        {
            if (ao.IsDone)
            {
                hairColor = ao.Result;
                hairColor.OnRedValueChanged += ChangeRedColor;
                hairColor.OnGreenValueChanged += ChangeGreenColor;
                hairColor.OnBlueValueChanged += ChangeBlueColor;
            }
        };
    }

    private void ChangeRedColor(float value)
    {
        color.r = value;
        Image.color = color;
    }

    private void ChangeGreenColor(float value)
    {
        color.g = value;
        Image.color = color;
    }

    private void ChangeBlueColor(float value)
    {
        color.b = value;
        Image.color = color;
    }
}
