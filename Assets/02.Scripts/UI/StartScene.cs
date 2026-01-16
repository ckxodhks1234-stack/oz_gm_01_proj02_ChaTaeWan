using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    [SerializeField] private AudioClip startSceneBGM;
    [SerializeField] private string sampleSceneName = "SampleScene";
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
}
