using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] private PlayerGold playerGold;
    public int currentMonsterCount => Monsters.Count;

    public List<MonsterBase> Monsters { get; private set; } = new List<MonsterBase>();

    //몬스터베이스 받아서 리스트에 추가
    public void SpawnMonster(MonsterBase monster)
    {
        if (!Monsters.Contains(monster))
        {
            Monsters.Add(monster);
            //currentMonsterCount++;
        }
    }

    //몬스터베이스 받아서 리스트에서 제거
    public void DieMonster(MonsterBase monster)
    {
        if (Monsters.Contains(monster))
        {
            Monsters.Remove(monster);
            //currentMonsterCount--;
        }
    }

    public void GiveGold(int amount)
    {
        playerGold.GetGold(amount);
    }
}
