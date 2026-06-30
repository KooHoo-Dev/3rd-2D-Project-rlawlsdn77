using UnityEngine;
using UnityEngine.UI;

namespace SHIFTER
{
    public class Shifter : MonoBehaviour, ITakeDamage
    {
        [SerializeField] private int MaxHp = 100;
        [SerializeField] private float hp;

        public float Hp
        {
            get => hp;
            set
            {
                hp = Mathf.Clamp(value, 0, MaxHp);
                if (HpBar != null) HpBar.fillAmount = hp / MaxHp;
                if (hp <= 0) Die();
            }
        }

        private const string TAG_COMMON = "Common";
        private const string TAG_WHITE = "White";
        private const string TAG_BLACK = "Black";

        [SerializeField] private Image HpBar;
        public string currentTag { get; private set; }

        private void Start()
        {
            Hp = MaxHp;
            gameObject.tag = TAG_COMMON;
            currentTag = TAG_COMMON;
        }

        public void TakeDamage(float damage)
        {
            Hp -= damage;
            Debug.Log($"{gameObject.name} 피격! 현재 HP: {Hp}");
        }

        public void ShiftTag()
        {
            gameObject.tag = (gameObject.CompareTag(TAG_WHITE)) ? TAG_BLACK : TAG_WHITE;
            currentTag = gameObject.tag;
        }

        private void Die()
        {
            Debug.Log($"{gameObject.name}가 사망하였습니다.");
        }
    }
}