using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private MonsterSpawner monsterSpawner;

    [SerializeField] private float waveTime;
    [SerializeField] private float restTime;
    [SerializeField] private TextMeshProUGUI waveTimerText;
    [SerializeField] private TextMeshProUGUI restTimerText;

    [SerializeField] private TextMeshProUGUI waveIndexText;
    private int currentWaveIndex = 1;

    private enum TimerState
    {
        Wave,
        Rest
    }

    private float currentTime;
    private TimerState currentState;

    void Start()
    {
        StartWave();
    }

    void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;

            if (currentState == TimerState.Wave)
            {
                EndWave();
            }
            else if (currentState == TimerState.Rest)
            {
                StartWave();
            }
        }

        TimeUI();
    }

    private void StartWave()
    {
        //웨이브 시작
        currentState = TimerState.Wave;
        currentTime = waveTime;

        //휴식시간은 끄고, 웨이브시간 켜기
        waveTimerText.gameObject.SetActive(true);
        restTimerText.gameObject.SetActive(false);

        //웨이브 표시
        waveIndexText.text = $"Wave {currentWaveIndex}";
        monsterSpawner.SetWave(currentWaveIndex);

        //몬스터 스폰
        monsterSpawner.StartSpawn();
    }

    private void EndWave()
    {
        //웨이브 종료
        currentState = TimerState.Rest;
        currentTime = restTime;

        //웨이브시간은 끄고, 휴식시간 켜기
        waveTimerText.gameObject.SetActive(false);
        restTimerText.gameObject.SetActive(true);

        //몬스터 스폰 종료
        monsterSpawner.StopSpawn();
    }

    private void TimeUI()
    {
        if (currentState == TimerState.Wave)
        {
            waveTimerText.text = $"{currentTime:F1}";
        }
        else
        {
            restTimerText.text = $"{currentTime:F1}";
        }
    }
}
