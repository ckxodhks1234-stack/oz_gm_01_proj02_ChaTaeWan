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
    private Coroutine spawnRoutine;

    public void Spawn()
    {
        if (currentWave == null || currentWave.spawnMonsters.Count == 0) return;

        MonsterData data =currentWave.spawnMonsters[Random.Range(0, currentWave.spawnMonsters.Count)];

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

        if (currentWave == null) return;
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
        while (true)
        {
            //맥스카운트가 될 때까지 계속 스폰
            if (monsterManager.currentMonsterCount < monsterManager.maxMonsterCount)
            {
                Spawn();
            }
            //웨이브별 인터벌만큼 기다리기
            yield return new WaitForSeconds(currentWave.spawnInterval);
        }
    }
}
