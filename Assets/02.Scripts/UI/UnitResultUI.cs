using UnityEngine;
using TMPro;
using System.Collections;

public class UnitResultUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float lifeTime = 3f;

    [SerializeField] private float punchScale = 1.2f;
    [SerializeField] private float punchDuration = 0.15f;

    //뽑기결과
    public void Show(UnitData data, string description, float unitChance)
    {
        Color gradeColor = GradeColor(data.unitGrade);
        string colorHex = ColorUtility.ToHtmlStringRGB(gradeColor);

        resultText.text = $"<color=#{colorHex}>{description} : {data.unitName} ({unitChance:F1}%)</color>";

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

    private Color GradeColor(UnitGrade grade)
    {
        switch (grade)
        {
            case UnitGrade.Unique:
                return Color.yellow;

            case UnitGrade.Legend:
                return new Color(0.6f, 1f, 0.3f); //연두색

            case UnitGrade.Chowall:
                return new Color(0.4f, 0.8f, 1f); //하늘색

            case UnitGrade.TaeCho:
                return Color.red;

            default:
                return Color.white;
        }
    }
}
