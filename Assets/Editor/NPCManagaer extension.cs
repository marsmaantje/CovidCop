using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NPCManager))]
public class NPCManagaerextension : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        NPCManager myScript = (NPCManager)target;
        if (GUILayout.Button("Setup Gathering Points from Children"))
        {
            //loop over all children of the gameobject
            foreach (Transform child in myScript.transform)
            {
                //add the child to the list of gathering points
                myScript.GatheringPoints.Add(child);
            }
        }
    }
}