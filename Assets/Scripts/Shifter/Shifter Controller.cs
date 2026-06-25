using Study.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SHIFTER
{
    [RequireComponent(typeof(CharacterController2D))]
    public class ShifterController : MonoBehaviour
    {
        private CharacterController2D characterController;
        private SpriteRenderer spriteRenderer;
        private Animator animator;
        private Shifter shifter;

        public int FacingDirection { get; private set; } = 1;

        public static readonly int MOVEMENT = Animator.StringToHash("Movement");
        public static readonly int IS_RUN = Animator.StringToHash("IsRun");
        public static readonly int IS_JUMP = Animator.StringToHash("IsJump");
        public static readonly int IS_DOWN = Animator.StringToHash("IsDown");
        public static readonly int IS_SHIFT = Animator.StringToHash("IsShift");

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            characterController = GetComponent<CharacterController2D>();
            animator = GetComponentInChildren<Animator>();
            shifter = GetComponent<Shifter>();
        }

        private void Update()
        {
            MoveInput();
            ShiftInput();
        }
        /// <summary>
        /// 좌우이동 + 점프
        /// 이동 시 해당 방향에 맞게 Scale 반전을 통해 Animation Flip
        /// </summary>
        private void MoveInput()
        {
            Vector2 inputVector = SimpleInput.GetMoveAxisRaw();
            float absMovement = Mathf.Abs(inputVector.x);
            animator.SetFloat(MOVEMENT, absMovement);

            characterController.SetMoveInput(inputVector.x);

            if (absMovement > 0) // 이동량이 있을때
            {
                // 삼항 연산자로 정면을 설정해준다
                FacingDirection = (inputVector.x < 0) ? -1 : 1;

                // x 스케일을 반전해서 좌우를 뒤집는다.
                animator.transform.localScale = new Vector3(FacingDirection, 1, 1);
            }

            if (SimpleInput.GetKeyDown(Key.Space)) characterController.RequestJump();
        }

        /// <summary>
        /// Shift 키 입력을 통해 속성(태그) 변경 + 로그 출력
        /// </summary>
        private void ShiftInput()
        {
            if (SimpleInput.GetKeyDown(Key.LeftShift) || SimpleInput.GetKeyDown(Key.RightShift))
            {
                animator.SetTrigger(IS_SHIFT);
                shifter.ShiftTag();

                Debug.Log($"현재 플레이어의 속성은 {gameObject.tag} 입니다.");
            }
        }

        private void PortalInput()
        {

        }
    }
}