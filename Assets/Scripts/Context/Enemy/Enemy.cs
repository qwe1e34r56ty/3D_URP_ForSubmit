using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : AEntity
{
    public EnemyData enemyData;
    private EnemyStateMachine stateMachine;

    public override void Initialize(GameContext gameContext, AEntityData enemyData)
    {
        this.gameContext = gameContext;
        this.enemyData = (EnemyData)enemyData;
        stateMachine = new EnemyStateMachine(gameContext, this);
        stateMachine.ChangeState(stateMachine.enemyIdleState);
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