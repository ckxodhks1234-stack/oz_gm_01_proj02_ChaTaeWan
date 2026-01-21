using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum UnitState
{
    Move,
    Attack,
    AttackCoolTime
}

public class UnitBase : MonoBehaviour
{
    [SerializeField] private UnitData unitData;
    private MonsterManager monsterManager;
    private PoolManager poolManager;

    [SerializeField] private GameObject selectCircle;
    private NavMeshAgent agent;
    private Vector3 destination;
    private bool isPlayerControlled;

    private float attackTimer;
    private float attackBeforeTimer; //공격 선딜레이 타이머
    [SerializeField] private float attackBeforeDelay = 0.2f;

    private MonsterBase target;
    private UnitState currentState;
    private Transform moveTarget;

    public UnitData UnitData { get; private set; }

    public void Init(UnitData data, PoolManager pool, MonsterManager manager)
    {
        this.unitData = data;
        this.UnitData = data;
        this.poolManager = pool;
        this.monsterManager = manager;

        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            return;
        }
        agent.enabled = true;
        agent.isStopped = false;
        agent.speed = unitData.moveSpeed;

        isPlayerControlled = false;
        target = null;
        Selected(false);
        currentState = UnitState.Move;
    }

    public void MoveTo(Vector3 targetPos)
    {
        destination = targetPos;
        isPlayerControlled = true;
        currentState = UnitState.Move;

        agent.isStopped = false;
        agent.SetDestination(destination);
    }

    public void Selected(bool isSelected)
    {
        if(selectCircle != null)
        {
            selectCircle.SetActive(isSelected);
        }
    }

    void Update()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
        if(attackBeforeTimer > 0)
        {
            attackBeforeTimer -= Time.deltaTime;
        }

        switch (currentState)
        {
            case UnitState.Move:
                UpdateMove();
                break;
            case UnitState.Attack:
                UpdateAttack();
                break;
            case UnitState.AttackCoolTime:
                UpdateAttackCoolTime();
                break;
        }
    }

    private void UpdateMove()
    {
        if (isPlayerControlled)
        {
            //목적지 거의 도착했으면 자동모드
            if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                isPlayerControlled = false;
                agent.isStopped = true;
            }
            return; //수동으로 이동중에는 적 탐색 안함
        }

        agent.isStopped = true;
        //자동으로 가장 가까운 몬스터 찾기
        target = FindClosestMonster();

        //타겟이 있고, 공격 쿨타임 돌았으면 공격
        if(target != null)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);

            //사거리 안에 들어왔으면 공격 모드로 전환
            if (distance <= unitData.attackRange && attackTimer <= 0)
            {
                currentState = UnitState.Attack;
                attackBeforeTimer = attackBeforeDelay;
            }
        }
    }

    private void UpdateAttack()
    {
        if (target == null || !target.gameObject.activeSelf)
        {
            currentState = UnitState.Move;
            return;
        }
        transform.LookAt(target.transform);
        if (attackBeforeTimer > 0)
        {
            return; //선딜레이 대기
        }
        Shoot(); //선딜레이 끝나면 공격 실행
        attackTimer = unitData.attackCoolTime; //공격 쿨타임 설정
        currentState = UnitState.AttackCoolTime;//공격 쿨타임 상태로 전환
    }

    private void Shoot()
    {
        if (target == null) return;

        GameObject bulletObj = poolManager.SpawnPool(
            unitData.bulletPrefab,
            transform.position + Vector3.up * 0.5f,
            Quaternion.identity
        );

        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bullet.Init(unitData.bulletSpeed, unitData.attackDamage, target, poolManager, unitData.bulletPrefab);
    }

    private void UpdateAttackCoolTime()
    {
        if (target == null || !target.gameObject.activeSelf)
        {
            currentState = UnitState.Move;
            return;
        }
        
        //공격 쿨타임 중은 타겟 따라가기
        agent.isStopped = true;

        //타겟 밖에 있으면 다시 탐색하기
        if (!IsTargetRange(target))
        {
            currentState = UnitState.Move;
            return;
        }

        //쿨타임 끝나고, 사정거리 안에 있으면 떄리기
        if (attackTimer <= 0 && IsTargetRange(target))
        {
            currentState = UnitState.Attack;
            attackBeforeTimer = attackBeforeDelay; //공격 선딜레이
        }
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
        MonsterBase closest = null;
        float closestDistance = float.MaxValue;

        //가장 가까운 몬스터 찾기
        foreach (var monster in monsterManager.Monsters)
        {
            if(monster == null) continue;
            if (!monster.gameObject.activeSelf) continue;

            float distance = Vector3.Distance(transform.position, monster.transform.position);

            if (distance < closestDistance)
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
