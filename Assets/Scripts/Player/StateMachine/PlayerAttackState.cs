using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }
    public override void Enter()
    {
        base.Enter();
        StartAnimation(player.animationData.AttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(player.animationData.AttackParameterHash);
    }

    public override void Update()
    {
        base.Update();
        if (player.enemyDetector.detectedEnemies.Count == 0)
        {
            playerStateMachine.ChangeState(playerStateMachine.playerIdleState);
            return;
        }

        Enemy target = null;
        float minDistance = float.MaxValue;
        Vector3 playerPos = player.transform.position;

        foreach (var enemy in player.enemyDetector.detectedEnemies)
        {
            if (enemy == null) continue;

            float distance = Vector3.Distance(playerPos, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                target = enemy;
            }
        }

        if (target != null)
        {
            Vector3 direction = target.transform.position - playerPos;
            direction.y = 0f;
            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
    }
}