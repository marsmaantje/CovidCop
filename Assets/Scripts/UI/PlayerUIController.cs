using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{


    [SerializeField] SicknessMeter sicknessMeter;
    [SerializeField] PlayerHealth playerHealth;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sicknessMeter.sickness = playerHealth.playerHealth;    
    }
}
