using UnityEngine;

public enum AudioZoneType
{
    On,
    Off
}
public class AudioZone : MonoBehaviour
{
    [SerializeField] private AudioZoneType type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<SpeedUnitController>() == null) return;

        if (BGMController.Instance == null) return;

        switch (type)
        {
            case AudioZoneType.On:
                BGMController.Instance.SetMute(false);
                break;

            case AudioZoneType.Off:
                BGMController.Instance.SetMute(true);
                break;
        }
    }
}
