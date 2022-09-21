using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Preference<T>
{
    public string Name;
    public T Value;
}

[Serializable]
public class FloatPreference : Preference<float>
{
    public Slider source;
    new public float Value
    {
        get
        {
            if (source != null)
                return source.value;
            else
                return 0;
        }

        set
        {
            if (source != null)
                source.value = value;
        }
    }
}