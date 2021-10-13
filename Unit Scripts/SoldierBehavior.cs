using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(HealthManager))]
[RequireComponent(typeof(CombatHandler))]
[RequireComponent(typeof(SelectionManager))]
public class SoldierBehavior : UnitAbstract, IUserMovable
{
    [SerializeField] private string Ground = "Ground";
    Outline outline;
    public bool objectSelected;
    Camera cam;
    NavMeshAgent navMeshAgent;
    Animator animator;
    CombatHandler combatHandler;
    SelectionManager selectionManager;
    public Vector3 previousLocationDestination = Vector3.zero;
    public Transform previousObjectDestination;
    public Transform objectDestination { get; set; }
    public Vector3 locationDestination { get; set; }

    StateMachine stateMachine;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        combatHandler = GetComponent<CombatHandler>();
        selectionManager = GetComponent<SelectionManager>();
        navMeshAgent.stoppingDistance = 1f;
        navMeshAgent.speed = movementSpeed;

        stateMachine = new StateMachine();

        Idle idle = new Idle(animator);
        MovingToTarget movingToTarget = new MovingToTarget(navMeshAgent, animator, this);
        MovingToEnemy movingToEnemy = new MovingToEnemy(navMeshAgent, animator, this, combatHandler);
        SoldierAttack soldierAttack = new SoldierAttack(animator, combatHandler);

        AT(idle, movingToTarget, HasTargetDestination());
        AT(idle, movingToEnemy, HasTargetEnemy());
        AT(movingToTarget, idle, HasReachedDestination());
        AT(movingToEnemy, soldierAttack, HasReachedEnemy());
        AT(movingToTarget, movingToEnemy, HasTargetEnemy());
        AT(movingToEnemy, movingToTarget, HasTargetDestination());
        AT(soldierAttack, movingToTarget, HasTargetDestination());
        AT(soldierAttack, idle, EnemyIsDead());


        stateMachine.SetState(idle);

        void AT(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
        Func<bool> HasTargetDestination() => () => locationDestination != Vector3.zero;
        Func<bool> HasTargetEnemy() => () => objectDestination != null && objectDestination.GetComponent<UnitAbstract>() != null;
        Func<bool> HasReachedDestination() => () => locationDestination != Vector3.zero && Vector3.Distance(transform.position, locationDestination) <= navMeshAgent.stoppingDistance;
        Func<bool> HasReachedEnemy() => () => Vector3.Distance(transform.position, objectDestination.position) <= combatHandler.attackRange && combatHandler.attackTarget.gameObject.GetComponent<UnitAbstract>().unitDisposition == Disposition.Hostile;
        Func<bool> EnemyIsDead() => () => combatHandler.attackTarget.GetComponent<HealthManager>().IsAlive() == false;
    }
    void Start()
    {
        cam = Camera.main;
        objectSelected = false;
        outline = transform.GetComponent<Outline>();
        outline.enabled = false;
    }
    private void Update()
    {
        stateMachine.Tick();
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
        while (selectionManager.isSelected == true && GetComponent<HealthManager>().isAlive == true)
        {
            outline.enabled = true;
            RightClick();

            yield return null;

            outline.enabled = false;
        }
    }

    private void RightClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.transform.CompareTag(Ground))
                {
                    previousLocationDestination = locationDestination;
                    locationDestination = hit.point;

                }
                else
                {
                    previousObjectDestination = objectDestination;
                    objectDestination = hit.transform;
                    combatHandler.SetAttackTarget(objectDestination.gameObject);
                }
            }
        }
    }
}
