using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }
    public override void Enter()
    {
        base.Enter();
        StartAnimation(player.animationData.WalkParameterHash);
        if (player.nav == null)
        {
            return;
        }
        player.nav.enabled = true;
        player.SetDestination(new Vector3(player.gameContext.stage.dest.x, 0, player.gameContext.stage.dest.y));
    }

    public override void Exit()
    {
        base.Exit();
        StartAnimation(player.animationData.WalkParameterHash);
        if (player.nav == null)
        {
            return;
        }
        player.nav.enabled = false;
    }

    public override void Update()
    {
        base.Update();
        if (player.nav == null)
        {
            return;
        }
        if (Time.timeScale == 0f)
        {
            if (player.nav.enabled)
            {
                player.nav.enabled = false;
            }
        }
        else
        {
            if (!player.nav.enabled)
            {
                player.nav.enabled = true;
                player.SetDestination(player.lastDest);
            }
            if (player.nav != null)
            {
                if (Vector3.Distance(player.transform.position, player.lastDest) <= 1.5f)
                {
                    player.onClearCallback?.Invoke();
                    playerStateMachine.ChangeState(playerStateMachine.playerIdleState);
                }
                if (player.enemyDetector.detectedEnemies.Count > 0)
                {
                    player.nav.enabled = false;
                    playerStateMachine.ChangeState(playerStateMachine.playerAttackState);
                    return;
                }
            }
        }
    }
}