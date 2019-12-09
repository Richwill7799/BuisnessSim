using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorMenuScript : ScriptableObject
{
    [MenuItem("Examples/EditorPrefs/Clear all Editor Preferences")]
    static void deleteAllExample() {
        if (EditorUtility.DisplayDialog("Delete all editor preferences.",
            "Are you sure you want to delete all the editor preferences? " +
            "This action cannot be undone.", "Yes", "No")) {
            Debug.Log("yes");
            EditorPrefs.DeleteAll();
        }
    }
}
