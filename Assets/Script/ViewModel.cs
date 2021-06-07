using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

enum PropertyName
{
    IsNormalType,
    IsAutoIntensity,
    LightIntensity,
    HasShadow,
}

public class ViewModel : INotifyPropertyChanged
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
    private bool isAutoIntensity = false;
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
    
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")  
    {  
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void SaveData()
    {   
        string filePath = Application.persistentDataPath + "/data.dat";

        using(FileStream stream = new FileStream(filePath, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Model model = new Model(this.IsNormalType, this.IsAutoIntensity, this.LightIntensity, this.HasShadow);
            formatter.Serialize(stream, model);
        };
    }


    public void LoadData()
    {
        Model model = null;
        string filePath = Application.persistentDataPath + "/data.dat";

        if(File.Exists(filePath))
        {
            using(FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                model = formatter.Deserialize(stream) as Model;
                formatter = null;
            };
        }
        else
            model = new Model(true, false, 0, false);
            
        InitFields(model);
    }

    private void InitFields(Model model)
    {
        this.IsNormalType = model.IsNormalType;
        this.IsAutoIntensity = model.IsAutoIntensity;
        this.LightIntensity = model.LightIntensity;
        this.HasShadow = model.HasShadow;
    }
}
 