using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private string sampleSceneName = "SampleScene";
    [SerializeField] private string startSceneName = "StartScene";

    private const string HIGHEST_WAVE_KEY = "HIGHESTWAVERECORD";
    
    public void Show(int wave)
    {
        gameOverPanel.SetActive(true);
        int bestWave = PlayerPrefs.GetInt(HIGHEST_WAVE_KEY, 0);

        //최고 기록 갱신
        if (wave > bestWave)
        {
            bestWave = wave;
            PlayerPrefs.SetInt(HIGHEST_WAVE_KEY, bestWave);
            PlayerPrefs.Save();
        }

        waveText.text = $"최고: {bestWave}Wave / 현재: {wave}Wave";
    }

    public void OnClickRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sampleSceneName);
    }

    public void OnClickMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(startSceneName);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
