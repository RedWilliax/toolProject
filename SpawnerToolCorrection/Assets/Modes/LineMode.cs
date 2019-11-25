using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class LineMode : Mode
{
    public int AgentNumber = 10;

    public Vector3 EndPos = Vector3.one;


    public override void Spawn(GameObject _gobj)
    {
        if (!_gobj) return;
        for (int i = 0; i < AgentNumber; i++)
        {
            Object.Instantiate(_gobj, GetLinearPosition(i, AgentNumber, Position, EndPos), Quaternion.identity);
        }
    }

    public override void Spawn(List<GameObject> _gobj)
    {
        for (int i = 0; i < AgentNumber; i++)
        {
            int _randomAgent = Random.Range(0, _gobj.Count);
            if (!_gobj[_randomAgent]) continue;
            Object.Instantiate(_gobj[_randomAgent], GetLinearPosition(i, AgentNumber,Position, EndPos), Quaternion.identity);
        }
    }

    public Vector3 GetLinearPosition(int _pos, int _maxPos, Vector3 _position, Vector3 _endPosition)
    {
        return Vector3.Lerp(_position, _endPosition, (float)_pos / _maxPos);
    }

    public override void SpawnWithDestroy(GameObject _gobj, float _time)
    {
        GameObject _obj;

        if (!_gobj) return;
        for (int i = 0; i < AgentNumber; i++)
        {
            _obj = Object.Instantiate(_gobj, GetLinearPosition(i, AgentNumber, Position, EndPos), Quaternion.identity);
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
            _obj = Object.Instantiate(_gobj[_randomAgent], GetLinearPosition(i, AgentNumber, Position, EndPos), Quaternion.identity);

            Object.Destroy(_obj, _time);
        }
    }

#if UNITY_EDITOR

    public override void DrawLinkToSpawner(Vector3 _position) => Handles.DrawDottedLine(_position, Position, 0.5f);

    public override void DrawSceneMode()
    {
        Position = Handles.PositionHandle(Position, Quaternion.identity);
        EndPos = Handles.PositionHandle(EndPos, Quaternion.identity);


        Handles.DrawLine(Position, EndPos);


        

        for (int i = 0; i < AgentNumber; i++)
        {
            Handles.CubeHandleCap(i, GetLinearPosition(i, AgentNumber, Position, EndPos), Quaternion.identity, 0.2f, EventType.Repaint);
        }
    }

    public override void DrawSettings()
    {
        AgentNumber = EditorGUILayout.IntSlider("AgentNumber", AgentNumber, 1, 360);
    }


#endif
}
