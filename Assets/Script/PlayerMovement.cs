using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f;
    public float rotationSpeed = 1f;
    private Vector2 movementValue;
    private float lookValue;
    public GameObject Menu;
    public GameObject MainMenu;
    public TextMeshProUGUI FPS;
    
    private PlayerShooting ps;
    private Rigidbody rb;
    public AudioSource[] audio;
    public AudioClip bulletSound;
    public AudioClip skillSound;
    public AudioClip healSound;
    public AudioClip powerupSound;
    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
        ps = GetComponent<PlayerShooting>();
        rb = GetComponent<Rigidbody>();
        audio = gameObject.GetComponents<AudioSource>();
    }

    void Update()
    {
        rb.AddRelativeForce(movementValue.x,0,movementValue.y);
        rb.AddRelativeTorque(0, lookValue*Time.deltaTime, 0);
        
    }

    public void OnMove(InputValue value )
    {
        movementValue = value.Get<Vector2>()*speed*Time.deltaTime*144;
    }

    public void OnLook(InputValue value)
    {
        lookValue = value.Get<Vector2>().x*rotationSpeed*Time.deltaTime*144;
    }

    public void OnMenu(InputValue value)
    {
        if (!Menu.activeSelf)
        {
            Menu.SetActive(true);
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            ps.stopFlag=true;
        }

        else
        {
            Menu.SetActive(false);
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            ps.stopFlag=false;
        }
            
    }

    public void Continue()
    {
        Menu.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        ps.stopFlag=false;
        
    }
    
    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
    
    public void MainPlay()
    {
        MainMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(UpdateFPS());
        audio[0].Play();
        audio[1].Play();
        
    }
    
    public void MainQuit()
    {
        Application.Quit();
    }

    IEnumerator UpdateFPS()
    {
        FPS.text = "FPS: " + (int)(1.0f/Time.deltaTime);
        while (true)
        {
            if (!ps.stopFlag)
            {
                FPS.text = "FPS: " + (int)(1.0f/Time.deltaTime);
                
            }
            yield return new WaitForSeconds(0.5f);
        }
        
    }
    
}
