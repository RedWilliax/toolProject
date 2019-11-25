using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EdiTool;
using UnityEditor;

[CustomEditor(typeof(Curve))]

public class CurveEditor : EditorCustom<Curve>
{
    System.Version version = new System.Version(0, 2, 0);

    int selectIndex = -1;

    protected override void OnEnable()
    {
        base.OnEnable();

        eTarget.SetCurve();

        Tools.current = Tool.None;
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        eTarget.CurveColor = EditorGUILayout.ColorField(eTarget.CurveColor);

        Layout.HelpBoxInfo($"Curve Tool v {version}");

        eTarget.curveDefinition = Layout.ETUSlider("Precision", ref eTarget.curveDefinition, 1, 1000);

        if (GUI.changed)
        {
            eTarget.SetCurve();
            SceneView.RepaintAll();
        }

        ETUButton.Button("Add", eTarget.Add, Color.green);
    }


    private void OnSceneGUI()
    {


        if (!eTarget.isValid) return;
        SceneCurveHandler();

    }






    void SceneCurveHandler()
    {

        Vector3 _lastHandle = eTarget.Anchor[eTarget.Anchor.Length - 1];

        _lastHandle = Handles.PositionHandle(_lastHandle, Quaternion.identity);

        eTarget.Anchor[eTarget.Anchor.Length - 1] = _lastHandle;

        if (GUI.changed)
            eTarget.SetCurve();

        for (int i = 0; i < eTarget.Anchor.Length; i += 3)
        {
            Vector3 _handleA = eTarget.Anchor[i];
            Vector3 _handleB = eTarget.Anchor[i + 1];
            Vector3 _handleC = eTarget.Anchor[i + 2];

            Handles.DrawLine(_handleA, _handleA + Vector3.up * 0.5f);
            Handles.DrawLine(_handleB, _handleB + Vector3.up * 0.5f);

            float _sizeA = HandleUtility.GetHandleSize(_handleA);
            float _sizeB = HandleUtility.GetHandleSize(_handleB);

            bool _pressA = Handles.Button(_handleA + Vector3.up * 0.5f, Quaternion.identity, 0.05f * _sizeA, 0.05f * _sizeA, Handles.DotHandleCap);
            bool _pressB = Handles.Button(_handleB + Vector3.up * 0.5f, Quaternion.identity, 0.05f * _sizeB, 0.05f * _sizeB, Handles.DotHandleCap);

            if (_pressA)
                selectIndex = i;
            else if (_pressB)
                selectIndex = i + 1;

            if (selectIndex == i)
            {
                _handleA = Handles.PositionHandle(_handleA, Quaternion.identity);

                if (GUI.changed)
                    eTarget.SetCurve();
            }
            else if (selectIndex == i + 1)
            {
                _handleB = Handles.PositionHandle(_handleB, Quaternion.identity);

                if (GUI.changed)
                    eTarget.SetCurve();

            }

            eTarget.Anchor[i] = _handleA;
            eTarget.Anchor[i + 1] = _handleB;

            Handles.color = Color.red;

            Handles.DrawDottedLine(_handleA, _handleB, 1);
            Handles.DrawDottedLine(_handleB, _handleC, 1);

            Handles.color = Color.white;

        }

        Vector3[] _curve = eTarget.Curvee;

                Handles.color = eTarget.CurveColor;
        for (int i = 0; i < _curve.Length; i++)
        {
            if (i < _curve.Length - 1)
            {
                Handles.DrawLine(_curve[i], _curve[i + 1]);
                Handles.color = Color.white;
            }
        }

        Handles.color = Color.red;

        Handles.DrawSolidDisc(eTarget.Curvee[0], Vector3.up, 0.2f);

        Handles.color = Color.green;

        Handles.DrawSolidDisc(eTarget.Curvee[eTarget.Curvee.Length-1], Vector3.up, 0.2f);


        Handles.color = Color.white;


    }
}