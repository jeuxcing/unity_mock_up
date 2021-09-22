using UnityEngine;
using System.Collections;
using System;
using UnityEditor;

[CustomEditor(typeof(SimpleButton))]
public class SimpleButtonEditor : Editor
{
    string[] options;
    private static int maxLine = 5;
    private static int maxColumn = 5;
    private static int maxNode = maxLine*maxColumn;
    public int index = 0;
    public bool isConnected = false;

    void OnEnable()
    {
        options = new string[maxNode];
        for(var i=0; i<maxNode; i++)
            options[i]="Ring_ " + i + "  ["+i%maxColumn+";"+Math.Floor(((float)i/maxLine))+"]";   
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        index = EditorGUILayout.Popup(index, options);
        SimpleButton myScript = (SimpleButton)target;

        if(GUILayout.Button("press"))
            myScript.Pouet();
        
    }
}
