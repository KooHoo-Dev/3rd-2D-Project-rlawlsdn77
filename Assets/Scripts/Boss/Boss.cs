using UnityEngine;
using Unity;

namespace SHIFTER
{
    // 보스의 체력 및 공격력 관리 스크립트
    public class Boss : MonoBehaviour
    {
        public int CurrentBossCount = 1;
        public int MaxBossCount = 3;
        public float BossAttack = 10;

        private void Awake()
        {
            CurrentBossCount = MaxBossCount; 
        }

        private void Update()
        {
            
        }

    }
}
