using UnityEngine;

namespace SHIFTER
{
    // 레이저가 한 번이라도 명중하면 door1을 영구적으로 엽니다.
    public class LightSensor : MonoBehaviour
    {
        [SerializeField] private GameObject door1;

        public bool IsSolved { get; private set; } = false;

        // LightBeam이 매 프레임(조준 중일 때만) 호출
        public void UpdateBeam(bool beamHit)
        {
            if (IsSolved) return; // 한 번 풀리면 계속 유지
            if (!beamHit) return;

            IsSolved = true;
            door1.SetActive(false);
        }
    }
}
