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

    protected override Status OnStart()
    {
        attack = GameObject.GetComponent<ActionAttack>();
        brain = GameObject.GetComponent<EnemyBrain>();

        brain.CurrentAction = "Attack";
        attack.Act();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
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