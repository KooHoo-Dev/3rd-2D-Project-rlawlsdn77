using UnityEngine;

namespace SHIFTER
{
    public class Volcano : MonoBehaviour
    {
        private Boss Boss;
        private Shifter Shifter;

        private void OnTriggerEnter2D (Collider2D collision)
        {
            if (collision.gameObject.name == "Shifter")
            {
                Shifter.Hp -= Boss.BossAttack;
                Debug.Log("플레이어가 화산에 닿았습니다!");
            }
        }
    }
}
