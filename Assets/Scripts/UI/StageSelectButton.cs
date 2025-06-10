using TMPro;
using UnityEngine;

public class StageSelectButton : MonoBehaviour
{
    [SerializeField]
    private TMP_Text buttonText;
    public void Initialize(StageData stageData)
    {
        buttonText?.SetText(stageData.name);
    }
}