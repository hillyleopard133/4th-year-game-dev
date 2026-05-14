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

    protected override Status OnStart()
    {
        agent = GameObject.GetComponent<BehaviorGraphAgent>();
        wander = agent.GetComponent<ActionWander>();
        brain = agent.GetComponent<EnemyBrain>();
        blackboard = agent.BlackboardReference;
        blackboard.SetVariableValue("Self", agent.gameObject);
        blackboard.SetVariableValue("Target", agent.gameObject);
        wander.Act();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Running; 
    }

    protected override void OnEnd()
    {
        blackboard.SetVariableValue("Target", brain.Player.gameObject);
        wander.StopMoving();
    }
}