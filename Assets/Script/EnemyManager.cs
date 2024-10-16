using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    
    public List<Enemy> enemies;
    public static EnemyManager instance;
    public UnityEvent onChanged;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogError("Duplicated EnemyManager,ignoring this one");
    }

    // Update is called once per frame
    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
        onChanged.Invoke();
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
        onChanged.Invoke();
    }
}
