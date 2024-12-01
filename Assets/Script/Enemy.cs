using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 velocity;
    public float moveSpeed;

    public float detectPlayerRadius = 10f;
    public float attackPlayerRadius = 5f;
    private GameObject player;
    private GameObject Base;

    public GameObject EnemyBullet;
    public Transform ShotPoint;

    public GameObject particle;
    
    public Material defaultMaterial;
    public Material StunMaterial;

    public Vector3 Lookdir;
    private SkinnedMeshRenderer meshRenderer;
    private Coroutine attackCoroutine;
    private Coroutine moveCoroutine;
    // Start is called before the first frame update
    
    private bool isAttack = false;
    
    public enum EnemyState
    {
        BaseAttack,GotoBase,Stun,AttackPlayer,ChasePlayer
    }

    private Life life;
    public EnemyState state = EnemyState.GotoBase;
    public bool canChangestate = true;
    private Life baseLife;
    private ParticleSystem ps;
    private Animator animator;
    void Start()
    {
        EnemyManager.instance.enemies.Add(this);
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        Base = GameObject.FindGameObjectWithTag("Base");
        meshRenderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        baseLife = Base.GetComponent<Life>();
        ps = gameObject.GetComponent<ParticleSystem>();
        life = gameObject.GetComponent<Life>();
        animator = GetComponent<Animator>();
        life.onDeath.AddListener(()=>Instantiate(particle, transform.position, Quaternion.identity));
    }

    private float playerDistance;
    private float baseDistance;
    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector3.Distance(player.transform.position, transform.position);
        baseDistance = Vector3.Distance(Base.transform.position, transform.position);

        if (canChangestate)
        {
            if (baseDistance < 3f)
            {
                state = EnemyState.BaseAttack;
                StartCoroutine(AttackBase());
                
                canChangestate =false;
            }
            else if ( playerDistance> detectPlayerRadius)
            {
                state = EnemyState.GotoBase;
                

            }
            else if (playerDistance > attackPlayerRadius)
            {
                state = EnemyState.ChasePlayer;
                
            }
            else
            {
                state = EnemyState.AttackPlayer;
                
                if (!isAttack)
                {
                    StartCoroutine(Attack());
                    isAttack = true;
                }
                    
            }
        }
        
        
        switch (state)
        {
            case EnemyState.BaseAttack:
                Lookdir = Base.transform.position - transform.position;
                rb.velocity = Vector3.zero;
                animator.SetBool("AttackB", true);
                break;
            
            case EnemyState.Stun:
                rb.velocity = Vector3.zero;
                break;
            
            case EnemyState.GotoBase:
                Lookdir = Base.transform.position - transform.position;
                Lookdir.y = 0;
                Lookdir.Normalize();
                rb.velocity = Lookdir * moveSpeed;
                animator.SetBool("DetectP", false);
                break;
            
            case EnemyState.AttackPlayer:
                Lookdir = player.transform.position - transform.position;
                rb.velocity = Vector3.zero;
                animator.SetBool("AttackP", true);
                break;
            
            case EnemyState.ChasePlayer:
                Lookdir = player.transform.position - transform.position;
                Lookdir.y = 0;
                Lookdir.Normalize();
                rb.velocity = Lookdir * moveSpeed;
                animator.SetBool("AttackP", false);
                animator.SetBool("DetectP", true);
                break;
        }
        
        transform.rotation = Quaternion.LookRotation(Lookdir);
    }
    
    void OnDestroy()
    {
        
        EnemyManager.instance.RemoveEnemy(this);
    }

    IEnumerator Attack()
    {
        while (true)
        {
            if(state==EnemyState.AttackPlayer&&Time.time!=0)
                Instantiate(EnemyBullet, ShotPoint.position, transform.rotation);
            yield return new WaitForSeconds(1.5f);
        }
    }
    
    IEnumerator AttackBase()
    {
        while (true)
        {
            baseLife.amount -= 10;
            
            yield return new WaitForSeconds(3f);
        }
    }

    public IEnumerator StunBack(EnemyState temp, float time)
    {
        meshRenderer.material = StunMaterial;
        canChangestate = false;
        yield return new WaitForSeconds(time);
        state = temp;
        canChangestate = true;
        meshRenderer.material = defaultMaterial;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.7f,0.5f,0.4f,1);
        Gizmos.DrawWireSphere(transform.position, detectPlayerRadius);
        
        Gizmos.color = new Color(0.3f,0.8f,0.8f,1);
        Gizmos.DrawWireSphere(transform.position, attackPlayerRadius);
    }
}
