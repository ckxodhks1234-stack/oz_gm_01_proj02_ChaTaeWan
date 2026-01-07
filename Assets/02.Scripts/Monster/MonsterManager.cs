using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public int currentMonsterCount;
    public int maxMonsterCount;

    public void SpawnMonster()
    {
        currentMonsterCount++;
        CheckGameOver();
    }

    public void DieMonster()
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
