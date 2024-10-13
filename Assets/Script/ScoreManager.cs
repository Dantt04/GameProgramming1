using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public GameObject scoreText;
    public int amount;
    
    private TextMeshProUGUI  text;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogError("Duplicated ScoreManager,ignoring this one");
    }
    void Start()
    {
        text = scoreText.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Score: " + amount.ToString() +" / 30";
    }
}
