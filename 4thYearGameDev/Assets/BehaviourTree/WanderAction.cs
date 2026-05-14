using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable]
[NodeDescription(name: "Wander", story: "Agent wanders", category: "Action")]
public partial class WanderAction : Action
{
    private ActionWander wander;
    
    private BehaviorGraphAgent agent;
    private BlackboardReference blackboard;
    private EnemyBrain brain;

    private float detectionRange;

    protected override Status OnStart()
    {
        agent = GameObject.GetComponent<BehaviorGraphAgent>();
        wander = GameObject.GetComponent<ActionWander>();
        brain = GameObject.GetComponent<EnemyBrain>();
        
        blackboard = agent.BlackboardReference;
        blackboard.GetVariableValue("DetectionRange", out detectionRange);

        brain.CurrentAction = "Wander";
        wander.Act();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        float distance;
        blackboard.GetVariableValue("Distance", out distance);

        if (distance < detectionRange)
        {
            return Status.Failure;
        }

        return Status.Running; 
    }

    protected override void OnEnd()
    {
        wander.StopMoving();
    }
}