using UnityEngine;

namespace SHIFTER
{
    // 보스 약점에 관한 스크립트
    // 그로기 상태일 시 BossHeart 오브젝트를 활성화시키고, 플레이어가 BossHeart 오브젝트를 위에서 밟을 시
    // 플레이어와 동일한 Tag를 가진 경우 Boss의 CurrentBossCount 감소 및 BossManager의 DamageToBoss() 호출
    // BossHeart 공격 성공 시 플레이어가 튕겨지는 효과 발생
    // 그로기 종료 시 BossHeart 오브젝트를 비활성화 시킵니다.
    public class BossHeart : MonoBehaviour
    {
        [SerializeField] private Boss boss;
        [SerializeField] private BossManager bossManager;
        [SerializeField] private float bounceForce = 15f; // 튕겨지는 힘 설정

        private void Start()
        {
            // 인스펙터에서 할당하지 않았을 경우를 대비한 초기화
            if (boss == null) boss = FindFirstObjectByType<Boss>();
            if (bossManager == null) bossManager = FindFirstObjectByType<BossManager>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Shifter")
            {
                Shifter player = collision.GetComponent<Shifter>();
                if (player == null) return;

                // 1. 위에서 밟았는지 확인 (플레이어의 y좌표가 BossHeart의 y좌표보다 높은지)
                if (collision.transform.position.y > transform.position.y)
                {
                    // 2. 플레이어와 동일한 Tag를 가진 경우인지 확인
                    if (gameObject.CompareTag(player.currentTag))
                    {
                        // 3. 체력 감소 및 매니저 호출
                        boss.CurrentBossCount--;
                        bossManager.DamageToBoss();

                        // 4. 공격 성공 시 플레이어가 튕겨지는 효과 발생
                        Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
                        if (playerRb != null)
                        {
                            // 기존 y축 속도를 초기화한 뒤 위로 튕겨냄 (더 자연스러운 바운스)
                            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 0);
                            playerRb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
                        }
                    }
                }
            }
        }
    }
}