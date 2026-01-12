using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "MonsterData")]
public class MonsterData : ScriptableObject
{
    [Header("몬스터 기본 정보")]
    public string monsterName;
    public int monsterMaxHp;
    public float monsterMoveSpeed;
    public GameObject monsterPrefab;

    [Header("몬스터 보상")]
    public int monsterGold;
}
