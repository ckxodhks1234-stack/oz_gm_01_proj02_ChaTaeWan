using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private MonsterSpawner monsterSpawner;
    [SerializeField] private GameOverUI gameOverUI;

    [Header("설정")]
    [SerializeField] private float waveTime;
    [SerializeField] private float restTime;
    [SerializeField] private TextMeshProUGUI waveTimerText;
    [SerializeField] private TextMeshProUGUI restTimerText;
    [SerializeField] private TextMeshProUGUI waveIndexText;
    [SerializeField] private GameObject gameClearPanel;

    private int currentWaveIndex = 1;
    private bool isGameOver;

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
        if(currentState == TimerState.Wave)
        {
            currentTime -= Time.deltaTime;

            if(currentTime <= 0)
            {
                GameOver();
                return;
            }
            //웨이브 종료조건
            if(monsterSpawner.waveSpawnFinish && monsterSpawner.MonsterCount == 0)
            {
                EndWave();
                return;
            }
        }
        if (currentState == TimerState.Rest)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                currentTime = 0;
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

        //웨이브에 맞는 BGM
        WaveData wave = monsterSpawner.CurrentWave;
        BGMController.Instance.PlayBGM(wave.bgm);

        //몬스터 스폰
        monsterSpawner.StartSpawn();
    }

    private void EndWave()
    {
        //몬스터 스폰 종료
        monsterSpawner.StopSpawn();

        if (monsterSpawner.IsLastWave())
        {
            GameClear();
            return;
        }

        //웨이브 종료
        currentState = TimerState.Rest;
        currentTime = restTime;

        //웨이브시간은 끄고, 휴식시간 켜기
        waveTimerText.gameObject.SetActive(false);
        restTimerText.gameObject.SetActive(true);

        currentWaveIndex++;
    }

    private void GameClear()
    {
        if (isGameOver) return;

        isGameOver = true;
        Time.timeScale = 0f;

        gameClearPanel.SetActive(true);
    }

    private void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        Time.timeScale = 0f;
        gameOverUI.Show(currentWaveIndex);

        Debug.Log("게임 오버");

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
