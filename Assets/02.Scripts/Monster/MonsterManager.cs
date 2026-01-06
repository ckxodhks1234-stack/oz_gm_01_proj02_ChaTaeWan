using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public int currentMonsterCount;
    public int maxMonsterCount;

    public void SpawnEnemy()
    {
        currentMonsterCount++;
        CheckGameOver();
    }

    public void DieEnemy()
    {
        currentMonsterCount--;
    }

    private void CheckGameOver()
    {
        if (currentMonsterCount >= maxMonsterCount)
        {
            Time.timeScale = 0f;
            //UIÃß°¡
        }
    }
}
