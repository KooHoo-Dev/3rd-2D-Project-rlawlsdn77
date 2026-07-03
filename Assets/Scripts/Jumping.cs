using UnityEngine;
using Study.Utilities;

namespace SHIFTER
{
    public class JumpPing : MonoBehaviour
    {
        [SerializeField] private float upwardForce = 15f; // 위로 밀어내는 힘 (VerticalVelocity용)
        [SerializeField] private float forwardMultiplier = 1.5f; // 이동 배율

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other != null)
            {
                // 1. 플레이어 컴포넌트가 있는지 확인 (키네마틱 대응)
                var playerController = other.GetComponent<CharacterController2D>();

                if (playerController != null)
                {
                    // 플레이어 전용 발사 함수 호출
                    playerController.LaunchByJumpPad(upwardForce / 2, forwardMultiplier);
                    return; // 플레이어라면 처리를 끝냅니다.
                }

                // 2. 플레이어가 아닌 일반 다이내믹 오브젝트(상자, 적 등) 처리
                Rigidbody2D objectRb = other.GetComponent<Rigidbody2D>();
                if (objectRb != null && objectRb.bodyType == RigidbodyType2D.Dynamic)
                {
                    float savedXVelocity = objectRb.linearVelocity.x;
                    objectRb.linearVelocity = Vector2.zero;

                    Vector2 jumpForce = new Vector2(savedXVelocity * forwardMultiplier, upwardForce);
                    objectRb.AddForce(jumpForce, ForceMode2D.Impulse);
                }
            }
        }
    }
}