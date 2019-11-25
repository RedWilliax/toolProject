using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTool : MonoBehaviour
{
    private void Start()
    {
        SpawnAll();
    }

    public List<SpawnPoint> AllPoint = new List<SpawnPoint>();

    public SpawnTrigger TriggerzonePrefab = null;

    public void AddPoint() => AllPoint.Add(new SpawnPoint());

    public void RemovePoint(int _i) => AllPoint.RemoveAt(_i);

    public void RemoveAll() => AllPoint.Clear();

    void SpawnAll()
    {
        if (!TriggerzonePrefab) return;

        for (int i = 0; i < AllPoint.Count; i++)
        {
            SpawnTrigger _trigger = Instantiate(TriggerzonePrefab);

            if (_trigger) _trigger.SetDate(AllPoint[i]);
        }
    }


}
