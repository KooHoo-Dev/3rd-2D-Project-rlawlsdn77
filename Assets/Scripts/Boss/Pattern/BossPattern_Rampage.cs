using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SHIFTER
{
    // Rampage (무차별 난사) 패턴
    // 보스가 중심에서 무차별 난사를 하며 플레이어 공격
    public class BossPattern_Rampage : MonoBehaviour, IBossPattern
    {
        // 씬에 하나만 존재하는 참조들은 인스펙터에 노출하지 않고 자동으로 찾습니다.
        private BossManager bossManager;
        private Transform bossTransform;

        [Header("난사 설정")]
        [SerializeField] private GameObject projectilePrefab; // 탄환 프리팹
        [SerializeField] private int projectilesPerBurst = 8;      // 한 번에 발사하는 총알 개수
        [SerializeField] private int burstCount = 3;               // 펄스 반복 횟수
        [SerializeField] private float burstInterval = 1.5f;       // 펄스 간격
        [SerializeField] private float projectileSpeed = 10f; // 탄환 속도
        [SerializeField] private float projectileDamage = 6f; // 탄환 피해량

        private List<GameObject> activeProjectiles = new List<GameObject>();

        private void Start()
        {
            bossManager = FindFirstObjectByType<BossManager>();
            bossTransform = bossManager.transform;
        }

        public void StartPattern()
        {
            activeProjectiles.Clear();
            StartCoroutine(RampageRoutine());
        }

        public void EndPattern()
        {
            StopAllCoroutines(); // 패턴 즉시 종료

            foreach (var projectile in activeProjectiles) // 남은 프리팹 제거
            {
                if (projectile != null) Destroy(projectile);
            }
            activeProjectiles.Clear();
        }

        // 무차별 난사 코루틴
        private IEnumerator RampageRoutine()
        {
            for (int burst = 0; burst < burstCount; burst++)
            {
                FireBurst();
                yield return new WaitForSeconds(burstInterval);
            }
        }

        // 한 번의 펄스 발사
        private void FireBurst()
        {
            // 원 위의 여러 각도에서 동시에 발사
            float angleStep = 360f / projectilesPerBurst;

            for (int i = 0; i < projectilesPerBurst; i++)
            {
                float angle = i * angleStep;
                FireProjectileAtAngle(angle);
            }
        }

        // 특정 각도로 발사체 발사
        private void FireProjectileAtAngle(float angle)
        {
            if (projectilePrefab == null) return;

            // 각도 -> 방향 벡터
            float radian = angle * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0);

            // 발사체 생성, 회전은 BossProjectile.Initialize()가 방향에 맞게 설정
            GameObject projectile = Instantiate(projectilePrefab, bossTransform.position, Quaternion.identity);
            activeProjectiles.Add(projectile);

            // 무작위 속성(White/Black) 부여
            string attributeTag = BossAttributeUtil.RandShiftTag();
            projectile.GetComponent<BossProjectile>().Initialize(direction, projectileSpeed, projectileDamage, attributeTag);
        }
    }
}
