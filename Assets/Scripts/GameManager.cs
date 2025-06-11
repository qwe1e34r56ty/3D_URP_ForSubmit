using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameContext gameContext;
    [SerializeField]
    private string stageDatasDir;
    [SerializeField]
    private string playerDataPath;
    [SerializeField]
    public UIManager uiManager;

    private void Awake()
    {
        gameContext = new GameContext(stageDatasDir,
                playerDataPath
            );
        uiManager.Initialize(gameContext);
    }

    public void Update()
    {

    }

}