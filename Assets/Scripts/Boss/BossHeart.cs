using UnityEngine;

namespace SHIFTER
{
    public class BossHeart : MonoBehaviour
    {

        // 보스를 잡아야 하는 ㄴ목표를 우ㅏ ㄹ
        private Shifter Shifter;
        private Boss Boss;
        private BossManager BossManager;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Shifter")
            {
                Boss.CurrentBossCount--;

                BossManager.DamageToBoss();
            }
        }

    }
}
