using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public void SetSpeed(float speed)
    {
        Time.timeScale = speed;
    }
}
