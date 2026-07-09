using UnityEngine;
using UnityEngine.Events;

namespace SHIFTER
{
    // 플레이어가 밟으면 즉시 onPressed 이벤트를 호출합니다.
    public class PressureButton : MonoBehaviour
    {
        [SerializeField] private UnityEvent onPressed;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name != "Shifter") return;
            onPressed.Invoke();
        }
    }
}
