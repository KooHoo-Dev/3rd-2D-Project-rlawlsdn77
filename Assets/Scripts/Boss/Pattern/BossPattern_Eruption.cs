using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SHIFTER
{
    // Eruption (용암분출) 패턴
    // 하늘에서 운석이 떨어지며 플레이어 공격
    public class BossPattern_Eruption : MonoBehaviour, IBossPattern
    {
        private BossManager bossManager;
        private Shifter shifter;
        private Transform bossTransform;

        [Header("운석")]
        [SerializeField] private GameObject meteorPrefab; // 운석 프리팹

        [SerializeField] private int meteorCount = 5; // 최대 운석 개수
        [SerializeField] private float meteorSpawnHeight = 10f; // 운석 생성 높이
        [SerializeField] private float meteorSpawnInterval = 0.5f; // 운석 생성 시간
        [SerializeField] private float meteorSpawnWidth = 8f; // 운석 생성 범위
        [SerializeField] private float meteorDamage = 10f; // 운석 피해량

        private List<GameObject> meteorList = new List<GameObject>();

        private void Start()
        {
            bossManager = FindFirstObjectByType<BossManager>();
            shifter = FindFirstObjectByType<Shifter>();
            bossTransform = bossManager.transform;

        }

        public void StartPattern()
        {
            meteorList.Clear();

            Debug.Log("운석 낙하 시작!");
            StartCoroutine(SpawnMeteorsRoutine());
        }

        public void EndPattern()
        {
            StopAllCoroutines(); // 운석이 떨어져도 즉시 중지

            // 운석 제거
            foreach (var meteor in meteorList)
            {
                if (meteor != null) Destroy(meteor);
            }
            meteorList.Clear();
        }

        // 운석 생성 코루틴
        private IEnumerator SpawnMeteorsRoutine()
        {
            for (int i = 0; i < meteorCount; i++)
            {
                // 보스 주변 랜덤 위치에 운석 생성
                float randomX = Random.Range(-meteorSpawnWidth, meteorSpawnWidth);
                Vector3 spawnPos = bossTransform.position + new Vector3(randomX, meteorSpawnHeight, 0);

                if (meteorPrefab != null)
                {
                    GameObject meteor = Instantiate(meteorPrefab, spawnPos, Quaternion.identity);
                    meteorList.Add(meteor);

                    // 무작위 속성(White/Black) 부여
                    string meteorTag = BossAttributeUtil.RandShiftTag();
                    BossProjectile meteorProjectile = meteor.GetComponent<BossProjectile>();
                    meteorProjectile.SetAttribute(meteorTag);
                    meteorProjectile.SetDamage(meteorDamage);

                    meteor.GetComponent<Rigidbody2D>().gravityScale = 2f; // 아래로 떨어지게 설정
                }

                yield return new WaitForSeconds(meteorSpawnInterval);
            }
        }
    }
}
