using UnityEngine;

namespace SHIFTER
{
    // 보스의 체력 관리 스크립트
    public class Boss : MonoBehaviour
    {
        public int CurrentBossCount = 1;
        public int MaxBossCount = 3;

        private void Awake()
        {
            CurrentBossCount = MaxBossCount;
        }
    }
}
