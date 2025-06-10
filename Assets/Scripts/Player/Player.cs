using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public EnemyDetector enemyDetector;
    public GameContext gameContext;
    public NavMeshAgent nav;
    public PlayerData playerData;
    public Vector3 lastDest;
    [field: SerializeField] public PlayerAnimationData animationData { get; private set; }
    public Animator animator { get; private set; }
    public CharacterController controller { get; private set; }
    private PlayerStateMachine stateMachine;
    public System.Action onClearCallback;
    public System.Action onFailCallback;

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

    public void initialize(GameContext gameContext, PlayerData playerData)
    {
        this.gameContext = gameContext;
        this.playerData = playerData;
        stateMachine = new PlayerStateMachine(this);
        stateMachine.ChangeState(stateMachine.playerIdleState);
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

    public void SetDestination(Vector3 dest)
    {
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

    public void SetClearHandler(System.Action callback)
    {
        onClearCallback = callback;
    }

    public void SetFailHandler(System.Action callback)
    {
        onFailCallback = callback;
    }
}
