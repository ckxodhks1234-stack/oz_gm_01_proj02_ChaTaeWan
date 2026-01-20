using UnityEngine;

public enum UnitGrade
{
    Normal,
    Rare,
    Epic,
    Unique,
    Legend,
    Chowall,
    TaeCho
}
[CreateAssetMenu(fileName = "UnitData", menuName = "UnitData")]
public class UnitData : ScriptableObject
{
    [Header("À¯´Ö Á¤º¸")]
    public string unitName;
    public GameObject unitPrefab;
    public UnitGrade unitGrade;
    public Sprite icon;

    [Header("UI")]
    public Sprite frameSprite;
    public Color frameColor;

    [Header("À¯´Ö ´É·ÂÄ¡")]
    public int attackDamage;
    public float attackCoolTime;
    public float attackRange;
    public float moveSpeed;

    [Header("È®·ü")]
    public float unitChance;

    [Header("ÃÑ¾Ë")]
    public GameObject bulletPrefab;
    public float bulletSpeed;
}
