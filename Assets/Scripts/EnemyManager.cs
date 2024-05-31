using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    public Text enemyCountText; // Reference to a UI Text component to display the count
    private int enemiesAlive = 0;
    private List<GameObject> enemies = new List<GameObject>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
        enemiesAlive++;
        UpdateEnemyCountUI();
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
        enemiesAlive--;
        UpdateEnemyCountUI();
    }

    public int GetEnemiesAlive()
    {
        return enemiesAlive;
    }

    private void UpdateEnemyCountUI()
    {
        if (enemyCountText != null)
        {
            enemyCountText.text = "Enemies Alive: " + enemiesAlive;
        }
    }
}
