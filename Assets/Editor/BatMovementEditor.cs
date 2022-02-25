using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(BatMovement)), CanEditMultipleObjects]
public class BatMovementEditor : Editor
{
    private void OnSceneGUI()
    {
        BatMovement batMovement = (BatMovement)target;
        Handles.color = Color.red;
        EditorGUI.BeginChangeCheck();
        Vector3 newPointA = Handles.FreeMoveHandle(batMovement.PointA, Quaternion.identity,.3f,new Vector3(.25f,.25f,.25f),Handles.CircleHandleCap);
        Vector3 newPointB = Handles.FreeMoveHandle(batMovement.PointB, Quaternion.identity,.3f,new Vector3(.25f,.25f,.25f),Handles.CircleHandleCap);

        // records changes
        if ( EditorGUI.EndChangeCheck() )
        {
            Undo.RecordObject(this, "Free Move Handle");
            batMovement.PointA = newPointA;
            batMovement.PointB = newPointB;
        }
    }
}
