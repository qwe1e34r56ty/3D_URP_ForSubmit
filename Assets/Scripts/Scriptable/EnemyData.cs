using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Customs/EnemyData")]
public class EnemyData : ScriptableObject
{
    public GameObject enemyPrefab;
    public int hp;
    public int maxHp;
}