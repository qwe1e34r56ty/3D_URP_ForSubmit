using System;
using TMPro;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    private GameContext gameContext;
    [SerializeField]
    private TMP_Text text;
    
    public void Initialize(GameContext gameContext, ItemData itemData)
    {
        this.gameContext = gameContext;
        text?.SetText(itemData.name);
    }
}