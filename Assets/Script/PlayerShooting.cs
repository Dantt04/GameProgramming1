using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject prefab;
    public GameObject playerShooting;
    // Start is called before the first frame update
    void Start()
    {
        
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
}
