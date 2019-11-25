using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

public class InitTool
{

    [MenuItem("SpawnTool/Init tool")]

    public static void Init()
    {
        SpawnTool[] allSpawn = Object.FindObjectsOfType<SpawnTool>();
        if (allSpawn.Length > 0) return;

        GameObject _obj = new GameObject("Spawn Tool", typeof(SpawnTool));

        Selection.activeGameObject = _obj;

       

    }


}
