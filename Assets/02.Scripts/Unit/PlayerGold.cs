using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    public int currentGold;

    public bool CanSpend(int amount)
    {
        return currentGold >= amount;
    }

    public void Spend(int amount)
    {
        if (CanSpend(amount))
        {
            currentGold -= amount;
        }
    }

    public void Earn(int amount)
    {
        currentGold += amount;
    }
}
