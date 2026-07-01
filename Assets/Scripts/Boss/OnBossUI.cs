using UnityEngine;

namespace SHIFTER
{
    public class OnBossUI : MonoBehaviour
    {
        [Header("보스 UI 목록")]
        [field: SerializeField]
        private GameObject[] BossUI;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Shifter")
            {
                for (int i = 0; i < BossUI.Length; i++)
                {
                    BossUI[i].gameObject.SetActive(true);
                }
            }
        }
    }
}
