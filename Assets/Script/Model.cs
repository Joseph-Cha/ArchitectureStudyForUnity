[System.Serializable]
public class Model
{
    public Model(bool isNormalType, bool isAutoIntensity, float lightIntensity, bool hasShadow)
    {
        this.IsNormalType = isNormalType;
        this.IsAutoIntensity = isAutoIntensity;
        this.LightIntensity = lightIntensity;
        this.HasShadow = hasShadow;
    }
    public bool IsNormalType;
    public bool IsAutoIntensity;
    public float LightIntensity;
    public bool HasShadow;
}
