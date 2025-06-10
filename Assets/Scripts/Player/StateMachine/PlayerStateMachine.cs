using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player player { get; }
    public float MovementSpeed { get; private set; }
    public bool IsAttacking { get; set; }

    public IState playerIdleState { get; set; }
    public IState playerWalkState { get; set; }

    public IState playerAttackState { get; set; }


    public Transform MainCameraTransform { get; set; }

    public PlayerStateMachine(Player player)
    {
        this.player = player;

        MainCameraTransform = Camera.main.transform;

        MovementSpeed = player.playerData.speed;

        playerIdleState = new PlayerIdleState(this);
        playerWalkState = new PlayerWalkState(this);
        playerAttackState = new PlayerAttackState(this);
    }
}