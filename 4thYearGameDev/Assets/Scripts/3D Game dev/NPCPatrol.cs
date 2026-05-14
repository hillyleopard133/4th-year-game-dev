using UnityEngine;
using UnityEngine.AI;

public class NPCPatrol : MonoBehaviour
{
    public Transform[] points;
    private NavMeshAgent agent;
    private int index;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GoNextPoint();
    }

    void Update()
    {
        if (agent.pathPending) return;

        if (agent.remainingDistance < 0.5f)
        {
            GoNextPoint();
        }
    }

    void GoNextPoint()
    {
        if (points.Length == 0) return;

        agent.destination = points[index].position;
        index = (index + 1) % points.Length;
    }
}