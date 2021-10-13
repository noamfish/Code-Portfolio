using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(HealthManager))]
[RequireComponent(typeof(CombatHandler))]

public class GhoulBehavior : UnitAbstract, IItemDroppable
{
    public Animator animator;
    NavMeshAgent navMeshAgent;
    CombatHandler combatHandler;
    HealthManager healthManager;
    UnitMover unitMover;

    
    public GameObject droppableItem_1;


    private StateMachine stateMachine;
    private void Awake()
    {

        navMeshAgent = GetComponent<NavMeshAgent>();
        healthManager = GetComponent<HealthManager>();
        combatHandler = GetComponent<CombatHandler>();
        unitMover = GetComponent<UnitMover>();

        navMeshAgent.speed = movementSpeed;
        stateMachine = new StateMachine();
        

        SearchForWanderDest searchWanderDest = new SearchForWanderDest(unitMover, combatHandler);
        MoveToLocation moveToLocation = new MoveToLocation(navMeshAgent, animator, unitMover);
        MoveToAttack moveToAttack = new MoveToAttack(navMeshAgent, animator, combatHandler, unitMover);
        Attack attack = new Attack(animator, combatHandler);
        Dead dead = new Dead();

        AT(searchWanderDest, moveToLocation, HasTarget());
        AT(moveToLocation, searchWanderDest, TargetReached());
        AT(moveToLocation, searchWanderDest, StuckForOverASecond());
        AT(moveToLocation, moveToAttack, HasAttackTarget());
        AT(moveToAttack, attack, AttackTargetReached());
        AT(moveToAttack, searchWanderDest, HasLeftDisengageRange());
        AT(attack, moveToAttack, HasLeftAttackRange());
        AT(attack, dead, IsDead());


        stateMachine.SetState(searchWanderDest);

        void AT(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
        Func<bool> HasTarget() => () => unitMover.targetDestination != null;
        Func<bool> TargetReached() => () => unitMover.targetDestination != null && Vector3.Distance(transform.position, unitMover.targetDestination) < 1f;
        Func<bool> StuckForOverASecond() => () => moveToLocation.TimeStuck > 2f;
        Func<bool> HasAttackTarget() => () => combatHandler.attackTarget != null;
        Func<bool> AttackTargetReached() => () => combatHandler.attackTarget != null && Vector3.Distance(transform.position, combatHandler.attackTarget.transform.position) < combatHandler.attackRange;
        Func<bool> HasLeftAttackRange() => () => Vector3.Distance(transform.position, combatHandler.attackTarget.transform.position) > combatHandler.attackRange;
        Func<bool> HasLeftDisengageRange() => () => Vector3.Distance(unitMover.startPosition, transform.position) > combatHandler.disengageRange;
        Func<bool> IsDead() => () => healthManager.isAlive == false;
    }

    private void Update()
    {
        stateMachine.Tick();
    }
    public void DropItems()
    {
        Instantiate(droppableItem_1, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), Quaternion.identity);
    }
}
