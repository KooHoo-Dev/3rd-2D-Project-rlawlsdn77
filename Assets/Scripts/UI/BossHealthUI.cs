using UnityEngine;
using UnityEngine.UI;

namespace SHIFTER
{
    // 보스의 현재 체력(Boss.CurrentBossCount / MaxBossCount)을 UI의 fillAmount에 실시간으로 반영합니다.
    public class BossHealthUI : MonoBehaviour
    {
        [SerializeField] private Image healthBar;

        private Boss boss;

        private void Start()
        {
            if (healthBar == null) healthBar = GetComponent<Image>();
            boss = FindFirstObjectByType<Boss>();
        }

        private void Update()
        {
            // 보스가 씬에 없거나 비활성화됬었을 때 찾을때까지 탐색
            if (boss == null)
            {
                boss = FindFirstObjectByType<Boss>();
                return;
            }

            if (healthBar != null && boss.MaxBossCount > 0)
            {
                healthBar.fillAmount = (float)boss.CurrentBossCount / boss.MaxBossCount;
            }
        }
    }
}
