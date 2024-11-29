using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Heal : MonoBehaviour
{
    public int healAmount;
    private GameObject player;
    private PlayerMovement pm;
    // Start is called before the first frame update
    void Awake()
    {
        
        
    }

    // Update is called once per frame
    void Start()
    {
        player = GameObject.Find("Player");
        pm = player.GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var Life = other.GetComponent<Life>();
            Life.Heal(healAmount);
            pm.audio[1].PlayOneShot(pm.healSound);
            Destroy(gameObject);
        }
    }
}
