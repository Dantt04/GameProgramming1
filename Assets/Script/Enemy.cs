using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 velocity;
    public float moveSpeed;
    private GameObject player;

    public GameObject EnemyBullet;
    public Transform ShotPoint;
    
    public Material defaultMaterial;
    public Material StunMaterial;

    public Vector3 Lookdir;
    private SkinnedMeshRenderer meshRenderer;
    private Coroutine attackCoroutine;
    private Coroutine moveCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        EnemyManager.instance.enemies.Add(this);
        rb = GetComponent<Rigidbody>();
        attackCoroutine=StartCoroutine(RandDirection());
        moveCoroutine=StartCoroutine(Attack());
        player = GameObject.FindGameObjectWithTag("Player");
        meshRenderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Lookdir = player.transform.position - transform.position;
        Lookdir.y = 0;
        Lookdir.Normalize();
        transform.rotation = Quaternion.LookRotation(Lookdir);
    }
    
    
    void FixedUpdate()
    {
        rb.velocity = velocity;

    }
    void OnDestroy()
    {
        EnemyManager.instance.RemoveEnemy(this);
    }

    IEnumerator Attack()
    {
        while (true)
        {
            Instantiate(EnemyBullet, ShotPoint.position, transform.rotation);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator RandDirection()
    {
        while (true)
        {
            velocity = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * moveSpeed;
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }

    public void Stop()
    {
        StopCoroutine(attackCoroutine);
        StopCoroutine(moveCoroutine);
        velocity=Vector3.zero;
        meshRenderer.material = StunMaterial;
        Invoke("BackStop",3f);
    }
    
    
    void BackStop()
    {
        attackCoroutine=StartCoroutine(RandDirection());
        moveCoroutine=StartCoroutine(RandDirection());
        meshRenderer.material = defaultMaterial;
    }
}
