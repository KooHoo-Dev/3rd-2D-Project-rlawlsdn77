using UnityEngine;

namespace SHIFTER
{
    // 보스 약점 스크립트
    // 그로기 상태일 때 활성화되고, 플레이어가 위에서 밟으면 보스 체력 감소 + 즉시 비활성화
    public class BossHeart : MonoBehaviour
    {
        private Boss boss;
        private BossManager bossManager;

        private void Start()
        {
            boss = FindFirstObjectByType<Boss>();
            bossManager = FindFirstObjectByType<BossManager>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name != "Shifter") return; // 플레이어만 판정
            if (collision.transform.position.y <= transform.position.y) return; // 위에서 밟았을 때만 처리

            boss.CurrentBossCount--; // 보스 체력 감소
            bossManager.DamageToBoss(); // 보스 체력 log + 플레이어 체력 회복
            gameObject.SetActive(false); // 약점 비활성화
        }
    }
}
