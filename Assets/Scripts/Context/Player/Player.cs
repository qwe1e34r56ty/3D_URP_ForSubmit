using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Player : AEntity
{
    public EnemyDetector enemyDetector;
    public PlayerData playerData;
    private PlayerStateMachine stateMachine;
    public System.Action onClearCallback;
    public System.Action onFailCallback;

    public override void Initialize(GameContext gameContext, AEntityData playerData)
    {
        this.gameContext = gameContext;
        this.playerData = (PlayerData)playerData;
        stateMachine = new PlayerStateMachine(this);
        stateMachine.ChangeState(stateMachine.playerIdleState);
    }

    public void SetClearHandler(System.Action callback)
    {
        onClearCallback = callback;
    }

    public void SetFailHandler(System.Action callback)
    {
        onFailCallback = callback;
    }

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        stateMachine?.Update();
    }
}
