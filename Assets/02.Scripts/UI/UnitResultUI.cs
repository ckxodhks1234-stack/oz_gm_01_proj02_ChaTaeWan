using UnityEngine;
using TMPro;
using System.Collections;

public class UnitResultUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float lifeTime = 3f;

    //뽑기결과
    public void Show(UnitData data, string description, float unitChance)
    {
        resultText.text = $"{description} : {data.unitName}({unitChance}%)";
        canvasGroup.alpha = 1f;

        StartCoroutine(FadeOut());
    }

    //합성결과
    public void ShowSynthesis(string description)
    {
        resultText.text = description;
        canvasGroup.alpha = 1f;

        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float elapsed = 0f;

        //라이프 타임동안 알파값 서서히 줄어들기
        while(elapsed < lifeTime)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = 1f - (elapsed / lifeTime);
            yield return null;
        }
        //파괴되면 아래 줄이 위로 올라감
        Destroy(gameObject);
    }
}
