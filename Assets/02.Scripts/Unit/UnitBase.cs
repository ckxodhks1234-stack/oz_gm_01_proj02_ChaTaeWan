using UnityEngine;

public class UnitBase : MonoBehaviour
{
    [SerializeField] private UnitData unitData;
    private PoolManager poolManager;

    private float attackTimer;
    private MonsterBase target;

    public UnitData UnitData { get; private set; }
    public void Init(UnitData data, PoolManager pool)
    {
        unitData = data;
        poolManager = pool;

        attackTimer = 0f;
        target = null;
    }

    void Update()
    {
        attackTimer -= Time.deltaTime;

        //타겟이 없어지거나 범위 밖으로 나가면
        if(target == null || !IsTargetRange(target))
        {
            //가까운 몬스터 찾기
            target = FindClosestMonster();
        }

        if (target == null) return;

        //공격 쿨타임이 0이면 공격
        if (attackTimer <= 0f)
        {
            Attack();
            attackTimer = unitData.attackCoolTime;
        }
    }

    private void Attack()
    {
        target.TakeDamage(unitData.attackDamage);
    }

    private bool IsTargetRange(MonsterBase monster)
    {
        if (monster == null) return false;

        //몬스터-유닛 거리 계산
        float distance = Vector3.Distance(transform.position, monster.transform.position);
        return distance <= unitData.attackRange;
    }

    private MonsterBase FindClosestMonster()
    {
        MonsterBase[] monsters = FindObjectsOfType<MonsterBase>();

        MonsterBase closest = null;
        float closestDistance = Mathf.Infinity;

        //가장 가까운 몬스터 찾기
        foreach (var monster in monsters)
        {
            float distance = Vector3.Distance(transform.position, monster.transform.position);

            if (distance < closestDistance && distance <= unitData.attackRange)
            {
                closestDistance = distance;
                closest = monster;
            }
        }
        return closest;
    }

    public void ReturnPoolUnit()
    {
        poolManager.ReturnPool(unitData.unitPrefab, gameObject);
    }
}
