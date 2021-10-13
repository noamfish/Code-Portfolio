using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(HealthManager))]
[RequireComponent(typeof(CombatHandler))]
[RequireComponent(typeof(SelectionManager))]
public class GathererBehavior : UnitAbstract, IUserMovable
{
    [SerializeField] private string Ground = "Ground";
    Camera cam;
    NavMeshAgent navMeshAgent;
    Animator animator;
    Outline outline;
    SelectionManager selectionManager;
    public bool objectSelected;
    public Vector3 locationDestination { get; set; }
    public Transform objectDestination { get; set; }
    public Vector3 previousRCPosition = Vector3.zero;

    public Transform previousRCTransform;
    Transform ax;
    Transform pickax;
    public ResourceAbstract currentResource;
    public ResourceAbstract previousResource;
    public Rock rock;
    private int chopRange = 4;
    private int pickRange = 5;
    bool isAlive = true;

    private StateMachine stateMachine;

    private void Awake()
    {
        FindAxAndPick();
        pickax.gameObject.SetActive(false);
        ax.gameObject.SetActive(true);
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        selectionManager = GetComponent<SelectionManager>();
        navMeshAgent.stoppingDistance = 1f;
        navMeshAgent.speed = movementSpeed;

        stateMachine = new StateMachine();
        
        Idle idle = new Idle(animator);
        MovingToTarget movingToTarget = new MovingToTarget(navMeshAgent, animator, this);
        MovingToObject movingToObject = new MovingToObject(navMeshAgent, animator, this);
        ChoppingWood choppingWood = new ChoppingWood(animator, transform, this, ax, pickax);
        Picking picking = new Picking(animator, this, pickax, ax);

        AT(idle, movingToTarget, HasTargetDestination());
        AT(idle, movingToObject, HasTargetObject());
        AT(movingToTarget, idle, HasReachedDestination());
        AT(movingToObject, choppingWood, HasReachedTree());
        AT(movingToObject, picking, HasReachedRock());
        AT(movingToObject, movingToTarget, HasTargetDestination());
        AT(movingToTarget, movingToObject, HasTargetObject());
        AT(choppingWood, movingToTarget, HasTargetDestination());
        AT(choppingWood, movingToObject, HasTargetObject());
        AT(picking, movingToTarget, HasTargetDestination());
        AT(picking, movingToObject, HasTargetObject());


        stateMachine.SetState(idle);
        
        void AT(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
        void AAT(IState state, Func<bool> condition) => stateMachine.AddAnyTransition(state, condition);
        Func<bool> HasTargetDestination() => () => locationDestination != Vector3.zero;
        Func<bool> HasTargetObject() => () => objectDestination != null;
        Func<bool> HasReachedDestination() => () => locationDestination != Vector3.zero && Vector3.Distance(transform.position, locationDestination) <=  navMeshAgent.stoppingDistance;
        Func<bool> HasReachedTree() => () => Vector3.Distance(transform.position, objectDestination.position) <= chopRange && currentResource.GetType() == typeof(Tree);
        Func<bool> HasReachedRock() => () => Vector3.Distance(transform.position, objectDestination.position) <= pickRange && currentResource.GetType() == typeof(Rock);

        objectSelected = false;
        outline = transform.GetComponent<Outline>();
        outline.enabled = false;
        cam = Camera.main;
        
    }

    private void Update()
    {
        stateMachine.Tick();
    }

    private void FindAxAndPick()
    {
        ax = transform.GetChild(7).GetChild(2).GetChild(0).GetChild(0);
        pickax = transform.GetChild(7).GetChild(2).GetChild(0).GetChild(2);
    }
    public void ClearLocationDestination()
    {
        locationDestination = Vector3.zero;
    }

    public void ClearObjectDestination() 
    { 
        objectDestination = null;
    }

    public void CallObjectSelected()
    {
        objectSelected = true;

        StartCoroutine(ObjectSelected());
    }

    IEnumerator ObjectSelected()
    {
        while (selectionManager.isSelected == true)
        {
            outline.enabled = true;
            RightClick();

            yield return null;

            outline.enabled = false;
        }
    }

    public void RightClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.transform.CompareTag(Ground))
                {
                    previousRCPosition = locationDestination;
                    locationDestination = hit.point;
                    previousResource = currentResource;
                }
                else if (hit.transform.GetComponent<ResourceAbstract>() != null)
                {
                    previousRCTransform = objectDestination;
                    objectDestination = hit.transform;
                    previousResource = currentResource;
                    currentResource = objectDestination.GetComponent<ResourceAbstract>();
                }
            }
        }
    }
}
