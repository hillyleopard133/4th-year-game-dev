using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable]
[NodeDescription(name: "Attack", story: "Attack player", category: "Action", id: "6e3791ab1dcb8748a905e25993f735f8")]
public partial class AttackAction : Action
{
    private ActionAttack attack;
    private EnemyBrain brain;
    private BehaviorGraphAgent agent;
    private BlackboardReference blackboard;

    private float attackRange;

    protected override Status OnStart()
    {
        attack = GameObject.GetComponent<ActionAttack>();
        brain = GameObject.GetComponent<EnemyBrain>();
        agent = GameObject.GetComponent<BehaviorGraphAgent>();
        
        blackboard = agent.BlackboardReference;
        blackboard.GetVariableValue("AttackRange", out attackRange);

        brain.CurrentAction = "Attack";
        attack.Act();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        float distance;
        blackboard.GetVariableValue("Distance", out distance);

        if (distance > attackRange)
        {
            return Status.Failure;
        }
        
        if (attack != null && attack.IsFinished)
            return Status.Success;

        return Status.Running;
    }

    protected override void OnEnd()
    {
        if (attack != null)
            attack.StopAttack();
    }
}