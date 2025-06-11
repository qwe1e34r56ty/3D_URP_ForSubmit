using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    protected GameContext gameContext;
    protected UIManager uiManager;

    public virtual void Initialize(GameContext gameContext, UIManager uiManager)
    {
        this.gameContext = gameContext;
        this.uiManager = uiManager;
    }

    protected abstract UIState GetUIState();
    public void SetActive(UIState state)
    {
        gameObject.SetActive(GetUIState() == state);
    }
}