using UnityEngine;
using UnityEngine.UI;

public class HairColorPresenter : MonoBehaviour
{
    // View
    [SerializeField]
    private Slider RedColor_Slider;
    [SerializeField]
    private Slider GreenColor_Slider;
    [SerializeField]
    private Slider BlueColor_Slider;

    // Model
    [SerializeField]
    private HairColorModel HairColorModel;

    private void Awake()
    {
        RedColor_Slider.onValueChanged.AddListener(v =>
        {
            HairColorModel.RedValue = v;
            HairColorModel.OnRedValueChanged?.Invoke(v);
        });

        GreenColor_Slider.onValueChanged.AddListener(v =>
        {
            HairColorModel.GreenValue = v;
            HairColorModel.OnGreenValueChanged?.Invoke(v);
        });

        BlueColor_Slider.onValueChanged.AddListener(v =>
        {
            HairColorModel.BlueValue = v;
            HairColorModel.OnBlueValueChanged?.Invoke(v);
        });
    }
}
