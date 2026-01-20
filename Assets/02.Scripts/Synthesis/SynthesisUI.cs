using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SynthesisUI : MonoBehaviour
{
    [SerializeField] private GameObject synthesisPanel;
    [SerializeField] private UnitSlot[] unitSlots;
    [SerializeField] private Button synthesisButton;

    public void Open()
    {
        synthesisPanel.SetActive(true);
    }

    public void Refresh(List<UnitData> units, int max)
    {
        //슬롯 초기화
        for (int i = 0; i < unitSlots.Length; i++)
        {
            unitSlots[i].Clear();
        }

        // 채워진 개수만큼 아이콘 표시
        for (int i = 0; i < units.Count; i++)
        {
            unitSlots[i].Set(units[i]);
        }

        synthesisButton.interactable = (units.Count >= max);
    }
}
