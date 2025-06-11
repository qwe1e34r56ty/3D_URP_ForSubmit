using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState
{
    StageSelect = 0,
    InGame = 1,
    Total = 2
}

public class UIManager : MonoBehaviour
{
    private GameContext gameContext;
    [SerializeField]
    private IngameUI ingameUI;
    [SerializeField]
    private InventoryUI inventoryUI;
    [SerializeField]
    private StageSelectUI stageSelectUI;

    private BaseUI[] uiArr = new BaseUI[(int)UIState.Total];

    public void Initialize(GameContext gameContext)
    {
        this.gameContext = gameContext;
        uiArr[(int)UIState.StageSelect] = stageSelectUI;
        uiArr[(int)UIState.InGame] = ingameUI;
        foreach (BaseUI ui in uiArr)
        {
            ui.Initialize(gameContext, this);
        }
        OpenBaseUI(UIState.StageSelect);
        inventoryUI.Initialize(gameContext, this);
    }

    public void OpenBaseUI(UIState state)
    {
        foreach (BaseUI ui in uiArr)
        {
            ui.SetActive(state);
        }
    }

    public void OpenInventoryUI()
    {
        inventoryUI.gameObject.SetActive(true);
    }

    public void CloseInventoryUI()
    {
        inventoryUI.gameObject.SetActive(false);
    }
}
