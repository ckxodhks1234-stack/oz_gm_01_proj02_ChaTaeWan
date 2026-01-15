using System.Collections.Generic;
using UnityEngine;

public class SynthesisController : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private UnitManager unitManager;
    [SerializeField] private PlayerGold playerGold;
    [SerializeField] private SynthesisUI synthesisUI;
    [SerializeField] private UnitResultUI unitResultUI;
    [SerializeField] private UnitResultPanel unitResultPanel;

    [Header("설정")]
    [SerializeField] private int synthesisCount = 3;
    [SerializeField] private int synthesisCost;
    [SerializeField] private float successRate = 0.7f;
    [SerializeField] private Transform synthesisSpawnPoint;

    private UnitGrade currentGrade;

    private List<UnitData> selectedUnits = new List<UnitData>();

    public void Open()
    {
        selectedUnits.Clear();
        synthesisUI.Open();
        synthesisUI.Refresh(selectedUnits,  synthesisCount);
    }

    public void AddUnit(UnitData unitData)
    {
        //첫 유닛이 등급설정
        if(selectedUnits.Count == 0)
        {
            currentGrade = unitData.unitGrade;
        }

        //다른 등급이면 리턴
        if (unitData.unitGrade != currentGrade) return;

        //카운트 넘어가면 리턴
        if (selectedUnits.Count >= synthesisCount) return;

        //유닛 보유 개수 체크
        int ownedCount = unitManager.GetUnitCountByData(unitData);
        int selectedCount = GetSelectedCount(unitData);

        if (selectedUnits.Count >= ownedCount) return;

        selectedUnits.Add(unitData);
        synthesisUI.Refresh(selectedUnits, synthesisCount);
    }

    private int GetSelectedCount(UnitData data)
    {
        int count = 0;
        foreach (var u in selectedUnits)
        {
            if (u == data) count++;
        }
        return count;
    }

    public void TrySynthesis()
    {
        //3개여야 가능
        if (selectedUnits.Count < synthesisCount) return;

        //돈있어야 가능
        if ((!playerGold.CanSpend(synthesisCost))) return;
        playerGold.Spend(synthesisCost);

        //유닛 제거
        unitManager.RemoveUnitsByGrade(currentGrade, synthesisCount);

        bool success = Random.value <= successRate;
        UnitGrade resultGrade;
        if (success)
        {
            //성공하면 등급업
            resultGrade = currentGrade +1;
        }
        //실패하면 그대로
        else resultGrade = currentGrade;

        //결과 데이터 가져와서 스폰
        UnitData resultData = unitManager.GetUnitDataByGrade(resultGrade);
        unitManager.SpawnUnit(resultData, synthesisSpawnPoint.position, UnitResult.Synthesis);

        //합성결과 UI
        unitResultPanel.AddSynthesisResult(success ? "Synthesis success!" : "Synthesis failed..");

        selectedUnits.Clear();
        synthesisUI.Refresh(selectedUnits, synthesisCount);
    }

    public void Cancel()
    {
        selectedUnits.Clear();
        synthesisUI.Refresh(selectedUnits, synthesisCount);
    }

    public void RemoveUnitAt(int index)
    {
        if (index >= 0 && index < selectedUnits.Count)
        {
            selectedUnits.RemoveAt(index);
            synthesisUI.Refresh(selectedUnits, synthesisCount);
        }
    }
}
