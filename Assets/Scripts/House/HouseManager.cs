using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour
{


    public static HouseManager instance;

    public List<House> houses = new List<House>();

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null) instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
