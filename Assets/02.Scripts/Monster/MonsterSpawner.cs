using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private WayPointPath wayPointPath;
    [SerializeField] private MonsterManager monsterManager;
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private List<WaveData> waveDataList;

    private WaveData currentWave;
    public WaveData CurrentWave => currentWave;
    public int TotalSpawnCount => currentWave.TotalMonsterCount;
    public int MonsterCount => monsterManager.currentMonsterCount;
    public bool waveSpawnFinish => spawnRoutine == null && monsterManager.currentMonsterCount == 0;


    private Coroutine spawnRoutine;

    public void Spawn(MonsterData data)
    {
        if (data == null) return;

        GameObject monsterObj = poolManager.SpawnPool(data.monsterPrefab, wayPointPath.points[0].position, Quaternion.identity);

        MonsterBase monster = monsterObj.GetComponent<MonsterBase>();
        monster.Initiallize(data, wayPointPath.GetPath(), monsterManager, poolManager);

        monsterManager.SpawnMonster(monster);
    }

    public void SetWave(int waveIndex)
    {
        //웨이브 데이터 리스트에서 현재 웨이브 인덱스에 해당하는 데이터 찾기
        //foreach (WaveData w in waveDataList)
        //{
        //    if (w.waveIndex == waveIndex)
        //    {
        //        currentWave = w;
        //        break;
        //    }
        //} 아래줄이 람다식으로 변경한 것
        currentWave = waveDataList.Find(w => w.waveIndex == waveIndex);

        if (currentWave == null || monsterManager == null) return;
        Debug.Log($"현재 웨이브 : {currentWave.waveIndex}");
    }

    public void StartSpawn()
    {
        if (spawnRoutine == null)
        {
            spawnRoutine = StartCoroutine(SpawnRoutine());
        }
    }

    public void StopSpawn()
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
            spawnRoutine = null;
        }
    }

    private IEnumerator SpawnRoutine()
    {
        if (currentWave == null) yield break;

        //웨이브몬스터에 설정된 몬스터 순차적으로 스폰
        foreach(WaveMonster waveMonster in currentWave.waveMonsters)
        {
            //설정된 몬스터 수만큼 스폰
            for (int i = 0; i < waveMonster.monsterSpawnCount; i++)
            {
                Spawn(waveMonster.monsterData);
                yield return new WaitForSeconds(currentWave.spawnInterval);
            }
        }
        spawnRoutine = null;
    }
}
