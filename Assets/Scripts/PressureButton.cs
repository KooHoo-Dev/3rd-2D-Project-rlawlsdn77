using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SHIFTER
{
    // 압력판처럼 플레이어가 밟으면 아래로 눌리는 연출을 재생하고,
    // 눌림 연출이 끝난 시점에 onPressed 이벤트를 호출합니다.
    public class PressureButton : MonoBehaviour
    {
        [SerializeField] private Transform visualTarget; // 눌리는 애니메이션을 적용할 스프라이트(자식 오브젝트). 비워두면 자기 자신
        [SerializeField] private float pressDepth = 0.15f;
        [SerializeField] private float pressDuration = 0.15f;
        [SerializeField] private UnityEvent onPressed;

        private Vector3 originalPos;

        private void Awake()
        {
            if (visualTarget == null) visualTarget = transform;
            originalPos = visualTarget.localPosition;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name != "Shifter") return;
            StopAllCoroutines();
            StartCoroutine(PressRoutine());
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.name != "Shifter") return;
            StopAllCoroutines();
            StartCoroutine(MoveTo(originalPos, pressDuration));
        }

        private IEnumerator PressRoutine()
        {
            yield return MoveTo(originalPos + Vector3.down * pressDepth, pressDuration);
            onPressed.Invoke();
        }

        private IEnumerator MoveTo(Vector3 target, float duration)
        {
            Vector3 start = visualTarget.localPosition;
            float elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                visualTarget.localPosition = Vector3.Lerp(start, target, elapsed / duration);
                yield return null;
            }
            visualTarget.localPosition = target;
        }
    }
}
