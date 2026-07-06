using Unity.VisualScripting;
using UnityEngine;

namespace SHIFTER
{
    public class OnBosspattern : MonoBehaviour
    {
        [Header("보스 UI 목록")]
        [field: SerializeField]
        private GameObject[] BossUI;

        [Header("보스 입구 문")]
        [SerializeField] private GameObject BossIndoor;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Shifter")
            {
                for (int i = 0; i < BossUI.Length; i++)
                {
                    BossUI[i].gameObject.SetActive(true);
                }

                BossIndoor.SetActive(true);

                // 보스 UI가 활성화 + 보스 패턴 시작
                BossManager bossManager = FindFirstObjectByType<BossManager>();
                if (bossManager != null) bossManager.BeginEncounter();

                Debug.Log("보스 방 진입");

                this.enabled = false;
            }
        }
    }
}
