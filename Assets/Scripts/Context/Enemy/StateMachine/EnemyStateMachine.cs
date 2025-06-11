using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public Enemy enemy { get; }
    public bool IsAttacking { get; set; }

    public IState enemyIdleState { get; set; }
    public IState enemyWalkState { get; set; }

    public EnemyStateMachine(GameContext gameContext, Enemy enemy)
    {
        this.enemy = enemy;

        enemyIdleState = new EnemyIdleState(gameContext, this);
        enemyWalkState = new EnemyWalkState(gameContext, this);
    }
}