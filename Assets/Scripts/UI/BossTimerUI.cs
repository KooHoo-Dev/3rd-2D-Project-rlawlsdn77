using TMPro;
using UnityEngine;

namespace SHIFTER
{
    // 보스전 시작(0초)부터 보스 격파 시점까지 걸린 시간을 mm:ss로 표시합니다.
    // 격파되면 그 시점 시간에서 멈춥니다.
    public class BossTimerUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text timerText; // 비워두면 같은 오브젝트/자식에서 자동으로 찾음

        private BossManager bossManager;
        private Boss boss;
        private float elapsedTime = 0f;
        private bool isRunning = false;

        private void Start()
        {
            if (timerText == null) timerText = GetComponentInChildren<TMP_Text>();
            bossManager = FindFirstObjectByType<BossManager>();
            boss = FindFirstObjectByType<Boss>();
        }

        private void Update()
        {
            if (bossManager == null || boss == null) return;

            if (!isRunning)
            {
                if (!bossManager.EncounterStarted) return;
                isRunning = true;
                elapsedTime = 0f;
            }

            if (boss.CurrentBossCount <= 0)
            {
                isRunning = false; // 격파 시점 시간에서 정지
            }
            else
            {
                elapsedTime += Time.deltaTime;
            }

            int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(elapsedTime % 60f);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }
}
