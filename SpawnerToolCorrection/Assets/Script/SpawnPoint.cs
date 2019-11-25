using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnPoint
{
    public bool activeDelay = false;

    public float delay = 0;

    public float delayAutoDestroy = 0;

    public bool InstantSpawn = false;

    public bool AutoDestroyAgent = false;

    public bool IsVisible = true;
    public List<SpawnMode> AllSpawnMode = new List<SpawnMode>();

    public bool IsMonoAgent = false;
    public GameObject MonoAgent = null;

    public List<GameObject> Agents = new List<GameObject>();

    public Vector3 Position = Vector3.zero;
    public Vector3 Size = Vector3.one;

    public void AddAgent() => Agents.Add(null);
    public void RemoveAgent(int _it) => Agents.RemoveAt( _it);

    public void RemoveAgent() => MonoAgent = null;

    public void RemoveAllAgent() => Agents.Clear();

    public void AddMode() => AllSpawnMode.Add(new SpawnMode());
    public void RemoveMode(int _i) => AllSpawnMode.RemoveAt(_i);
    public void RemoveAll() => AllSpawnMode.Clear();


}
