using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curve : MonoBehaviour
{
    public List<Vector3> allPoint = new List<Vector3>();

    public Color CurveColor = Color.white; 

    public Vector3[] Anchor = new Vector3[]
    {
        new Vector3(0,0,0),
        new Vector3(0,0,1),
        new Vector3(0,0,2)
    };

    [SerializeField] Vector3[] curvePoints = new Vector3[] { };

    public Vector3[] Curvee => curvePoints;

    [SerializeField, Header("Precision"), Range(3, 100)] public int curveDefinition = 3;

    public bool isValid => true;

    public Vector3[] ComputeCurve(Vector3 _a, Vector3 _b, Vector3 _c, int _curveDefinition)
    {
        Vector3[] _curve = new Vector3[_curveDefinition + 1];

        for (int i = 0; i < _curveDefinition; i++)
        {

            float _t = (float)i / _curveDefinition;

            Vector3 _firstPart = Vector3.Lerp(_a, _b, _t);
            Vector3 _secondPart = Vector3.Lerp(_b, _c, _t);

            _curve[i] = Vector3.Lerp(_firstPart, _secondPart, _t);

        }
        _curve[curveDefinition] = _c;

        return _curve;
    }

    Vector3[] ComputeCurve(Vector3[] _anchor, int _curveDefinition)
    {

        Vector3[] _curve = new Vector3[_curveDefinition * (_anchor.Length/3)];

        int _curveIndex = 0;

        for (int i = 0; i < _anchor.Length; i+= 3)
        {
            Vector3 _a = Vector3.zero;
            Vector3 _b = Vector3.zero;
            Vector3 _c = Vector3.zero;

            for (int j = 0; j < _curveDefinition; j++)
            {
                float _t = (float)j / _curveDefinition;

                _a = _anchor[i];
                _b = _anchor[i + 1];
                _c = _anchor[i + 2];

                Vector3 _firstPart = Vector3.Lerp(_a, _b, _t);
                Vector3 _secondPart = Vector3.Lerp(_b, _c, _t);

                _curve[_curveIndex] = Vector3.Lerp(_firstPart, _secondPart, _t);
                _curveIndex++;
            }
            if (i > 1)
                _anchor[i - 1] = _anchor[i];
            _curve[_curveIndex - 1] = _c;
        }
        return _curve;
    }

    public void AddPartOfCurve()
    {
        if (allPoint.Count <= 0)
        {
            AddPoint(3);
        }
        else
            AddPoint();

    }

    void AddPoint(int _nbOfPoint = 2)
    {
        for (int i = 0; i < _nbOfPoint; i++)
        {
            if (allPoint.Count <= 3)
                allPoint.Add(new Vector3());
            else
            {
                int _lastPoint = allPoint.Count - 1;
                allPoint.Add(new Vector3(allPoint[_lastPoint].x, allPoint[_lastPoint].y, allPoint[_lastPoint].z));
            }


        }
    }

    public void RemovePoint()
    {
        for (int i = 0; i < 2; i++)
        {
            int _indexToRemove = allPoint.Count - 1;

            allPoint.Remove(allPoint[_indexToRemove]);
        }
    }

    public void Clear()
    {
        allPoint.Clear();
    }


    public void Add()
    {

        Vector3 _lastPoint = Anchor[Anchor.Length - 1];

        System.Array.Resize(ref Anchor, Anchor.Length + 3);

        int _index = 0;

        for (int i = Anchor.Length - 3; i < Anchor.Length; i++)
        {
            _index++;
            Vector3 _newPoint = _lastPoint + Vector3.forward;
            Anchor[i] = _newPoint;
        }

    }

    public void SetCurve() => curvePoints = ComputeCurve(Anchor, curveDefinition);

    private void OnDrawGizmos()
    {
        Gizmos.color = CurveColor;
        for (int i = 0; i < curvePoints.Length; i++)
        {

            if (i < curvePoints.Length - 1)
                Gizmos.DrawLine(curvePoints[i], curvePoints[i + 1]);

        }

    }

}
