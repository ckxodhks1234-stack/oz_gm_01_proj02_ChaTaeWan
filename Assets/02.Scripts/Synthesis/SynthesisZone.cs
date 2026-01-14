using UnityEngine;
using UnityEngine.SocialPlatforms;

public class SynthesisZone : MonoBehaviour
{
    [SerializeField] private SynthesisController synthesisController;
    [SerializeField] private GameObject synthesisUIPanel;
    [SerializeField] private GameObject unitSelectPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<SpeedUnitController>() != null)
        {
            synthesisController.Open();
            synthesisUIPanel.SetActive(true);
            unitSelectPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<SpeedUnitController>() != null)
        {
            synthesisController.Cancel();
            synthesisUIPanel.SetActive(false);
            unitSelectPanel.SetActive(false);
        }
    }
}
