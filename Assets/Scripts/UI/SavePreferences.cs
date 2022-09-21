using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePreferences : MonoBehaviour
{
    public List<FloatPreference> floatPreferences = new List<FloatPreference>();

    public void Save()
    {
        foreach (var preference in floatPreferences)
        {
            PlayerPrefs.SetFloat(preference.Name, preference.Value);
            Debug.Log(preference.Name + " = " + preference.Value);
        }
    }
}