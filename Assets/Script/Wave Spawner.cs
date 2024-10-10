using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject prefab;
    public float startTime;
    public float endTime;
    public float spawnRate;
    public float spawnRadius;
    // Start is called before the first frame update
    void Start()
    {
        WaveManager.instance.waves.Add(this);
        InvokeRepeating("Spawn", startTime, spawnRate);
        Invoke("EndSpawner", endTime);
    }

    void Spawn()
    {
        Instantiate(prefab, randomPosition(), transform.rotation);
    }

    // Update is called once per frame
    void EndSpawner()
    {
        WaveManager.instance.waves.Remove(this);
        CancelInvoke();
    }

    Vector3 randomPosition()
    {
        Vector3 randPos = ((Vector3)Random.insideUnitSphere * spawnRadius )+ transform.position;
        randPos.y = transform.position.y;
        return randPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1,0,0,1);
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
