using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WavesGameMode : MonoBehaviour
{
    [SerializeField] Life playerLife;
    [SerializeField] Life playerBaseLife;
    public GameObject LifeText;
    private TextMeshProUGUI textMesh;
    void Start()
    {
        playerLife.onDeath.AddListener(OnPlayerOrBaseDied);
        playerBaseLife.onDeath.AddListener(OnPlayerOrBaseDied);
        EnemyManager.instance.onChanged.AddListener(CheckWinCondition);
        WaveManager.instance.onChanged.AddListener(CheckWinCondition);
        textMesh = LifeText.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = "HP : " + playerLife.amount.ToString()+"\nBase HP : "+playerBaseLife.amount.ToString();
    }

    void OnPlayerOrBaseDied()
    {
        foreach (var enemy in EnemyManager.instance.enemies)
        {
            Destroy(enemy);
        }

        
        SceneManager.LoadScene("LoseScreen");
        
    }

    void CheckWinCondition()
    {
        if(ScoreManager.instance.amount>=30)
        {
            SceneManager.LoadScene("WinScreen");
            
        }
    }

    
}
