using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable]
[NodeDescription(name: "Chase", story: "Chase player", category: "Action", id: "eb27d86379bbd98a047bacab96a9498d")]
public partial class ChaseAction : Action
{
    private ActionChase chase;
    private EnemyBrain brain;
    private BehaviorGraphAgent agent;
    private BlackboardReference blackboard;

    private float detectionRange;
    private float attackRange;
    
    protected override Status OnStart()
    {
        chase = GameObject.GetComponent<ActionChase>();
        brain = GameObject.GetComponent<EnemyBrain>();
        agent = GameObject.GetComponent<BehaviorGraphAgent>();
        
        blackboard = agent.BlackboardReference;
        blackboard.GetVariableValue("DetectionRange", out detectionRange);
        blackboard.GetVariableValue("AttackRange", out attackRange);

        brain.CurrentAction = "Chase";
        chase.Act(); 
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        float distance;
        blackboard.GetVariableValue("Distance", out distance);

        if (distance > detectionRange || distance < attackRange)
        {
            return Status.Failure;
        }
        
        return Status.Running;
    }

    protected override void OnEnd()
    {
        if (chase != null)
            chase.StopMoving();
    }
}