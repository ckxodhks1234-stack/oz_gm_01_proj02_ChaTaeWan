using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public int currentMonsterCount;
    public int maxMonsterCount;

    public List<MonsterBase> Monsters { get; private set; } = new List<MonsterBase>();

    //몬스터베이스 받아서 리스트에 추가
    public void SpawnMonster(MonsterBase monster)
    {
        if (!Monsters.Contains(monster))
        {
            Monsters.Add(monster);
            //currentMonsterCount++;
            CheckGameOver();
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

    private void CheckGameOver()
    {
        if (currentMonsterCount >= maxMonsterCount)
        {
            Time.timeScale = 0f;
            Debug.Log("게임오버");
            //UI추가
        }
    }
}
