using UnityEngine;
using TMPro;

public class GoldUnitUI : MonoBehaviour
{
    [Header("텍스트")]
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI unitText;
    [SerializeField] private TextMeshProUGUI enemyText;

    [Header("스크립트")]
    [SerializeField] private PlayerGold playerGold;
    [SerializeField] private UnitManager unitManager;
    [SerializeField] private MonsterManager monsterManager;

    void Update()
    {
        UpdateGold();
        UpdateUnitCount();
        UpdateMonsterCount();
    }

    private void UpdateGold()
    {
        goldText.text = $"{playerGold.currentGold}G";
    }

    private void UpdateUnitCount()
    {
        unitText.text = $"Unit: {unitManager.GetUnitCount()} / {unitManager.maxUnitCount}";
    }

    private void UpdateMonsterCount()
    {
        enemyText.text = $"Monster: {monsterManager.currentMonsterCount} / {monsterManager.maxMonsterCount}";
    }
}
