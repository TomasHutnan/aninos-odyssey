using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Test : MonoBehaviour
{
    public bool flag;
    public int i = 1;
}

[CustomEditor(typeof(Test))]
public class MyScriptEditor : Editor
{
    override public void  OnInspectorGUI()
    {
        var myScript = target as Test;

        myScript.flag = GUILayout.Toggle(myScript.flag, "Flag");

        if (myScript.flag)
            myScript.i = EditorGUILayout.IntSlider("I field:", myScript.i, 1, 100);

    }
}