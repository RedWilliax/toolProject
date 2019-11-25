using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using EdiTool;
using System;

[CustomEditor(typeof(SpawnTool))]

public class SpawnToolEditor : EditorCustom<SpawnTool>
{
    Version version = new Version(1, 1, 0);

    const string triggerSpawnAssetName = "TriggerSpawn";

    protected override void OnEnable()
    {
        base.OnEnable();

        if (!eTarget.TriggerzonePrefab)
        {
            
            
            SpawnTrigger _triggerSpawn = Resources.Load<SpawnTrigger>(triggerSpawnAssetName);
            if (_triggerSpawn) eTarget.TriggerzonePrefab = _triggerSpawn;
        }
        Tools.current = Tool.None;
    }

    protected void OnDisable()
    {
        Tools.current = Tool.Move;
    }

    #region Inspector

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        EdiTool.Layout.HelpBoxInfo($"SPAWN TOOL V {version}");

        eTarget.TriggerzonePrefab = (SpawnTrigger)Layout.ObjectField(eTarget.TriggerzonePrefab, typeof(SpawnTrigger), false);

        if (!eTarget.TriggerzonePrefab) return;

        Layout.Space();

        DrawSpawnPointUI();
        SceneView.RepaintAll();
    }

    void DrawSpawnPointUI()
    {
        Layout.Horizontal(true);

        EdiTool.Layout.HelpBoxInfo("Add or Remove spawn Point");

        Layout.Vertical(true);

        EdiTool.ETUButton.Button("+", eTarget.AddPoint, Color.green);

        EdiTool.ETUButton.ButtonConfirmation("Delete all", eTarget.RemoveAll, "Remove all ?", "Delete all point ?", "Yes", "No", eTarget.AllPoint.Count > 0);

        Layout.Vertical(false);

        Layout.Horizontal(false);

        Layout.Space(2);

        for (int i = 0; i < eTarget.AllPoint.Count; i++)
        {
            Layout.Horizontal(true);
            ETUButton.ButtonConfirmation("-", eTarget.RemovePoint, "Remove point", $"Remove point {i + 1} ?", "Yes", "No", i);
            Layout.Horizontal(false);

            if (i > eTarget.AllPoint.Count - 1) return;

            Layout.HelpBox($"SpawnPoint {i + 1}");
            SpawnPoint _point = eTarget.AllPoint[i];
            Layout.Foldout(ref _point.IsVisible, "Show / Hide point", true);

            if (_point.IsVisible)
            {
                _point.Position = EditorGUILayout.Vector3Field("Position", _point.Position);
                _point.Size = EditorGUILayout.Vector3Field("Size", _point.Size);

                Layout.Space();

                Layout.Horizontal(true);

                eTarget.AllPoint[i].InstantSpawn = EditorGUILayout.Toggle("Instant", eTarget.AllPoint[i].InstantSpawn);

                if (eTarget.AllPoint[i].InstantSpawn) eTarget.AllPoint[i].activeDelay = false;

                Layout.Horizontal(false);

                Layout.Space();

                Layout.Horizontal(true);

                eTarget.AllPoint[i].activeDelay = EditorGUILayout.Toggle("Delay", eTarget.AllPoint[i].activeDelay);

                if (eTarget.AllPoint[i].activeDelay)
                {
                    Layout.HelpBox("Spawner delay");

                    eTarget.AllPoint[i].InstantSpawn = false;

                    eTarget.AllPoint[i].delay = EditorGUILayout.FloatField(eTarget.AllPoint[i].delay);
                }

                Layout.Horizontal(false);

                Layout.Space();

                Layout.Horizontal(true);

                eTarget.AllPoint[i].AutoDestroyAgent = EditorGUILayout.Toggle("AutoDestroy Agent", eTarget.AllPoint[i].AutoDestroyAgent);

                if (eTarget.AllPoint[i].AutoDestroyAgent)
                {
                    Layout.HelpBox("Auto Destroy Agent delay");

                    eTarget.AllPoint[i].delayAutoDestroy = EditorGUILayout.FloatField(eTarget.AllPoint[i].delayAutoDestroy);
                }

                Layout.Horizontal(false);

                DrawSpawModeUI(_point);
                Layout.Space();
            }
            DrawAgentUi(_point);
        }

    }

    void DrawAgentUi(SpawnPoint _point)
    {
        _point.IsMonoAgent = EditorGUILayout.Toggle("Unique Agent ?", _point.IsMonoAgent);

        if (_point.IsMonoAgent)
        {
            Layout.Horizontal(true);

            _point.MonoAgent = (GameObject)Layout.ObjectField(_point.MonoAgent, typeof(GameObject), false);

            EdiTool.ETUButton.ButtonConfirmation("Delete Agent", _point.RemoveAgent, "Remove agent ?", "Delete ?", "Yes", "No");

            Layout.Horizontal(false);
        }
        else
        {
            ETUButton.Button("Add agent", _point.AddAgent, Color.cyan);

            EdiTool.ETUButton.ButtonConfirmation("Delete all", _point.RemoveAllAgent, "Remove all ?", "Delete all Agent ?", "Yes", "No", _point.Agents.Count > 0);

            for (int j = 0; j < _point.Agents.Count; j++)
            {
                Layout.Horizontal(true);

                _point.Agents[j] = (GameObject)Layout.ObjectField(_point.Agents[j], typeof(GameObject), false);

                ETUButton.ButtonConfirmation("-", _point.RemoveAgent, "Remove agent", $"Remove agent {j + 1} ?", "Yes", "No", j);

                Layout.Horizontal(false);

            }
        }
    }

    void DrawSpawModeUI(SpawnPoint _point)
    {
        Layout.Horizontal(true);
        Layout.Space();
        Layout.HelpBoxInfo("Add or Remove modes");
        Layout.Vertical(true);
        ETUButton.Button("+", _point.AddMode, Color.yellow);
        ETUButton.ButtonConfirmation("Clear all modes", _point.RemoveAll, "Remove all", "Remove all modes ?", "Yes", "No", _point.AllSpawnMode.Count > 0);
        Layout.Vertical(false);

        Layout.Horizontal(false);


        for (int i = 0; i < _point.AllSpawnMode.Count; i++)
        {
            SpawnMode _mode = _point.AllSpawnMode[i];
            Layout.Space();

            Layout.Horizontal(true);

            _mode.Type = (SpawnType)Layout.EnumPop("Mode Type", _mode.Type);

            //_mode.Type = (SpawnType)EditorGUILayout.EnumPopup("Mode Type", _mode.Type);
            ETUButton.ButtonConfirmation("-", _point.RemoveMode, "Remove mode", $"Remove point {i + 1} ?", "Yes", "No", i);
            Layout.Horizontal(false);
            DrawModeSettings(_mode);

            Layout.Space();
        }


    }

    void DrawModeSettings(SpawnMode _mode) => _mode.Mode.DrawSettings();

    #endregion

    #region Scene

    private void OnSceneGUI()
    {
        if (!eTarget.TriggerzonePrefab) return;

        DrawSpawnPointScene();


    }


    void DrawSpawnPointScene()
    {

        for (int i = 0; i < eTarget.AllPoint.Count; i++)
        {

            SpawnPoint _point = eTarget.AllPoint[i];

            ETUHandles.DrawWireCube(_point.Position, _point.Size, Color.green);

            _point.Position = ETUHandles.PositionHandles(_point.Position, Quaternion.identity);
            _point.Size = ETUHandles.ScaleHandles(_point.Size, _point.Position, 1.5f, Quaternion.identity);

            GetModesScene(_point);
        }

    }

    void GetModesScene(SpawnPoint _point)
    {
        for (int i = 0; i < _point.AllSpawnMode.Count; i++)
        {
            SpawnMode _mode = _point.AllSpawnMode[i];
            DrawModeScene(_mode, _point);
        }

    }

    void DrawModeScene(SpawnMode _mode, SpawnPoint _fromSpawner)
    {
        _mode.Mode.DrawLinkToSpawner(_fromSpawner.Position);
        _mode.Mode.DrawSceneMode();
    }

    #endregion

}
