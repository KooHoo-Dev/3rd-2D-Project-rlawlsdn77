using UnityEngine;
using UnityEngine.UI;

namespace SHIFTER
{
    public class Shifter : MonoBehaviour
    {
        private const int MaxHp = 100;
        public int Hp = 10;

        private const string TAG_COMMON = "Common";
        private const string TAG_WHITE = "White";
        private const string TAG_BLACK = "Black";
        private const string TAG_RED = "red";

        [field: SerializeField] private Image HpBar;


        public string currentTag { get; private set; }

        private void Start()
        {
            Hp = MaxHp;
            gameObject.tag = TAG_COMMON;
        }

        private void Update()
        {
            HpBar.fillAmount = Hp / MaxHp;

            if (Hp < 0)
            {
                Hp = 0;
                Debug.Log("플레이어가 사망하였습니다.");
            }
        }

        public void ShiftTag()
        {
            if (gameObject.CompareTag(TAG_COMMON) || gameObject.CompareTag(TAG_BLACK)) gameObject.tag = TAG_WHITE;
            else gameObject.tag = TAG_BLACK;

            currentTag = gameObject.tag;
        }
    } 
}
