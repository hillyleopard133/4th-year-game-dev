using Unity.Behavior;
using UnityEngine;

public enum EnemyMovementType
{
        Wander,
        Patrol
}
public class EnemyBrain : MonoBehaviour
{
        [HideInInspector] public Rigidbody2D rb;
        [HideInInspector] public EnemyAnimations animations;
        [HideInInspector] public int updateFrameNumber = 1;
        [HideInInspector] public bool isAlive = true;
        
        public Transform Player {get; set;}
        
        private BehaviorGraphAgent agent;
        public string CurrentAction { get; set; }

        private void Awake()
        {
                rb = GetComponent<Rigidbody2D>();
                animations = GetComponent<EnemyAnimations>();
                agent = GetComponent<BehaviorGraphAgent>();
        }

        private void Start()
        {
                Player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
                SetPlayerDistance();
        }

        private void SetPlayerDistance()
        {
                float distance = Vector3.Distance(transform.position, Player.position);
                BlackboardReference blackboard = agent.BlackboardReference;
                blackboard.SetVariableValue("Distance", distance);
        }
        
}
