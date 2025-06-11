using UnityEngine;

public class EnemyFactory
{
    public Enemy BuildEnemy(GameContext gameContext, EnemyData enemyData)
    {
        GameObject enemyRoot = GameObject.Instantiate(enemyData.prefab);
        if (enemyData.prefab != null)
        {
            Enemy enemy = enemyRoot.AddComponent<Enemy>();
            enemy.Initialize(gameContext, enemyData);
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