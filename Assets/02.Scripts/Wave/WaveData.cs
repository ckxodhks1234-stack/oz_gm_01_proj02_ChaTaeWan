using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveMonster
{
    public MonsterData monsterData;
    public int monsterSpawnCount;
}

[CreateAssetMenu(fileName = "WaveData", menuName = "WaveData")]
public class WaveData : ScriptableObject
{
    public int waveIndex;

    [Header("스폰세팅")]
    public float spawnInterval;

    [Header("몬스터 데이터")]
    public List<WaveMonster> waveMonsters;

    [Header("BGM")]
    public AudioClip bgm;
}
