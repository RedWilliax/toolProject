using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
[System.Serializable]
public class PointMode : Mode
{
    public override void Spawn(GameObject _gobj)
    {
        if (!_gobj) return;

        Object.Instantiate(_gobj, Position, Quaternion.identity);
    }

    public override void Spawn(List<GameObject> _gobj)
    {
        int _randomAgent = Random.Range(0, _gobj.Count);
        if (!_gobj[_randomAgent]) return;
        Object.Instantiate(_gobj[_randomAgent], Position, Quaternion.identity);
    }

    public override void SpawnWithDestroy(GameObject _gobj, float _time)
    {
        GameObject _obj;

        _obj = Object.Instantiate(_gobj, Position, Quaternion.identity);

        Object.Destroy(_obj, _time);

    }

    public override void SpawnWithDestroy(List<GameObject> _gobj, float _time)
    {
        GameObject _obj;
        int _randomAgent = Random.Range(0, _gobj.Count);
        if (!_gobj[_randomAgent]) return;

        _obj = Object.Instantiate(_gobj[_randomAgent], Position, Quaternion.identity);

        Object.Destroy(_obj, _time);
    }

#if UNITY_EDITOR
    public override void DrawLinkToSpawner(Vector3 _position) => Handles.DrawDottedLine(_position, Position, 0.5f);

    public override void DrawSceneMode()
    {
        Position = Handles.PositionHandle(Position, Quaternion.identity);
    }

    public override void DrawSettings()
    {
        
    }
#endif
}
