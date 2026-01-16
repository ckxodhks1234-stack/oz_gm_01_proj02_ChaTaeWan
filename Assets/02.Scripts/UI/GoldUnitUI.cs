using UnityEngine;
using TMPro;

public class GoldUnitUI : MonoBehaviour
{
    [Header("텍스트")]
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI unitText;
    [SerializeField] private TextMeshProUGUI enemyText;

    [Header("참조")]
    [SerializeField] private PlayerGold playerGold;
    [SerializeField] private UnitManager unitManager;
    [SerializeField] private MonsterManager monsterManager;
    [SerializeField] private MonsterSpawner monsterSpawner;

    void Update()
    {
        UpdateGold();
        UpdateUnitCount();
        UpdateMonsterCount();
    }

    private void UpdateGold()
    {
        goldText.text = $"{playerGold.currentGold}";
    }

    private void UpdateUnitCount()
    {
        unitText.text = $"Unit: {unitManager.GetUnitCount()} / {unitManager.maxUnitCount}";
    }

    private void UpdateMonsterCount()
    {
        int alive = monsterManager.Monsters.Count;
        int total = monsterSpawner.TotalSpawnCount;
        enemyText.text = $"남은 적: {alive}/{total}";
    }
}
