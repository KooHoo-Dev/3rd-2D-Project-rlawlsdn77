using UnityEngine;

namespace SHIFTER
{
    public class OnBossUI : MonoBehaviour
    {
        [Header("보스 UI 목록")]
        [field: SerializeField]
        private GameObject[] BossUI;

        [Header("플레이어")]
        [field : SerializeField]
        private GameObject Player;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == Player)
            {
                for (int i = 0; i < BossUI.Length; i++)
                {
                    BossUI[i].gameObject.SetActive(true);
                }
            }
        }
    }
}
