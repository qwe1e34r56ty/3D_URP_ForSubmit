using UnityEngine;

public class PlayerBaseState : IState
{
    public PlayerStateMachine playerStateMachine {  get; private set; }
    public Player player;
    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.playerStateMachine = stateMachine;
        this.player = stateMachine.player;
    }
    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void Update()
    {
    }

    protected void StartAnimation(int animationHash)
    {
        player.animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        player.animator.SetBool(animationHash, false);
    }
}