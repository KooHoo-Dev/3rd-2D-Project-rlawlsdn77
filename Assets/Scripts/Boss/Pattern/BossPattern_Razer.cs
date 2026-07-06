using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SHIFTER
{
    // Razer (레이저) 패턴
    public class BossPattern_Razer : MonoBehaviour, IBossPattern
    {
        private BossManager bossManager;
        private Shifter shifter;
        private Transform bossTransform;

        [Header("레이저 설정")]
        [SerializeField] private GameObject laserProjectilePrefab; // 레이저 프리팹
        [SerializeField] private int minLaserCount = 5; // 최소 레이저 개수
        [SerializeField] private int maxLaserCount = 10; // 최대 레이저 개수
        [SerializeField] private float laserSpawnInterval = 0.3f; // 레이저 발사 시간 간격
        [SerializeField] private float laserSpeed = 12f; // 레이저 속도
        [SerializeField] private float laserDamage = 8f; // 레이저 피해량

        private List<GameObject> activeLasers = new List<GameObject>();

        private void Start()
        {
            bossManager = FindFirstObjectByType<BossManager>();
            shifter = FindFirstObjectByType<Shifter>();
            bossTransform = bossManager.transform;
        }

        public void StartPattern()
        {
            activeLasers.Clear();
            StartCoroutine(FireLaserRoutine());
        }

        public void EndPattern()
        {
            StopAllCoroutines(); // 패턴 즉시 종료

            foreach (var laser in activeLasers) // 프리팹 제거
            {
                if (laser != null) Destroy(laser);
            }
            activeLasers.Clear();
        }

        // 레이저 발사 코루틴
        private IEnumerator FireLaserRoutine()
        {
            int laserCount = Random.Range(minLaserCount, maxLaserCount + 1);

            for (int i = 0; i < laserCount; i++)
            {
                FireLaser();
                yield return new WaitForSeconds(laserSpawnInterval);
            }
        }

        // 개별 레이저 발사
        private void FireLaser()
        {
            if (laserProjectilePrefab == null) return;

            // 오차각 만들기
            Vector3 directionToPlayer = (shifter.transform.position - bossTransform.position).normalized;
            float baseAngle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            float finalRadian = (baseAngle + Random.Range(-15f, 15f)) * Mathf.Deg2Rad;
            Vector3 firingDirection = new Vector3(Mathf.Cos(finalRadian), Mathf.Sin(finalRadian), 0);

            GameObject laser = Instantiate(laserProjectilePrefab, bossTransform.position, Quaternion.identity);
            activeLasers.Add(laser);

            string attributeTag = BossAttributeUtil.RandShiftTag();
            laser.GetComponent<BossProjectile>().Initialize(firingDirection, laserSpeed, laserDamage, attributeTag);
        }
    }
}
