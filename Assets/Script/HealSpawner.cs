using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealSpawner : MonoBehaviour
{
    public Vector2 spawnRange;
    public GameObject healPrefab;
    private Coroutine spawnCoroutine;
    void Start()
    {
        spawnCoroutine = StartCoroutine(Spawn());
    }
    

    IEnumerator Spawn()
    {
        while (true)
        {
            Instantiate(healPrefab,randomPosition(),transform.rotation);
            yield return new WaitForSeconds(15f);
        }
    }

    Vector3 randomPosition()
    {
        float x = Random.Range(-spawnRange.x/2, spawnRange.x/2);
        float z = Random.Range(-spawnRange.y/2, spawnRange.y/2);
        return new Vector3(x+transform.position.x,transform.position.y,z+transform.position.z);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnRange.x, transform.position.y, spawnRange.y));
    }
}
