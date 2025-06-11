using TMPro;
using UnityEngine;

public class StageSelectButton : MonoBehaviour
{
    private GameContext gameContext;
    [SerializeField]
    private TMP_Text buttonText;
    public void Initialize(GameContext gameContext, StageData stageData)
    {
        this.gameContext = gameContext;
        buttonText?.SetText(stageData.name);
    }
}