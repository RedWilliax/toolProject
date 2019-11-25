using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using EdiTool;



public class CurveInit
{

    [MenuItem("CurveTool/ Create Curve")]

    public static void InitCurve()
    {

        GameObject _curve = new GameObject("Curve", typeof(Curve));
        Selection.activeObject = _curve;
        SceneView.lastActiveSceneView.pivot = _curve.transform.position;

    }

}

