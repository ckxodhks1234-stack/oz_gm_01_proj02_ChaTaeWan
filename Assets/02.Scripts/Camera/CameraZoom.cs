using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 0.1f;
    [SerializeField] private float maxHeight = 40f;
    [SerializeField] private float minHeight = 10f;

    void Start()
    {
        //기본 상태 - 가장 넓은 화면
        Vector3 pos = transform.position;
        pos.y = maxHeight;
        transform.position = pos;
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) < 0.001f) return;

        //스크롤 위 - 줌 인 / 아래 - 줌 아웃
        float zoomAmount = -scroll * zoomSpeed;

        Vector3 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y + zoomAmount, minHeight, maxHeight);
        transform.position = pos;
    }
}
