using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Customs/PlayerData")]
public class PlayerData : AEntityData
{
    public int gold;
    public int speed;
    public List<ItemData> inventory;
}