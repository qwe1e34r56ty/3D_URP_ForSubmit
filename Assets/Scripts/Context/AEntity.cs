using UnityEngine.AI;
using UnityEngine;
using Unity.VisualScripting;
using System.Collections;

public abstract class AEntity : MonoBehaviour
{
    public GameContext gameContext;
    public NavMeshAgent nav { get; private set; }
    public Vector3 lastDest;
    public bool isDie = false;
    [field: SerializeField] public PlayerAnimationData animationData { get; private set; }
    public Animator animator { get; private set; }
    public CharacterController controller { get; private set; }

    protected virtual void Awake()
    {
        animationData = new PlayerAnimationData();
        animationData.Initialize();
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
    }

    public abstract void Initialize(GameContext gameContext, AEntityData entityData);

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

    protected IEnumerator WaitForNavMeshAndSetDestination(Vector3 destination)
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