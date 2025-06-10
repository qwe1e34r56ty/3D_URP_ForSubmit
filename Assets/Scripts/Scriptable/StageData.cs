using UnityEngine;

[CreateAssetMenu(fileName = "Stage", menuName = "Customs/StageData")]
public class StageData : ScriptableObject 
{
    public int rooms = 5;
    public int roomWidth = 20;
    public int roomHeight = 20;
    public GameObject floorPrefab;
    public GameObject EndPrefab;
    public EnemyData enemyData;
}