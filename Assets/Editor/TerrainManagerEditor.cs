//using UnityEngine;
//using System.Collections;
//using UnityEditor;

//[CustomEditor(typeof(TerrianManager))]
//public class TerrianManagerEditor : Editor
//{


//    bool shown = false;

//    public override void OnInspectorGUI()
//    {
//        TerrianManager manager = (TerrianManager)target;
//        shown = EditorGUILayout.Foldout(shown, "Data of Terrain");

//        if (shown)
//        {
//            manager.Distance = EditorGUILayout.IntField("Distance", manager.Distance);
//            manager.Resoultion = EditorGUILayout.FloatField("Resoultion", manager.Resoultion);
//            manager.Angle = EditorGUILayout.FloatField("Angle", manager.Angle);
//            manager.SizeOfPiece = EditorGUILayout.Vector3Field("SizeOfPiece", manager.SizeOfPiece);
//        }

//        shown = EditorGUILayout.Foldout(shown, "Materials of Terrain");

//        if (shown)
//        {
//            EditorGUILayout.BeginHorizontal();
//            manager.TopMaterial = (Material)EditorGUILayout.ObjectField("", manager.TopMaterial, typeof(Material), true);

//            EditorGUILayout.EndHorizontal();
//        }

//        if (GUI.changed)
//        {
//            EditorUtility.SetDirty(manager);
//            manager.Start();
//        }
//    }
//}