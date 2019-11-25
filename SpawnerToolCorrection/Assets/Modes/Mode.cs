using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public abstract class Mode
{

    public Vector3 Position = Vector3.zero;

    public abstract void Spawn(GameObject _gobj);
    public abstract void Spawn(List<GameObject> _gobj);

    public abstract void SpawnWithDestroy(GameObject _gobj, float _time);
    public abstract void SpawnWithDestroy(List<GameObject> _gobj, float _time);

#if UNITY_EDITOR

    public abstract void DrawSettings();

    public abstract void DrawLinkToSpawner(Vector3 _position);

    public abstract void DrawSceneMode();

#endif
}
