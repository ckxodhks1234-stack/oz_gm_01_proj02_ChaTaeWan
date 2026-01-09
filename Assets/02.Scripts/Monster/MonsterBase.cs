using UnityEngine;

public class MonsterBase : MonoBehaviour
{
    private int currentHp;
    private float moveSpeed;

    [Header("웨이 세팅")]
    private  Transform[] path;
    private int targetIndex;
    [SerializeField] private float arriveRange = 0.1f;
    [SerializeField] private float rotateSpeed = 10f;

    private PoolManager poolManager;
    private MonsterManager monsterManager;
    private MonsterData monsterData;

    //몬스터 정보들 초기화
    public void Initiallize(MonsterData data, Transform[] pathPoints,
        MonsterManager manager, PoolManager pool)
    {
        monsterData = data;
        monsterManager = manager;
        poolManager = pool;

        currentHp = data.monsterMaxHp;
        moveSpeed = data.monsterMoveSpeed;
        path = pathPoints;
        targetIndex = 0;

        monsterManager.Monsters.Add(this);
        if(monsterManager.Monsters.Count > 0)
        {
            //Debug.Log($"현재 몬스터 수 : {monsterManager.Monsters.Count}");
        }
    }

    void Update()
    {
        MovePath();
    }

    private void MovePath()
    {
        if (path == null || path.Length == 0) return;

        Vector3 currentPos = transform.position;
        Vector3 targetPos = path[targetIndex].position;

        //이동
        Vector3 moveDir = (targetPos - currentPos).normalized;
        transform.position = Vector3.MoveTowards(currentPos, targetPos, moveSpeed * Time.deltaTime);

        //보는 방향으로 회전
        if (moveDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }

        if (Vector3.Distance(currentPos, targetPos) <= arriveRange)
        {
            targetIndex++;  //다음 웨이 포인트를 목표로 설정

            //타겟 인덱스가 경로 길이만큼 오면 도착
            if (targetIndex >= path.Length)
            {
                //처음부터 다시 시작
                targetIndex = 0;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //죽으면 풀 반환
        monsterManager.DieMonster(this);
        poolManager.ReturnPool(monsterData.monsterPrefab, gameObject);
        monsterManager.Monsters.Remove(this);
    }
}
