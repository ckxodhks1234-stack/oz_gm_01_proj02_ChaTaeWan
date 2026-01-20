using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitInfoPanel : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private Image frameImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI gradeText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI rangeText;

    void Start()
    {
        Hide();
    }

    public void Show(UnitData data)
    {
        if (data == null) return;

        iconImage.sprite = data.icon;
        frameImage.sprite = data.frameSprite;
        frameImage.color = data.frameColor;
        nameText.text = data.unitName;
        gradeText.text = $"등급 {data.unitGrade}";
        damageText.text = $"데미지 {data.attackDamage}";
        rangeText.text = $"사거리 {data.attackRange}";

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
