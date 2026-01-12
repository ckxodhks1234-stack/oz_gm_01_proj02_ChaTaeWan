using UnityEngine;

public class SpeedPlane : MonoBehaviour
{
    [SerializeField] private float timeScale = 1.0f;
    [SerializeField] private SpeedController speedController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<SpeedUnitController>() != null)
        {
            speedController.SetSpeed(timeScale);
        }
    }
}
