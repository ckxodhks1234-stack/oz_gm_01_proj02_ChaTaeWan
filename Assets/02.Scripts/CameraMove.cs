using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float cameraSpeed = 10f;
    [SerializeField] private float edgeSize = 10f; //마우스 가장자리 감지 크기

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

        transform.position = pos;
    }
}
