using UnityEngine;

public class EnemyFactory
{
    public Enemy BuildEnemy(GameContext gameContext, EnemyData enemyData)
    {
        GameObject enemyRoot = GameObject.Instantiate(enemyData.enemyPrefab);
        if (enemyData.enemyPrefab != null)
        {
            Enemy enemy = enemyRoot.AddComponent<Enemy>();
            enemy.initialize(gameContext, enemyData);
            return enemy;
        }
        return null;
    }

    public void DestoryStage(GameObject enemyRoot)
    {
        if (enemyRoot != null)
        {
            GameObject.Destroy(enemyRoot);
        }
    }
}