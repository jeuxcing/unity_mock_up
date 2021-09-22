using UnityEngine;
using System.Collections;
using System;
using UnityEditor;

[CustomEditor(typeof(Cable))]
public class CableEditor : Editor
{    
    string[] options;

    
    private static int maxLine = 5;
    private static int maxColumn = 5;
    private static int maxNode = maxLine*maxColumn;

    public int index=0;

    public bool isConnected = false;

    void OnEnable()
    {

        options = new string[maxNode];
        for(var i=0; i<maxNode; i++)
        {
            options[i]="Ring_ " + i + "  ["+i%maxColumn+";"+Math.Floor(((float)i/maxLine))+"]" ;
        }
        
    }

    public override void OnInspectorGUI ()
    {
        // Draw the default inspector
        DrawDefaultInspector();
        index = EditorGUILayout.Popup(index, options);
 
        EditorUtility.SetDirty(target);
    }

}
