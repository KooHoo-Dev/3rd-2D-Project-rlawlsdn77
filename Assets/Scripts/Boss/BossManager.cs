using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SHIFTER
{
    // 보스의 패턴 관리 및 그로기 상태 전환을 담당하는 스크립트
    // 패턴은 일정 시간 간격으로 변경되며, 3번의 패턴 실행 후 보스는 그로기 상태로 전환됩니다.
    public class BossManager : MonoBehaviour
    {
        // 패턴 참조
        private BossPattern_Eruption eruptionPattern;
        private BossPattern_Razer razerPattern;
        private BossPattern_Rampage rampagePattern;

        [Header("패턴 설정")]
        [SerializeField] private float PatternInterval = 10f; // 패턴 간격
        private float PatternTimer = 0f;                       // 패턴 타이머
        private int patternCount = 0;                          // 패턴 실행 횟수 카운트

        [Header("보스 약점")]
        [SerializeField] private BossHeart bossHeart;

        [Header("보스 문")]
        [SerializeField] private GameObject BossOutdoor;

        private Animator bossAnimator;

        private const string ANIM_ATK1_ERUPTION = "Atk1";
        private const string ANIM_ATK2_RAMPAGE = "Atk2";
        private const string ANIM_ATK3_RAZER = "Atk3";
        private const string ANIM_DEATH = "Death";
        private const string ANIM_IDLE_STATE = "boss_idle";

        // 보스 사망 애니메이션 길이
        [SerializeField] private float deathAnimationDuration = 2.1f;

        [Header("플랫폼 생성 설정")]
        [SerializeField] private GameObject platformPrefab;   // 플랫폼 프리팹
        [SerializeField] private int platformCountPerPattern = 5;
        [SerializeField] private float platformSpawnRadius = 6f;

        private Shifter shifter;
        private Boss boss;

        private bool isGroggy = false;
        private bool encounterStarted = false;
        public bool EncounterStarted => encounterStarted; // 보스 타이머 UI 등 외부에서 조회용

        private IBossPattern currentPattern = null;
        private List<GameObject> activePlatforms = new List<GameObject>();

        private void Start()
        {
            boss = FindFirstObjectByType<Boss>();
            shifter = FindFirstObjectByType<Shifter>();

            eruptionPattern = GetComponent<BossPattern_Eruption>();
            razerPattern = GetComponent<BossPattern_Razer>();
            rampagePattern = GetComponent<BossPattern_Rampage>();

            bossHeart = GetComponentInChildren<BossHeart>(true);
            bossHeart.gameObject.SetActive(false);

            bossAnimator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            if (!encounterStarted) return;
            if (!isGroggy) PatternManager();
        }

        // OnBossUI(보스룸 진입 트리거)가 호출하여 보스 패턴을 시작시킵니다.
        public void BeginEncounter()
        {
            if (encounterStarted) return;
            encounterStarted = true;
            PatternTimer = 0f;
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
            // 이전 패턴 종료 및 플랫폼 제거
            if (currentPattern != null)
            {
                currentPattern.EndPattern();
            }
            ClearPlatforms();

            patternCount++;

            // 3번 실행 후 그로기 상태로 전환
            if (patternCount > 3)
            {
                StartGroggy();
                return;
            }

            // 패턴 무작위 선택
            int randomPattern = Random.Range(0, 3);
            switch (randomPattern)
            {
                case 0:
                    StartEruptionPattern();
                    break;
                case 1:
                    StartRazerPattern();
                    break;
                case 2:
                    StartRampagePattern();
                    break;
            }

            // 무작위 태그의 플랫폼 생성
            GenerateRandomTagPlatforms();
        }

        #region === 패턴 실행 메서드 ===

        private void StartEruptionPattern()
        {
            bossAnimator.SetTrigger(ANIM_ATK1_ERUPTION);
            currentPattern = eruptionPattern;
            eruptionPattern.StartPattern();
        }

        private void StartRazerPattern()
        {
            bossAnimator.SetTrigger(ANIM_ATK3_RAZER);
            currentPattern = razerPattern;
            razerPattern.StartPattern();
        }

        private void StartRampagePattern()
        {
            bossAnimator.SetTrigger(ANIM_ATK2_RAMPAGE);
            currentPattern = rampagePattern;
            rampagePattern.StartPattern();
        }

        #endregion

        #region === 그로기 상태 관리 ===

        public void DamageToBoss()
        {
            shifter.Hp += 30; // 플레이어 체력 회복
            BossDead();
        }

        private void StartGroggy()
        {
            if (isGroggy) return;
            isGroggy = true;
            patternCount = 0;

            // 현재 패턴 종료
            if (currentPattern != null)
            {
                currentPattern.EndPattern();
                currentPattern = null;
            }

            ForceIdleAnimation(); // 공격 애니메이션이 남지 않도록 Idle로 강제 전환

            bossHeart.gameObject.SetActive(true); // 약점 활성화 (속성 상관없이 밟으면 공격 성공)

            // 5초 뒤 그로기 자동 해제
            Invoke(nameof(EndGroggy), 5f);
        }

        private void EndGroggy()
        {
            isGroggy = false;
            bossHeart.gameObject.SetActive(false);
        }

        // 대기 중인 공격 트리거를 모두 지우고 Idle 스테이트로 즉시 강제 전환 (Exit Time을 기다리지 않음)
        private void ForceIdleAnimation()
        {
            bossAnimator.ResetTrigger(ANIM_ATK1_ERUPTION);
            bossAnimator.ResetTrigger(ANIM_ATK2_RAMPAGE);
            bossAnimator.ResetTrigger(ANIM_ATK3_RAZER);
            bossAnimator.Play(ANIM_IDLE_STATE);
        }

        private void BossDead()
        {
            if (boss.CurrentBossCount <= 0)
            {
                StartCoroutine(PlayDeathAndDisappear());
                BossOutdoor.SetActive(false);
            }
        }

        // boss_death 애니메이션을 재생한 뒤, 마지막 프레임까지 끝나면 보스를 사라지게 합니다.
        private IEnumerator PlayDeathAndDisappear()
        {
            // 남아있던 그로기/타이머/패턴 정리
            CancelInvoke(nameof(EndGroggy));
            isGroggy = false;
            encounterStarted = false;

            if (currentPattern != null)
            {
                currentPattern.EndPattern();
                currentPattern = null;
            }
            ClearPlatforms();
            bossHeart.gameObject.SetActive(false);
            bossAnimator.SetTrigger(ANIM_DEATH);

            // boss_death 클립 길이만큼 대기 (클립 길이가 바뀌면 deathAnimationDuration도 맞춰 조정)
            yield return new WaitForSeconds(deathAnimationDuration);

            // 마지막 프레임까지 끝났으면 그냥 사라짐
            gameObject.SetActive(false);
        }

        #endregion

        #region === 플랫폼 생성 및 제거 ===

        // 무작위 속성(태그)을 가진 플랫폼 생성 (색상도 속성에 맞게 함께 적용)
        private void GenerateRandomTagPlatforms()
        {
            if (platformPrefab == null)
            {
                Debug.LogWarning("플랫폼 프리팹이 설정되지 않았습니다!");
                return;
            }

            for (int i = 0; i < platformCountPerPattern; i++)
            {
                // 보스 주변 랜덤 위치에 생성
                float randomAngle = Random.Range(0f, 360f);
                float randomDistance = Random.Range(2f, platformSpawnRadius);

                Vector3 spawnPos = transform.position + new Vector3(
                    Mathf.Cos(randomAngle * Mathf.Deg2Rad) * randomDistance,
                    Mathf.Sin(randomAngle * Mathf.Deg2Rad) * randomDistance,
                    0
                );

                // 플랫폼 생성
                GameObject platform = Instantiate(platformPrefab, spawnPos, Quaternion.identity);

                // 무작위 속성(White/Black만 사용) 및 그에 맞는 색상 적용
                // Common은 항상 통과 가능한 발판이 되어 셔프트 챌린지 의미가 없으므로 제외합니다.
                string randomTag = BossAttributeUtil.RandShiftTag();
                BossAttributeUtil.Apply(platform, randomTag);

                activePlatforms.Add(platform);
            }
        }

        // 생성된 플랫폼 모두 제거
        private void ClearPlatforms()
        {
            foreach (var platform in activePlatforms)
            {
                if (platform != null) Destroy(platform);
            }
            activePlatforms.Clear();
        }

        #endregion
    }
}
