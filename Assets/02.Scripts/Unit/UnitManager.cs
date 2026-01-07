using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField] public int maxUnitCount = 30;
    [SerializeField] private float synthesisSuccessRate = 0.7f;

    [SerializeField] private List<UnitData> unitDataList;
    [SerializeField] private PoolManager poolManager;

    private List<UnitBase> units = new List<UnitBase>();

    public bool CanAddUnit()
    {
        return units.Count < maxUnitCount;
    }

    public void AddUnit(UnitBase unit)
    {
        units.Add(unit);
    }

    public void RemoveUnit(UnitBase unit)
    {
        units.Remove(unit);
    }

    public int GetUnitCount()
    {
        return units.Count;
    }

    //합성 시도
    private bool TrySynthesis(UnitGrade grade)
    {
        //모든 유닛의 등급이 동일한지 확인
        List<UnitBase> candidates = GetUnitByGrade(grade);

        if(candidates.Count < 3) return false; //합성할 유닛이 부족

        //최고등급이면 합성 불가
        if (grade == UnitGrade.WuKong) return false;

        //합성 유닛 3개 선택
        List<UnitBase> synthesisUnits = new List<UnitBase>
        {
            candidates[0],
            candidates[1],
            candidates[2]
        };

        ExecuteSynthesis(synthesisUnits);
        return true;
    }

    //합성 처리
    private void ExecuteSynthesis(List<UnitBase> synthesisUnits)
    {
        //합성 유닛 등급과 위치 저장
        UnitGrade currentGrade = synthesisUnits[0].UnitData.unitGrade;
        Vector3 spawnPos = synthesisUnits[0].transform.position;

        //합성 유닛 없애기
        foreach (var unit in synthesisUnits)
        {
            RemoveUnit(unit);
            unit.ReturnPoolUnit();
        }

        bool success = Random.value <= synthesisSuccessRate;
        if (!success)
        {
            //실패하면 원래 등급 1개 생성
            UnitData failUnitData = GetUnitDataByGrade(currentGrade);
            SpawnUnit(failUnitData, spawnPos);
            return;
        }

        //새로운 유닛 생성
        UnitGrade newGrade = currentGrade + 1;
        UnitData newUnitData = GetUnitDataByGrade(newGrade);

        if(newUnitData == null) return;

        SpawnUnit(newUnitData, spawnPos);
    }

    //유닛 생성
    private void SpawnUnit(UnitData unitData, Vector3 pos)
    {
        GameObject unitObj = poolManager.SpawnPool(unitData.unitPrefab, pos, Quaternion.identity);
        UnitBase unitBase = unitObj.GetComponent<UnitBase>();
        unitBase.Init(unitData, poolManager);
        AddUnit(unitBase);
    }

    //특정 등급 유닛 가져오기
    private List<UnitBase> GetUnitByGrade(UnitGrade grade)
    {
        List<UnitBase> result = new List<UnitBase>();

        foreach (var unit in units)
        {
            if (unit.UnitData.unitGrade == grade)
            {
                result.Add(unit);
            }
        }
        return result;
    }

    //등급에 맞는 유닛 데이터 가져오기
    private UnitData GetUnitDataByGrade(UnitGrade grade)
    {
        foreach (var data in unitDataList)
        {
            if (data.unitGrade == grade)
            {
                return data;
            }
        }
        return null;
    }
}
