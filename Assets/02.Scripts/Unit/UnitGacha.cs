using UnityEngine;
using System.Collections.Generic;

public class UnitGacha : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private List<UnitData> unitDataList;
    [SerializeField] private PlayerGold playerGold;
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private UnitManager unitManager;
    [SerializeField] private MonsterManager monsterManager;

    [Header("설정")]
    [SerializeField] private int gachaCost;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private KeyCode gachaKey = KeyCode.G;
    [SerializeField] private float gachaCoolTime = 0.1f;
    [SerializeField] private float spawnRadius = 2f;
    [SerializeField] private LayerMask unitLayer;

    private float gachaTimer;

    private void Update()
    {
        gachaTimer -= Time.deltaTime;

        if(Input.GetKeyDown(gachaKey) && gachaTimer <= 0f)
        {
            TryGacha();
            gachaTimer = gachaCoolTime;
        }
    }
    public void TryGacha()
    {
        if(playerGold == null || !playerGold.CanSpend(gachaCost))
        {
            return;
        }
        if (!unitManager.CanAddUnit()) return;

        if (playerGold != null && playerGold.CanSpend(gachaCost))
        {
            UnitData randomUnit = GetRandomUnit();

            if (randomUnit == null) return;
            if (randomUnit != null)
            {
                playerGold.Spend(gachaCost);

                Vector3 spawnPos = GetUnitPosition();

                GameObject unitObj = poolManager.SpawnPool(randomUnit.unitPrefab,
                    spawnPos, randomUnit.unitPrefab.transform.rotation);

                UnitBase unitBase = unitObj.GetComponent<UnitBase>();
                unitBase.Init(randomUnit, poolManager, monsterManager);

                unitManager.AddUnit(unitBase);
            }
        }
    }

    private UnitData GetRandomUnit()
    {
        if (unitDataList == null || unitDataList.Count == 0) return null;

        //전체 확률 합계산
        float totalChance = 0;
        foreach (var data in unitDataList)
        {
            totalChance += data.unitChance;
        }

        //랜덤 값 생성
        float randomValue = Random.Range(0, totalChance);
        float tempSum = 0;

        //확률에 따른 유닛 선택
        foreach (var data in unitDataList)
        {
            tempSum += data.unitChance;
            if (randomValue <= tempSum)
            {
                return data;
            }
        }
        return null;
    }

    private Vector3 GetUnitPosition()
    {
        Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
        randomOffset.y = 0;
        Vector3 spawnPos = spawnPoint.position + randomOffset;
        //다른 유닛과 겹치지 않도록 위치 조정
        Collider[] colliders = Physics.OverlapSphere(spawnPos, 0.5f, unitLayer);
        while (colliders.Length > 0)
        {
            randomOffset = Random.insideUnitSphere * spawnRadius;
            randomOffset.y = 0;
            spawnPos = spawnPoint.position + randomOffset;
            colliders = Physics.OverlapSphere(spawnPos, 0.5f, unitLayer);
        }
        return spawnPos;
    }
}
