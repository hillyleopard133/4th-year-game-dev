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

    protected override Status OnStart()
    {
        chase = GameObject.GetComponent<ActionChase>();
        brain = GameObject.GetComponent<EnemyBrain>();

        brain.CurrentAction = "Chase";
        chase.Act(); 
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Running;
    }

    protected override void OnEnd()
    {
        if (chase != null)
            chase.StopMoving();
    }
}