using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "WaveData")]
public class WaveData : ScriptableObject
{
    public int waveIndex;

    [Header("스폰세팅")]
    public float spawnInterval;

    [Header("몬스터 데이터")]
    public List<MonsterData> spawnMonsters;
}
