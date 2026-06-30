using Study.Utilities;
using System;
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

        [SerializeField] private float shiftCooldown = 2f;
        private float cooldownTimer = 0f;
        

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
            UpdateCooldown();
            MoveInput();
            ShiftInput();
        }

        // Shift 쿨다운 함순
        private void UpdateCooldown()
        {
            if (cooldownTimer > 0) cooldownTimer -= Time.deltaTime;
        }

        // 좌우이동 + 점프
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
                transform.localScale = new Vector3(FacingDirection, 1, 1);
            }

            if (SimpleInput.GetKeyDown(Key.Space)) characterController.RequestJump();
        }

        // Shift 키 입력을 통해 속성(태그) 변경 + 로그 출력
        private void ShiftInput()
        {
            if (SimpleInput.GetKeyDown(Key.LeftShift) || SimpleInput.GetKeyDown(Key.RightShift))
            {
                if (cooldownTimer <= 0f)
                {
                    cooldownTimer = shiftCooldown;

                    animator.SetTrigger(IS_SHIFT);
                    shifter.ShiftTag();

                    Debug.Log($"현재 플레이어의 속성은 {gameObject.tag} 입니다.");
                }
            }
        }

    }
}