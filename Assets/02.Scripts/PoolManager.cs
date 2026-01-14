using System.Collections.Generic;
using UnityEngine;
using static PoolManager;

public class PoolManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public GameObject prefab;
        public int size;
    }

    [SerializeField] private List<Pool> pools;

    //프리팹과 오브젝트 큐를 매핑하는 딕셔너리
    private Dictionary<GameObject, Queue<GameObject>> poolDictionary
        = new Dictionary<GameObject, Queue<GameObject>>();

    private void Awake()
    {
        if (poolDictionary == null) poolDictionary = new Dictionary<GameObject, Queue<GameObject>>();
        if (pools == null || pools.Count == 0)
        {
            Debug.LogWarning("PoolManager pools 리스트가 비어있음");
            return;
        }
        Debug.Log($"풀 카운트 {pools.Count}, 풀 {pools}");

        foreach (var pool in pools)
        {
            Debug.Log($"Pool 등록 {pool.prefab.name} / instanceID: {pool.prefab.GetInstanceID()}");

            //오브젝트 큐 생성
            Queue<GameObject> objectQ = new Queue<GameObject>();

            //풀 사이즈만큼 오브젝트 생성
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                objectQ.Enqueue(obj);
            }
            //딕셔너리에 큐 추가
            poolDictionary.Add(pool.prefab, objectQ);
        }
    }

    public GameObject SpawnPool(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        string keys = string.Join(", ", poolDictionary.Keys);
        Debug.Log($"현재 풀 목록: {keys} / 찾는 키: {prefab.name}");
        Debug.Log($"SpawnPool 스폰 {prefab.name} / instanceID: {prefab.GetInstanceID()}");
        if (!poolDictionary.ContainsKey(prefab))
        {
            Debug.LogError($"풀에 등록 안됨: {prefab.name}");
            return null;
        }

        GameObject obj;

        if(poolDictionary[prefab].Count > 0)
        {
            //큐에서 오브젝트 꺼내기
            obj = poolDictionary[prefab].Dequeue();
        }
        else
        {
            //큐가 비어있으면 새로 생성
            obj = Instantiate(prefab, transform);
        }

        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);

        return obj;
    }

    public void ReturnPool(GameObject prefab, GameObject obj)
    {
        //프리팹이 딕셔너리에 없으면 반환하지 않음
        if (!poolDictionary.ContainsKey(prefab)) return;

        obj.SetActive(false);
        poolDictionary[prefab].Enqueue(obj);
    }
}
