using UnityEngine;
using UnityEngine.UI;

public class PauseZone : MonoBehaviour
{
    [SerializeField] private SpeedController speedController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<SpeedUnitController>() == null) return;

        speedController.Pause();
    }
}
