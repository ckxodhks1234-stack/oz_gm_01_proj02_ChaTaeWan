using UnityEngine;

public class SpeedController : MonoBehaviour
{
    private float currentSpeed = 1f;
    private float beforePauseSpeed = 1f;
    private bool isPaused;

    [SerializeField] private GameObject pauseText;
    [SerializeField] private GameObject resumeButton;

    public void SetSpeed(float speed)
    {
        if (isPaused) return;

        currentSpeed = speed;
        Time.timeScale = speed;
    }

    public void Pause()
    {
        if (isPaused) return;

        isPaused = true;
        beforePauseSpeed = currentSpeed;
        Time.timeScale = 0f;

        if (pauseText != null) pauseText.SetActive(true);
        if (resumeButton != null) resumeButton.SetActive(true);
    }

    public void OnClickResume()
    {
        isPaused = false;
        currentSpeed = beforePauseSpeed;
        Time.timeScale = beforePauseSpeed;

        if (pauseText != null) pauseText.SetActive(false);
        if (resumeButton != null) resumeButton.SetActive(false);
    }
}
