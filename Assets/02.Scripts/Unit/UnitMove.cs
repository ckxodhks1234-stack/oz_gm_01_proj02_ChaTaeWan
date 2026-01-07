using UnityEngine;
using UnityEngine.AI;

public class UnitMove : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject selectCircle;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        Selected(false);
    }

    public void Selected(bool isSelected)
    {
        if (selectCircle != null)
        {
            selectCircle.SetActive(isSelected);
        }
    }

    public void MoveTo(Vector3 targetPos)
    {
        if (agent != null)
        {
            agent.SetDestination(targetPos);
        }
    }
}
