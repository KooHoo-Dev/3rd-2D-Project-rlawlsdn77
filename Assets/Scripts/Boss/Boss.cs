using UnityEngine;
using Unity;

namespace SHIFTER
{
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
