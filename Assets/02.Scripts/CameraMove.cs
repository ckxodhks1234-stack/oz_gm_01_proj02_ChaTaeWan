using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float cameraSpeed = 10f;
    [SerializeField] private float edgeSize = 10f; //마우스 가장자리 감지 크기
    [SerializeField] private float maxDist = 50f;

    void Update()
    {
        Vector3 pos = transform.position;

        //마우스 위치
        Vector3 mousePos = Input.mousePosition;

        //오른쪽 끝
        if (mousePos.x >= Screen.width - edgeSize)
            pos.x -= cameraSpeed * Time.deltaTime;

        //왼쪽 끝
        if (mousePos.x <= edgeSize)
            pos.x += cameraSpeed * Time.deltaTime;

        //위쪽 끝
        if (mousePos.y >= Screen.height - edgeSize)
            pos.z -= cameraSpeed * Time.deltaTime;

        //아래쪽 끝
        if (mousePos.y <= edgeSize)
            pos.z += cameraSpeed * Time.deltaTime;

        //카메라 이동 범위 제한
        pos.x = Mathf.Clamp(pos.x, -maxDist, maxDist);
        pos.z = Mathf.Clamp(pos.z, -maxDist, maxDist);

        transform.position = pos;
    }
}
