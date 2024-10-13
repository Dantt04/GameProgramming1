using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("restart",3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void restart()
    {
        SceneManager.LoadScene("Assignment1");
    }
}
