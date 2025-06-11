using UnityEngine;

public class EnemyBaseState : IState
{
    public EnemyStateMachine enemyStateMachine {  get; private set; }
    public Enemy enemy;
    protected GameContext gameContext;
    public EnemyBaseState(GameContext gameContext, EnemyStateMachine stateMachine)
    {
        this.gameContext = gameContext;
        this.enemyStateMachine = stateMachine;
        this.enemy = stateMachine.enemy;
    }
    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void Update()
    {
        if(enemy.enemyData.hp <= 0)
        {
            enemy.isDie = true;
            GameObject.Destroy(enemy.gameObject);
            enemy.gameContext.enemies.Remove(enemy);
        }
    }

    protected void StartAnimation(int animationHash)
    {
        enemy.animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        enemy.animator.SetBool(animationHash, false);
    }
}