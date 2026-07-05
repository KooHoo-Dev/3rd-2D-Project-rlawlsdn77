using UnityEngine;
using UnityEngine.UI;

namespace SHIFTER
{
    // 보스의 현재 체력(Boss.CurrentBossCount / MaxBossCount)을 UI의 fillAmount에 실시간으로 반영합니다.
    //
    // 사용법:
    // 1. 체력바로 사용할 UI Image(Image Type = Filled)에 이 스크립트를 붙입니다.
    // 2. healthBar 필드를 비워두면 같은 오브젝트의 Image 컴포넌트를 자동으로 사용합니다.
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
            // 보스가 아직 씬에 없거나(비활성화 등) 나중에 생성되는 경우를 대비해 계속 재탐색합니다.
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
