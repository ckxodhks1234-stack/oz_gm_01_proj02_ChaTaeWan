using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitSelectSlot : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private Image frameImage;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private Button button;

    private UnitData unitData;
    private SynthesisController synthesisController;

    public void Init(UnitData data, SynthesisController controller, int count)
    {
        unitData = data;
        synthesisController = controller;

        iconImage.sprite = data.icon;
        iconImage.color = Color.white;

        frameImage.sprite = data.frameSprite;
        frameImage.color = data.frameColor;
        frameImage.enabled = true;

        if (countText.text == null) Debug.Log("카운트 텍스트 없음");
        countText.gameObject.SetActive(true);
        countText.text = count.ToString();

        button.interactable = count > 0; //버튼 0보다 커야 활성화
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);

        gameObject.SetActive(true);
    }

    public void Clear()
    {
        unitData = null;

        iconImage.sprite = null;
        iconImage.color = new Color(1, 1, 1, 0);

        frameImage.sprite = null;
        frameImage.enabled = false;

        countText.text = "";
        countText.gameObject.SetActive(false);

        button.interactable = false;
        button.onClick.RemoveAllListeners();

        gameObject.SetActive(false);
    }

    public void OnClick()
    {
        Debug.Log("슬롯 클릭됨");

        if (unitData != null)
        {
            Debug.Log($"선택된 유닛: {unitData.name}");
            synthesisController.AddUnit(unitData);
        }
    }
}
