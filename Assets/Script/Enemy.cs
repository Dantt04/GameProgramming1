using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 velocity;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        EnemyManager.instance.enemies.Add(this);
        rb = GetComponent<Rigidbody>();
        StartCoroutine(RandDirection());
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        rb.velocity = velocity;
    }
    void OnDestroy()
    {
        EnemyManager.instance.enemies.Remove(this);
    }

    IEnumerator RandDirection()
    {
        while (true)
        {
            velocity = new Vector3(Random.Range(-1f, 1f) , 0, Random.Range(-1f, 1f) ).normalized* moveSpeed;
            Debug.Log(velocity);
            yield return new WaitForSeconds(Random.Range(1f,3f));
        }
        
    }
    
}
