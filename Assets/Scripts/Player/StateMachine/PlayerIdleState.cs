using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }
    public override void Enter()
    {
        base.Enter();
        StartAnimation(player.animationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(player.animationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();
        if (player.enemyDetector.detectedEnemies.Count > 0)
        {
            playerStateMachine.ChangeState(playerStateMachine.playerAttackState);
            return;
        }
        playerStateMachine.ChangeState(playerStateMachine.playerWalkState);
    }
}