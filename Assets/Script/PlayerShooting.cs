using System.Collections;
using System.Collections.Generic;
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

    private ContactDamager cd;
    private bool powerFlag =true;
    // Start is called before the first frame update
    
    void Awake()
    {
        cd = Bullet.GetComponent<ContactDamager>();
        bulletMaterial.color = defaultBullet;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
           
            GameObject clone = Instantiate(prefab);
            clone.transform.position = playerShooting.transform.position;
            clone.transform.rotation = playerShooting.transform.rotation;
        }
    }

    public void OnPowerUp(InputValue value)
    {
        if(value.isPressed == true&& powerFlag)
        {
            bulletMaterial.color = powerBullet;
            cd.damage = cd.damage * 2;
            Invoke("bulletBack", 3f);
            powerFlag = false;
        }
    }

    void bulletBack()
    {
        bulletMaterial.color = defaultBullet;
        cd.damage = cd.damage / 2;
        powerFlag = true;
        Debug.Log("1");
    }
}
