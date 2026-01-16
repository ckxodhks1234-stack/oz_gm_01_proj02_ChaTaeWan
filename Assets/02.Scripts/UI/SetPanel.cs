using UnityEngine;
using UnityEngine.SceneManagement;

public class SetPanel : MonoBehaviour
{
    [SerializeField] private GameObject setPanel;
    [SerializeField] private string startSceneName = "StartScene";
    [SerializeField] private string sampleSceneName = "SampleScene";

    void Start()
    {
        setPanel.SetActive(false);
    }

    public void Open()
    {
        setPanel.SetActive(true);
    }

    public void Close()
    {
        setPanel.SetActive(false);
    }

    public void GoMain()
    {
        SceneManager.LoadScene(startSceneName);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(sampleSceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ExitSetPanel()
    {
        setPanel.SetActive(false);
    }
}
