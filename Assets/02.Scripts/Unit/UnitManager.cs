using System;
using System.Collections.Generic;
using UnityEngine;

public enum UnitResult
{
    Gacha,
    Synthesis
}

public class UnitManager : MonoBehaviour
{
    [SerializeField] public int maxUnitCount = 30;

    [SerializeField] private List<UnitData> unitDataList;
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private MonsterManager monsterManager;

    private List<UnitBase> Units = new List<UnitBase>();

    public event Action UnitChanged;
    public event Action<UnitData, UnitResult> UnitSpawned;

    public bool CanAddUnit()
    {
        return Units.Count < maxUnitCount;
    }

    public void AddUnit(UnitBase unit)
    {
        Units.Add(unit);
        UnitChanged?.Invoke(); //유닛 변화생기면 이벤트 실행
    }

    public void RemoveUnit(UnitBase unit)
    {
        Units.Remove(unit);
        UnitChanged?.Invoke();
    }

    public int GetUnitCount()
    {
        return Units.Count;
    }

    //특정등급 유닛 수 가져오기
    public int GetUnitCountByGrade(UnitGrade grade)
    {
        int count = 0;
        //유닛 리스트에서 등급 같은 것끼리 숫자 올리기
        foreach (var unit in Units)
        {
            if (unit.UnitData.unitGrade == grade)
            {
                count++;
            }
        }
        return count;
    }

    //특정 등급 유닛 가져오기
    public List<UnitBase> GetUnitByGrade(UnitGrade grade)
    {
        List<UnitBase> result = new List<UnitBase>();

        foreach (var unit in Units)
        {
            if (unit.UnitData.unitGrade == grade)
            {
                result.Add(unit);
            }
        }
        return result;
    }

    public void RemoveUnitsByGrade(UnitGrade grade, int count)
    {
        int removed = 0;

        for (int i = Units.Count - 1; i >= 0; i--)
        {
            if (Units[i].UnitData.unitGrade == grade)
            {
                Units[i].ReturnPoolUnit();
                Units.RemoveAt(i);
                removed++;
                UnitChanged?.Invoke();

                if (removed >= count) break;
            }
        }
    }

    //유닛 생성
    public void SpawnUnit(UnitData unitData, Vector3 pos, UnitResult result)
    {
        if (unitData == null || unitData.unitPrefab == null)
        {
            Debug.LogError("UnitData 또는 프리팹이 비어있음");
            return;
        }

        GameObject unitObj = poolManager.SpawnPool(unitData.unitPrefab, pos, Quaternion.identity);
        if (unitObj == null)
        {
            Debug.LogError("SpawnPool 오브젝트 없음: " + unitData.unitPrefab.name);
            return;
        }
        UnitBase unitBase = unitObj.GetComponent<UnitBase>();
        if (unitBase == null)
        {
            Debug.LogError("UnitBase 없음: " + unitObj.name);
        }
        unitBase.Init(unitData, poolManager, monsterManager);
        if (unitBase.UnitData == null)
        {
            Debug.LogError($"{unitObj.name}의 UnitData Init null");
        }
        AddUnit(unitBase);

        UnitSpawned?.Invoke(unitData, result);
    }

    //등급에 맞는 유닛 데이터 가져오기
    public UnitData GetUnitDataByGrade(UnitGrade grade)
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

    public List<UnitBase> GetUnits()
    {
        return Units;
    }

    public int GetUnitCountByData(UnitData data)
    {
        int count = 0;
        foreach (var unit in Units)
        {
            if (unit != null && unit.UnitData == data)
                count++;
        }
        return count;
    }
}
