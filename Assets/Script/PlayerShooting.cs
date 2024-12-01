using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public GameObject prefab;
    public GameObject playerShooting;

    public Material bulletMaterial;
    public Color defaultBullet;
    public Color powerBullet;
    public GameObject Bullet;

    public float detectAngle;
    public float detectRadius;
    public Collider[] detectEnemys;
    
    private ContactDamager cd;
    private bool powerFlag =true;
    private Rigidbody rb;

    private ParticleSystem ps;
    private PlayerMovement pm;
    public bool stopFlag = false;
    private Animator animator;
    // Start is called before the first frame update
    
    void Awake()
    {
        cd = Bullet.GetComponent<ContactDamager>();
        rb = GetComponent<Rigidbody>();
        ps = GetComponent<ParticleSystem>();
        pm = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        bulletMaterial.color = defaultBullet;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)&&!stopFlag)
        {
            animator.SetBool("isAttack", true);
            pm.audio[0].PlayOneShot(pm.bulletSound);
            GameObject clone = Instantiate(prefab);
            clone.transform.position = playerShooting.transform.position;
            clone.transform.rotation = playerShooting.transform.rotation;
        }
    }


    public void OnPowerUp(InputValue value)
    {
        if(value.isPressed && powerFlag)
        {
            pm.audio[0].PlayOneShot(pm.powerupSound);
            bulletMaterial.color = powerBullet;
            cd.damage = cd.damage * 2;
            Invoke("bulletBack", 3f);
            powerFlag = false;
        }
    }

    public void OnSkill(InputValue value)
    {
        detectEnemys = Physics.OverlapSphere(transform.position, detectRadius, LayerMask.GetMask("Enemy"));
        ps.Play();
        pm.audio[0].PlayOneShot(pm.skillSound);
        foreach (var enemy in detectEnemys)
        {
            float tempDot = Vector3.Dot(rb.transform.forward,-enemy.GameObject().GetComponent<Enemy>().Lookdir  );
            float tempAngle = Mathf.Acos(tempDot) * Mathf.Rad2Deg;
            if (tempAngle < detectAngle / 2)
            {
                Enemy EnemyComp = enemy.gameObject.GetComponent<Enemy>();
                if (EnemyComp.state != Enemy.EnemyState.Stun)
                {
                    Enemy.EnemyState temp = EnemyComp.state;
                    EnemyComp.state=Enemy.EnemyState.Stun;
                    EnemyComp.canChangestate = false;
                    EnemyComp.StartCoroutine(EnemyComp.StunBack(temp,3f));
                }
            }
                
        }
    }
    
    

    void bulletBack()
    {
        bulletMaterial.color = defaultBullet;
        cd.damage = cd.damage / 2;
        powerFlag = true;
        
    }

    void OnDrawGizmos()
    {
        DrawGizmosCone(transform.position, detectAngle, detectRadius, rb.transform.forward, Color.magenta,
            true);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(rb.transform.position, rb.transform.position + rb.transform.forward * detectRadius);
    }
    
    
    public static void DrawGizmosCircle(Vector3 pos,float radius,Vector3 up,Color color,int step=10,Action<Vector3>action=null)
    {
        float theta=360f/(float)step;
        Vector3 cross=Vector3.Cross(up,Vector3.up);
        if(cross.magnitude==0f)
        {
            cross=Vector3.forward;
        }

        Vector3 prev=pos+Quaternion.AngleAxis(0f,up)*cross*radius;
        Vector3 next=prev;
        Gizmos.color=color;

        for(int i=1;i<=step;++i)
        {
            next=pos+Quaternion.AngleAxis(theta*(float)i,up)*cross*radius;

            Gizmos.DrawLine(prev,next);

            if(null!=action)
            {
                action(prev);
            }

            prev=next;
        }
    }

    static Vector3 top=Vector3.zero;
    public static void DrawGizmosCone(Vector3 pos,float angle,float height,  Vector3 up,Color color,bool inverse=false,int step=10)
    {
        float radius=height*Mathf.Tan(angle*Mathf.Deg2Rad*0.5f);

        if(inverse)
        {
            top=pos;
            DrawGizmosCircle(pos+up*height,radius,up,color,step,(v)=>{Gizmos.DrawLine(top,v);});
        }
        else
        {
            top=pos+up*height;
            DrawGizmosCircle(pos,radius,up,color,step,(v)=>{Gizmos.DrawLine(top,v);});
        }
    }
}
