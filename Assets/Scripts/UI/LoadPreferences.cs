using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPreferences : MonoBehaviour
{
    public List<FloatPreference> floatPreferences = new List<FloatPreference>();
    [SerializeField] bool loadOnStart = true;

    private void Start()
    {
        if(loadOnStart)
            Load();
    }

    public void Load()
    {
        foreach (var preference in floatPreferences)
        {
            if (PlayerPrefs.HasKey(preference.Name))
            {
                preference.Value = PlayerPrefs.GetFloat(preference.Name);
            }
        }
    }
}
