using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameContext gameContext;
    public NavMeshAgent nav;
    public EnemyData enemyData;
    public Vector3 lastDest;
    public bool isDie = false;
    [field: SerializeField] public PlayerAnimationData animationData { get; private set; }
    public Animator animator { get; private set; }
    public CharacterController controller { get; private set; }
    private EnemyStateMachine stateMachine;

    private void Awake()
    {
        animationData = new PlayerAnimationData();
        animationData.Initialize();
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        stateMachine?.Update();
    }

    public void initialize(GameContext gameContext, EnemyData enemyData)
    {
        this.gameContext = gameContext;
        this.enemyData = enemyData;
        stateMachine = new EnemyStateMachine(this);
        stateMachine.ChangeState(stateMachine.enemyIdleState);
    }

    public void SetNavAgent()
    {
        if (nav == null)
        {
            nav = this.AddComponent<NavMeshAgent>();

            int walkableArea = NavMesh.GetAreaFromName("Walkable");
            if (walkableArea != -1)
            {
                nav.areaMask = 1 << walkableArea;
            }
        }
    }

    public void SetDestination()
    {
        Vector3 dest = gameContext.player.transform.position;
        StartCoroutine(WaitForNavMeshAndSetDestination(dest));
    }

    private IEnumerator WaitForNavMeshAndSetDestination(Vector3 destination)
    {
        while (!nav.isOnNavMesh || !nav.isActiveAndEnabled)
        {
            yield return null;
        }
        nav.SetDestination(destination);
        lastDest = destination;
    }

    public void Teleport(Vector3 dest)
    {
        transform.position = dest;
    }
}