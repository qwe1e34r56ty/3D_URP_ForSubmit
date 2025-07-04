﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngameUI : BaseUI
{
    [SerializeField]
    private Image HpImage;
    [SerializeField]
    private TMP_Text GoldText;
    [SerializeField]
    private Button InventoryButton;
    protected override UIState GetUIState()
    {
        return UIState.InGame;
    }

    void Start()
    {
        InventoryButton.onClick.AddListener(() =>
        {
            uiManager.OpenInventoryUI();
            Time.timeScale = 0f;
        });
    }

    void Update()
    {
        Player player = gameContext.player;
        HpImage.fillAmount = (float)player.playerData.hp / player.playerData.maxHp;
        GoldText.SetText($"Gold : {player.playerData.gold}");
    }
}
