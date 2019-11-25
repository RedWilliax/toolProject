using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class CircleMode : Mode
{
    public int Radius = 5;
    public int AgentNumber = 10;


    Vector3 GetRadiusPosition(int _pos, int _maxPos, int _radius, Vector3 _center)
    {
        float _angle = (float)_pos / AgentNumber * Mathf.PI * 2;

        float _x = _center.x + Mathf.Cos(_angle) * _radius;
        float _y = _center.y;
        float _z = _center.z + Mathf.Sin(_angle) * _radius;

        return new Vector3(_x, _y, _z);
    }

    public override void Spawn(GameObject _gobj)
    {
        if (!_gobj) return;
        for (int i = 0; i < AgentNumber; i++)
        {
            Object.Instantiate(_gobj, GetRadiusPosition(i, AgentNumber, Radius, Position), Quaternion.identity);
        }
    }

    public override void Spawn(List<GameObject> _gobj)
    {
        for (int i = 0; i < AgentNumber; i++)
        {
            int _randomAgent = Random.Range(0, _gobj.Count);
            if (!_gobj[_randomAgent]) continue;
            Object.Instantiate(_gobj[_randomAgent], GetRadiusPosition(i, AgentNumber, Radius, Position), Quaternion.identity);
        }
    }
    
    public override void SpawnWithDestroy(GameObject _gobj, float _time)
    {
        GameObject _obj;

        if (!_gobj) return;
        for (int i = 0; i < AgentNumber; i++)
        {
            _obj = Object.Instantiate(_gobj, GetRadiusPosition(i, AgentNumber, Radius, Position), Quaternion.identity);
            Object.Destroy(_obj, _time);
        }
    }

    public override void SpawnWithDestroy(List<GameObject> _gobj, float _time)
    {
        GameObject _obj;

        for (int i = 0; i < AgentNumber; i++)
        {
            int _randomAgent = Random.Range(0, _gobj.Count);
            if (!_gobj[_randomAgent]) continue;
            _obj = Object.Instantiate(_gobj[_randomAgent], GetRadiusPosition(i, AgentNumber, Radius, Position), Quaternion.identity);

            Object.Destroy(_obj, _time);
        }
    }

#if UNITY_EDITOR

    public override void DrawSettings()
    {
        Radius = EditorGUILayout.IntSlider("Radius", Radius, 1, 100);
        AgentNumber = EditorGUILayout.IntSlider("AgentNumber", AgentNumber, 1, 360);
    }

    public override void DrawLinkToSpawner(Vector3 _position) => Handles.DrawDottedLine(_position, Position, 0.5f);

    public override void DrawSceneMode()
    {
        Position = Handles.PositionHandle(Position, Quaternion.identity);

        Handles.DrawWireDisc(Position, Vector3.up, Radius);

        for (int i = 0; i < AgentNumber; i++)
        {
            Handles.CubeHandleCap(i, GetRadiusPosition(i, AgentNumber, Radius, Position), Quaternion.identity, 0.2f, EventType.Repaint);
        }
    }
#endif
}
