using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectUI : BaseUI
{
    [SerializeField]
    private Transform content;
    [SerializeField]
    private Button buttonPrefab;
    private Dictionary<Button, int> stageSelectButtons = new Dictionary<Button, int>(); 
    protected override UIState GetUIState()
    {
        return UIState.StageSelect;
    }

    public override void Initialize(GameContext gameContext, UIManager uiManager)
    {
        base.Initialize(gameContext, uiManager);
        for (int i = 0; i < gameContext.stageDataList.Count; i++)
        {
            Button stageSelectButton = Instantiate(buttonPrefab, content);
            stageSelectButtons.Add(stageSelectButton, i);
            if (stageSelectButton.TryGetComponent<StageSelectButton>(out StageSelectButton _stageSelectButton))
            {
                _stageSelectButton.Initialize(gameContext, gameContext.stageDataList[i]);
            }
            int stageIndex = i;
            stageSelectButton.onClick.AddListener(() =>
            {
                EnterStage(stageIndex);
                uiManager.OpenBaseUI(UIState.InGame);
            });
        }
    }

    public void EnterStage(int stageIndex)
    {
        if (stageIndex < 0 || stageIndex >= gameContext.stageDataList.Count)
        {
            Logger.LogWarning($"[GameManager] Enter Stage Index out of bound : {stageIndex}");
            return;
        }
        Stage stage = gameContext.stageFactory.BuildStage(gameContext, gameContext.stageDataList[stageIndex]);
        gameContext.stage = stage;

        Player player;
        if (gameContext.player == null)
        {
            player = gameContext.playerFactory.BuildPlayer(gameContext, gameContext.playerData);
            gameContext.player = player;
        }
        else
        {
            player = gameContext.player;
            player.gameObject.SetActive(true);
        }
        player.Teleport(new Vector3(stage.start.x, 0, stage.start.y));
        player.SetNavAgent();

        player.SetClearHandler(() =>
        {
            ClearStage();
        });
        player.SetFailHandler(() =>
        {
            ClearStage();
        });
        void ClearStage()
        {
            uiManager.OpenBaseUI(UIState.StageSelect);
            GameObject.Destroy(stage.gameObject);
            player.gameObject.SetActive(false);
            foreach (Enemy enemy in gameContext.enemies)
            {
                if (enemy != null)
                {
                    Destroy(enemy.gameObject);
                }
            }
            gameContext.enemies.Clear();
        }
    }
}
