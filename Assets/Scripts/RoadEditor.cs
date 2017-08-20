#if UNITY_EDITOR

using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(RoadPoint))]
public class RoadEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RoadPoint myScript = (RoadPoint)target;
        if (GUILayout.Button("Create"))
        {
            myScript.Create();
        }
        if(GUILayout.Button("Delete"))
        {
            myScript.Delete();
        }
    }
}
#endif