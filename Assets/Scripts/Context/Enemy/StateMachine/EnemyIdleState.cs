using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(GameContext gameContext, EnemyStateMachine enemyStateMachine) : base(gameContext, enemyStateMachine)
    {

    }
    public override void Enter()
    {
        base.Enter();
        StartAnimation(enemy.animationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(enemy.animationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();
        if(Vector3.Distance(enemy.gameContext.player.transform.position, enemy.transform.position) < 15.0f)
        {
            enemyStateMachine.ChangeState(enemyStateMachine.enemyWalkState);
        }
    }
}