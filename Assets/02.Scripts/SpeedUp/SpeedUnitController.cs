using UnityEngine;
using UnityEngine.AI;

public class SpeedUnitController : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            MoveSpeedUnit();
        }
    }

    private void MoveSpeedUnit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            agent.SetDestination(hit.point);
        }
    }
}
