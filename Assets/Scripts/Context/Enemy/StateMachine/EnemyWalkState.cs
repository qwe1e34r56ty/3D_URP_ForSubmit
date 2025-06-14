﻿using UnityEngine;

public class EnemyWalkState : EnemyBaseState
{
    public EnemyWalkState(GameContext gameContext, EnemyStateMachine enemyStateMachine) : base(gameContext, enemyStateMachine)
    {

    }
    public override void Enter()
    {
        base.Enter();
        StartAnimation(enemy.animationData.WalkParameterHash);
        if (enemy.nav == null)
        {
            return;
        }
        enemy.nav.enabled = true;
        enemy.SetDestination(gameContext.player.transform.position);
    }

    public override void Exit()
    {
        base.Exit();
        StartAnimation(enemy.animationData.WalkParameterHash);
        if (enemy.nav == null)
        {
            return;
        }
        enemy.nav.enabled = false;
    }

    public override void Update()
    {
        base.Update();
        if (enemy.nav == null)
        {
            return;
        }
        else
        {
            enemy.SetDestination(gameContext.player.transform.position);
        }
        if (Time.timeScale == 0f)
        {
            if (enemy.nav.enabled)
            {
                enemy.nav.enabled = false;
            }
        }
        else
        {
            if (!enemy.nav.enabled)
            {
                enemy.nav.enabled = true;
            }
        }
    }
}