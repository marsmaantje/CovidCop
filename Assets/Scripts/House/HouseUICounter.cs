using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HouseUICounter : MonoBehaviour
{
    [SerializeField]House house;
    [SerializeField]TextMeshProUGUI text;
    

    // Start is called before the first frame update
    void Start()
    {
        if(house != null && text != null)
        {
            house.OnNPCAdded += onNPCAdded;
            house.OnNPCRemoved += onNPCRemoved;
        }

        UpdateVisuals();
    }

    void onNPCAdded(NPCBehavior npc)
    {
        UpdateVisuals();
    }

    void onNPCRemoved(NPCBehavior npc)
    {
        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        text.SetText(string.Format("{0}/{1}", house.housedNPCCount, house.HouseCapacity));
    }
}
