using UnityEngine;

namespace SHIFTER
{
    // 버튼을 누르면 카메라 전환 + 플레이어 이동 잠금(Shift는 허용) + 레이저 조준
    // LightSensor가 풀리면 자동으로 원래 상태로 복귀합니다.
    public class LaserPuzzleManager : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Camera puzzleCamera;
        [SerializeField] private ShifterController shifterController;
        [SerializeField] private LightBeam lightBeam;
        [SerializeField] private LightSensor lightSensor;

        private bool isAiming = false;

        // 버튼(PressureButton)의 onPressed에 연결
        public void EnterAimMode()
        {
            if (isAiming || lightSensor.IsSolved) return;
            isAiming = true;

            mainCamera.gameObject.SetActive(false);
            puzzleCamera.gameObject.SetActive(true);

            shifterController.SetMovementLocked(true);
            lightBeam.IsAiming = true;
        }

        private void Update()
        {
            if (!isAiming) return;
            if (lightSensor.IsSolved) ExitAimMode();
        }

        private void ExitAimMode()
        {
            isAiming = false;

            puzzleCamera.gameObject.SetActive(false);
            mainCamera.gameObject.SetActive(true);

            shifterController.SetMovementLocked(false);
            lightBeam.IsAiming = false;
        }
    }
}
