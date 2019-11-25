using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    [SerializeField] SpawnPoint data = null;

    [SerializeField] BoxCollider triggerZone = null;

    public bool Triggered { get; set; } = false;

    public void SetDate(SpawnPoint _data)
    {
        data = _data;
        if (_data.InstantSpawn)
        {
            OnTriggerEnter();
            return;
        }

        transform.position = data.Position;

        if (triggerZone) triggerZone.size = _data.Size;

    }

    void TriggerSpawn()
    {
        for (int i = 0; i < data.AllSpawnMode.Count; i++)
        {
            SpawnMode _mode = data.AllSpawnMode[i];

            if(data.AutoDestroyAgent)
            {
                if (data.IsMonoAgent)
                    _mode.Mode.SpawnWithDestroy(data.MonoAgent, data.delayAutoDestroy);
                else
                    _mode.Mode.SpawnWithDestroy(data.Agents, data.delayAutoDestroy);
            }
            else
            {
                if (data.IsMonoAgent)
                    _mode.Mode.Spawn(data.MonoAgent);
                else
                    _mode.Mode.Spawn(data.Agents);
            }
        }
        Triggered = true;
    }

    IEnumerator SpawnWithDelay(float _time)
    {

        if (Triggered || data == null) yield break;
        Triggered = true;
        yield return new WaitForSeconds(_time);
        TriggerSpawn();

    }

    private void OnTriggerEnter()
    {
        if (data != null)
            StartCoroutine(SpawnWithDelay(data.activeDelay ? data.delay : 0));

    }
    private void OnDrawGizmos()
    {
        if(triggerZone)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, triggerZone.size);
        }

    }

}
