using TMPro;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DebugOverlay : MonoBehaviour
{
    public TextMeshProUGUI debugText;
    public EnemyBrain enemy; 
    private BehaviorGraphAgent agent;
    private BlackboardReference blackboard;
    
    float distance;

    private void Start()
    {
        agent = enemy.GetComponent<BehaviorGraphAgent>();
        blackboard = agent.BlackboardReference;
    }
    
    private void Update()
    {
        string fsmState = enemy.CurrentAction;
        string targetName = enemy.Player != null ? enemy.Player.name : "None";

        string targetPos = "";
        if (enemy.Player != null)
        {
            targetPos = enemy.Player.position.ToString("F1");
        }
        else
        {
            Vector3? movementGoal = null;

            if (fsmState == "Patrol")
            {
                var patrol = enemy.GetComponent<ActionPatrol>();
                if (patrol != null && patrol.movementSteps != null && patrol.movementSteps.Count > 0)
                    movementGoal = patrol.movementSteps.Peek();
            }
            else if (fsmState == "Wander")
            {
                var wander = enemy.GetComponent<ActionWander>();
                if (wander != null && wander.movementSteps != null && wander.movementSteps.Count > 0)
                    movementGoal = wander.movementSteps.Peek();
            }

            targetPos = movementGoal.HasValue ? movementGoal.Value.ToString("F1") : "None";
        }
        
        blackboard.GetVariableValue("Distance", out distance);
        
        debugText.text = enemy.name + "\nFSM State: " + fsmState + "\nCurrent Target: " + targetName + "\nTarget Pos: " + 
                         targetPos + "\nTarget Distance: " + distance + "\nAlive: " + enemy.isAlive;
    }

}