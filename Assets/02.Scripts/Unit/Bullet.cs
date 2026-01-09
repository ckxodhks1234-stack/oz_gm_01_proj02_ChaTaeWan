using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed;
    private int bulletDamage;
    private MonsterBase targetMonster;
    private PoolManager poolManager;
    private GameObject bulletPrefab;

    public void Init(float speed, int damage, MonsterBase target, PoolManager pool, GameObject prefab)
    {
        bulletSpeed = speed;
        bulletDamage = damage;
        targetMonster = target;
        poolManager = pool;
        bulletPrefab = prefab;

        transform.rotation = Quaternion.LookRotation((targetMonster.transform.position - transform.position).normalized);
    }

    void Update()
    {
        //타겟 몬스터가 없거나 비활성화 되었는지 확인
        if (targetMonster == null || !targetMonster.gameObject.activeSelf)
        {
            Return ();
            return;
        }

        //총알 방향
        Vector3 dir = (targetMonster.transform.position - transform.position).normalized;
        transform.position += dir * bulletSpeed * Time.deltaTime;

        //총알이 몬스터에 도달했는지 확인
        if (Vector3.Distance(transform.position, targetMonster.transform.position) < 0.1f)
        {
            Debug.Log($"{gameObject.name}가 {targetMonster.gameObject.name} 에게 {bulletDamage} 데미지");
            targetMonster.TakeDamage(bulletDamage);
            Return();
        }
    }

    private void Return()
    {
        poolManager.ReturnPool(bulletPrefab, gameObject);
    }
}
