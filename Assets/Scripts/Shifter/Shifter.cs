using UnityEngine;
using UnityEngine.UI;

namespace SHIFTER
{
    public class Shifter : MonoBehaviour
    {
        private const int MaxHp = 100;
        private float hp = 10.0f;

        public float Hp
        {
            get => hp;
            set
            {
                hp = Mathf.Clamp(value, 0, MaxHp);

                if (HpBar != null)
                {
                    HpBar.fillAmount = hp / MaxHp;
                }

                // 2. 사망 조건 체크
                if (hp <= 0)
                {
                    Debug.Log("플레이어가 사망하였습니다.");
                }
            }
        }

        private const string TAG_COMMON = "Common";
        private const string TAG_WHITE = "White";
        private const string TAG_BLACK = "Black";

        [field: SerializeField] private Image HpBar;

        public string currentTag { get; private set; }

        private void Start()
        {
            // 프로퍼티를 호출하여 초기 UI 상태도 자동으로 동기화
            Hp = MaxHp;
            gameObject.tag = TAG_COMMON;
            currentTag = TAG_COMMON;
        }

        public void ShiftTag()
        {   
            if (gameObject.CompareTag(TAG_COMMON) || gameObject.CompareTag(TAG_BLACK))
            {
                gameObject.tag = TAG_WHITE;
            }
            else
            {
                gameObject.tag = TAG_BLACK;
            }

            currentTag = gameObject.tag;
        }
    }
}