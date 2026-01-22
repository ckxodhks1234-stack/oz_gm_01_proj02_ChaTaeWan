using System.Collections;
using UnityEngine;


public class HitAnim : MonoBehaviour
{
    [SerializeField] private float knockBackDistance = 0.12f;
    [SerializeField] private float shakePower = 0.08f;
    [SerializeField] private float duration = 0.12f;

    Vector3 originPos;
    Coroutine hitRoutine;

    void Awake()
    {
        originPos = transform.localPosition;
    }

    public void PlayHit(Vector3 hitDir)
    {
        if (hitRoutine != null) StopCoroutine(hitRoutine);

        hitRoutine = StartCoroutine(HitCoRoutine(hitDir));
    }

    IEnumerator HitCoRoutine(Vector3 dir)
    {
        Vector3 back = -dir.normalized * knockBackDistance;
        Vector3 shake = Random.insideUnitSphere * shakePower;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.localPosition = Vector3.Lerp(
                originPos,
                originPos + back + shake,
                t
            );
            yield return null;
        }

        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.localPosition = Vector3.Lerp(
                originPos + back + shake,
                originPos,
                t
            );
            yield return null;
        }

        transform.localPosition = originPos;
    }
}
