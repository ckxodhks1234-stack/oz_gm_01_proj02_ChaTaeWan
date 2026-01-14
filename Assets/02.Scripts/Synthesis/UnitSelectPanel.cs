using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnitSelectPanel : MonoBehaviour
{
    [SerializeField] private UnitManager unitManager;
    [SerializeField] private SynthesisController synthesisController;
    [SerializeField] private UnitSelectSlot[] slots;

    [SerializeField] private UnitGrade maxGrade = UnitGrade.WuKong;

    private void OnEnable()
    {
        //유닛매니저에서 유닛변화생기면 최신화
        unitManager.UnitChanged += Refresh;
        //패널이 열릴 때 최신화
        Refresh();
    }

    private void OnDisable()
    {
        unitManager.UnitChanged -= Refresh;
    }

    public void Refresh()
    {
        Debug.Log("UnitSelectPanel Refresh 호출됨");
        //모든 슬롯 비우기
        foreach (var slot in slots) slot.Clear();

        //등급별 보유 수
        Dictionary<UnitData, int> unitCount = new Dictionary<UnitData, int>();

        foreach(var unit in unitManager.GetUnits())
        {
            if (unit == null || unit.UnitData == null)
            {
                Debug.LogWarning("UnitData가 없음");
                continue;
            }
            //최고등급은 안나오게
            if(unit.UnitData.unitGrade == maxGrade) continue;

            UnitData data = unit.UnitData;
            if (!unitCount.ContainsKey(data))
            {
                unitCount[data] = 0;
            }
            unitCount[data]++;
        }
        //등급순으로 리스트 정렬
        var sortedUnits = unitCount.Keys
            .OrderBy(data => data.unitGrade)
            .ToList();

        //정렬된 리스트 슬롯에 배치
        int index = 0;
        foreach(var data in sortedUnits)
        {
            if (index >= slots.Length) break;

            //정렬된 순서의 UnitData 딕셔너리에서 개수 가져오기
            int count = unitCount[data];

            slots[index].Init(data, synthesisController, count);
            index++;
        }
    }
}
