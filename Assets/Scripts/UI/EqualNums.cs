using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EqualNums : MonoBehaviour
{

    
    public TextMeshProUGUI txtToCopy;
   
    TextMeshProUGUI txt;
    void Start()
    {
        txt = GetComponent<TextMeshProUGUI>();      
    }

    void Update()
    {
        txt.text = txtToCopy.text;
    }
}
