using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    [SerializeField] private AudioClip startSceneBGM;
    [SerializeField] private string sampleSceneName = "SampleScene";
    [SerializeField] private GameObject descPanel;
    void Start()
    {
        BGMController.Instance.PlayBGM(startSceneBGM);
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene(sampleSceneName);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }

    public void OnClickDescPanel()
    {
        descPanel.SetActive(true);
    }

    public void OnClickQuitDesc()
    {
        descPanel.SetActive(false);
    }
}
