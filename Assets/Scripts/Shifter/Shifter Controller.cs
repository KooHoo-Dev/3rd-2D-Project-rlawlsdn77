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

        [Header("Shift 쿨타임")]
        [SerializeField] private float shiftCooldown = 2f;
        private float cooldownTimer = 0f;

        [Header("일시정지")]
        [SerializeField] private GameObject PauseUI;
        private bool isPaused = false;

        public int FacingDirection { get; private set; } = 1;

        private bool movementLocked = false;

        public static readonly int MOVEMENT = Animator.StringToHash("Movement");
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
            if (!movementLocked) MoveInput();
            ShiftInput();
            EscInput();
        }

        // 퍼즐 조준 모드 등에서 이동만 잠그고 Shift(태그 전환)는 그대로 허용할 때 사용
        public void SetMovementLocked(bool locked)
        {
            movementLocked = locked;

            if (locked)
            {
                animator.SetFloat(MOVEMENT, 0f);
                characterController.SetMoveInput(0f);
            }
        }

        // Shift 쿨다운 감소
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

        // Shift 키 입력을 통해 속성(태그) 변경
        private void ShiftInput()
        {
            if (SimpleInput.GetKeyDown(Key.LeftShift) || SimpleInput.GetKeyDown(Key.RightShift))
            {
                if (cooldownTimer <= 0f)
                {
                    cooldownTimer = shiftCooldown;

                    animator.SetTrigger(IS_SHIFT);
                    shifter.ShiftTag();
                }
            }
        }

        // Esc 키 입력 시 게임 일시정지 후 UI_Pause 활성화
        private void EscInput()
        {
            if (SimpleInput.GetKeyDown(Key.Escape))
            {
                if (!isPaused)
                {
                    Time.timeScale = 0f;
                    PauseUI.SetActive(true);
                    isPaused = true;
                }
                else
                {
                    Time.timeScale = 1f;
                    PauseUI.SetActive(false);
                    isPaused = false;
                }
            }
        }
    }
}