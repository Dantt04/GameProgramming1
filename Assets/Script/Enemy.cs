using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 velocity;
    public float moveSpeed;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        EnemyManager.instance.enemies.Add(this);
        rb = GetComponent<Rigidbody>();
        StartCoroutine(RandDirection());
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = player.transform.position - transform.position;
        dir.y = 0;
        transform.rotation = Quaternion.LookRotation(dir.normalized);
    }
    
    
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
            yield return new WaitForSeconds(Random.Range(1f,3f));
        }
        
    }
    
}
