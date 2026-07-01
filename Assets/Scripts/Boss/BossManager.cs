using Unity.VisualScripting;
using UnityEngine;

namespace SHIFTER
{
    public class BossManager : MonoBehaviour
    {
        private Shifter Shifter;
        private Boss Boss;

        [SerializeField]private float PatternInterval = 10f; // 패턴 간격
        [SerializeField]private float PatternTimer = 0f; // 패턴 타이머

        [SerializeField]private GameObject BossHeart; // 그로기 상태 시 드러나는 보스 약점

        private int Temp;
        private void Start()
        {
            Temp = Boss.CurrentBossCount;
        }


        private void Update()
        {
            PatternManager();
            DamageToBoss();
            Groggy();
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

        // 타이머가 초기화 될 때마다 패턴을 변경하는 함수
        private void ChangePattern()
        {

        }

        // 보스 체력 감소 감지 및 플레이어 체력 증가, 보스 공격력 증가
        public void DamageToBoss()
        {
            if (Temp == Boss.CurrentBossCount + 1) // 보스 체력카운트 감소 감지
            {
                Temp--; // 현재 체력과 동기화
                Shifter.Hp += 30;
                Boss.BossAttack += 5;
            }
        }

        /// <summary>
        /// 보스가 패턴 3번 행동 후 그로기 상태로 전환
        /// 보스의 위치 
        /// </summary>
        private void Groggy()
        {
            BossHeart.SetActive(true);
            
        }
        /// <summary>
        /// 보스 격파
        /// </summary>
        private void BossDead()
        {
            if (Boss.CurrentBossCount <= 0)
            {
                // 보스 격파 시 처리

            }
        }
    }
}
