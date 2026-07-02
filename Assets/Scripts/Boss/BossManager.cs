using UnityEngine;
using System.Collections.Generic; 

namespace SHIFTER
{
    // 보스의 패턴 관리 및 그로기 상태 전환을 담당하는 스크립트

    // 패턴은 일정 시간 간격으로 변경되며, 3번의 패턴 실행 후 보스는 그로기 상태로 전환됩니다.
    // 패턴마다 무작위 태그의 플랫폼이 다량 생성되고 패턴 종료시 생성된 플랫폼은 제거
    // 패턴 : Eruption (용암분출) , Razer (레이저) , Rampage (무차별 난사)
    // Eruption : 바닥이 용암으로 변하며 초당 10의 지속 입힘. 하늘에서는 운석이 떨어지며 플레이어 공격
    // Razer : 보스가 레이저를 5 ~ 10회 발사하여 플레이어 공격
    // Rampage : 보스가 보스 오브젝트의 중심에서 무차별 난사를 하며 플레이어 공격
    // 그로기 : 보스가 일정 시간 동안 약점이 드러나며 플레이어가 약점을 밟으면 보스 체력 감소

    public class BossManager : MonoBehaviour
    {
        [Header("참조 스크립트")]
        [SerializeField] private Shifter Shifter;
        [SerializeField] private Boss Boss;

        [Header("패턴 설정")]
        [SerializeField] private float PatternInterval = 10f; // 패턴 간격
        [SerializeField] private float PatternTimer = 0f;     // 패턴 타이머
        private int patternCount = 0;                         // 패턴 실행 횟수 카운트

        [Header("보스 상태 오브젝트")]
        [SerializeField] private GameObject BossHeart;        // 그로기 상태 시 드러나는 보스 약점

        private bool isGroggy = false; // 현재 그로기 상태인지 체크하는 플래그

        // [추가] 생성된 플랫폼들을 관리할 리스트
        private List<GameObject> activePlatforms = new List<GameObject>();

        private void Start()
        {
            if (Boss == null) Boss = FindFirstObjectByType<Boss>();
            if (Shifter == null) Shifter = FindFirstObjectByType<Shifter>();

            if (BossHeart != null) BossHeart.SetActive(false);
        }

        private void Update()
        {
            if (!isGroggy)
            {
                PatternManager();
            }
        }

        private void PatternManager()
        {
            PatternTimer += Time.deltaTime;
            if (PatternTimer >= PatternInterval)
            {
                PatternTimer = 0f;
                ChangePattern();
            }
        }

        private void ChangePattern()
        {
            // 패턴 종료 시 생성된 플랫폼 제거
            ClearPlatforms();

            patternCount++;
            Debug.Log($"보스가 {patternCount}번째 패턴을 실행합니다.");

            if (patternCount > 3) // 3번 실행 '후' 그로기 전환 (4번째 타이밍)
            {
                StartGroggy();
                return;
            }

            // 패턴 무작위 선택
            int randomPattern = Random.Range(0, 3);
            switch (randomPattern)
            {
                case 0:
                    Debug.Log("패턴 : Eruption (용암분출) - 초당 10의 지속 피해 및 운석 공격");
                    // TODO: Eruption 로직 구현
                    break;
                case 1:
                    Debug.Log("패턴 : Razer (레이저) - 5~10회 레이저 발사");
                    // TODO: Razer 로직 구현
                    break;
                case 2:
                    Debug.Log("패턴 : Rampage (무차별 난사) - 중심에서 무차별 난사");
                    // TODO: Rampage 로직 구현
                    break;
            }

            // 무작위 태그의 플랫폼 다량 생성 (예시 뼈대)
            GenerateRandomTagPlatforms();
        }

        // 그로기 상태에서 공격 성공시 보스 체력 감소 및 플레이어 체력 회복, 보스 공격력 증가
        // BossHeart 스크립트에서 플레이어가 약점을 밟았을 때 직접 호출됩니다.
        public void DamageToBoss()
        {
            if (Boss == null) return;

            if (Shifter != null) Shifter.Hp += 30; // 플레이어 체력 회복
            Boss.BossAttack += 5; // 보스 공격력 증가

            BossDead();

            // (선택 사항) 피격 성공 시 즉시 그로기를 해제하려면 주석 해제
            // EndGroggy();
        }

        private void StartGroggy()
        {
            if (isGroggy) return;
            isGroggy = true;
            patternCount = 0;

            if (BossHeart != null) BossHeart.SetActive(true);
            Debug.Log("보스가 그로기 상태에 빠졌습니다! 약점 노출!");

            // 5초 뒤 그로기 자동 해제
            Invoke(nameof(EndGroggy), 5f);
        }

        private void EndGroggy()
        {
            isGroggy = false;
            if (BossHeart != null) BossHeart.SetActive(false);
            Debug.Log("그로기 상태가 종료되었습니다.");
        }

        private void BossDead()
        {
            if (Boss.CurrentBossCount <= 0)
            {
                Debug.Log("보스 격파 완료!");
                // TODO: 보스 사망 애니메이션 재생, 결과창 띄우기 등의 처리
            }
        }

        // 플랫폼 생성 및 제거 로직 뼈대 추가
        private void GenerateRandomTagPlatforms()
        {
            // 플랫폼 프리팹을 생성하고 태그를 랜덤 부여한 뒤 activePlatforms 리스트에 추가
        }

        private void ClearPlatforms()
        {
            foreach (var platform in activePlatforms)
            {
                if (platform != null) Destroy(platform);
            }
            activePlatforms.Clear();
        }
    }
}