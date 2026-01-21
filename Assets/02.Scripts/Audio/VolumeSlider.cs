using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        if (BGMController.Instance == null) return;

        //초기값 동기화
        volumeSlider.value = BGMController.Instance.GetVolume();

        //볼륨 변경 이벤트 연결
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    private void OnDestroy()
    {
        volumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);
    }

    private void OnVolumeChanged(float value)
    {
        if (BGMController.Instance == null) return;

        BGMController.Instance.SetVolume(value);
    }
}
