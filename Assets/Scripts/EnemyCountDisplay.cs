using UnityEngine;
using UnityEngine.UI;

public class EnemyCountDisplay : MonoBehaviour
{
    public Text enemyCountText;

    void Update()
    {
        // Check if the enemyCountText is assigned
        if (enemyCountText == null)
        {
            Debug.LogWarning("Enemy count text is not assigned.");
            return;
        }

        // Get the enemy count from the EnemyManager
        int enemiesAlive = EnemyManager.Instance.GetEnemiesAlive();

        // Update the UI text with the enemy count
        enemyCountText.text = "Enemies Alive: " + enemiesAlive;
    }
}
