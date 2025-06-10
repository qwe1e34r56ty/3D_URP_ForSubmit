using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Customs/PlayerData")]
public class PlayerData : ScriptableObject
{
    public GameObject playerPrefab;
    public int gold;
    public int hp;
    public int maxHp;
    public int speed;
    public List<ItemData> inventory;
}