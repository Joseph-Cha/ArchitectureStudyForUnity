[System.Serializable]
public class Model
{
    public Model(bool hasToggleValue, float sliderIntensity)
    {
        this.HasToggleValue = hasToggleValue;
        this.SliderIntensity = sliderIntensity;
    }
    public bool HasToggleValue;
    public float SliderIntensity;
}
