using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearUI : MonoBehaviour
{
    [SerializeField] private GameObject gameClearPanel;
    [SerializeField] private string sampleSceneName = "SampleScene";
    [SerializeField] private string startSceneName = "StartScene";

    public void Show(int wave)
    {
        gameClearPanel.SetActive(true);
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
