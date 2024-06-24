using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Scripting.Python;
using System;

[CustomEditor(typeof(PythonManager))]
public class PythonManagerEditor : Editor
{
    PythonManager targetManager;

    private void OnEnable()
    {
        //we will cast the target as a python varaiable
        targetManager = (PythonManager)target;
    }

    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Launch Python Script", GUILayout.Height(35)))
        {
            String path = Application.dataPath + "/Python/log_name.py";
            PythonRunner.RunFile(path);
        }
    }
}
