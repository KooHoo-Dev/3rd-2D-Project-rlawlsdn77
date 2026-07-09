using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Study.Utilities
{
    // 키보드/마우스 입력을 어디서든 호출할 수 있도록 묶어놓은 정적 유틸 클래스
    public static class SimpleInput
    {
        #region 키보드 입력

        public static bool GetKeyDown(Key key)
        {
            return Keyboard.current[key].wasPressedThisFrame;
        }
        public static bool GetKeyUp(Key key)
        {
            return Keyboard.current[key].wasReleasedThisFrame;
        }
        public static bool GetKey(Key key)
        {
            return Keyboard.current[key].isPressed;
        }
        public static bool AnyKeyDown()
        {
            return Keyboard.current.anyKey.wasPressedThisFrame;
        }

        #endregion

        #region 마우스 입력

        public enum MouseButton { Left = 0, Right = 1, Middle = 2 }

        private static ButtonControl GetMouseControl(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return Mouse.current.leftButton;
                case MouseButton.Right:
                    return Mouse.current.rightButton;
                case MouseButton.Middle:
                    return Mouse.current.middleButton;
            }

            return null;
        }

        public static bool GetMouseButtonDown(MouseButton button)
        {
            return GetMouseControl(button).wasPressedThisFrame;
        }

        public static bool GetMouseButtonUp(MouseButton button)
        {
            return GetMouseControl(button).wasReleasedThisFrame;
        }

        public static bool GetMouseButton(MouseButton button)
        {
            return GetMouseControl(button).isPressed;
        }

        public static Vector2 GetMousePosition()
        {
            return Mouse.current.position.ReadValue();
        }

        // 마우스가 한 프레임에 이동한 위치 변화량
        public static Vector2 GetMouseDelta()
        {
            return Mouse.current.delta.ReadValue();
        }

        public static float GetMouseScrollDeltaRaw()
        {
            return Mouse.current.scroll.ReadValue().y;
        }

        // 마우스 휠 스크롤을 -1, 0, 1 단위로 반환합니다.
        public static float GetMouseScrollDelta()
        {
            float raw = GetMouseScrollDeltaRaw();
            return Mathf.Approximately(raw, 0f) ? 0f : Mathf.Sign(raw);
        }

        #endregion

        #region 축 입력 (WASD / 방향기)

        // A/D 또는 좌우 화살표 입력을 -1, 0, 1로 반환
        public static float GetAxisHorizontalRaw()
        {
            float axis = 0.0f;

            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) 
                axis += 1.0f;

            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) 
                axis -= 1.0f;

            return axis;
        }

        // W/S 또는 위아래 화살표 입력을 -1, 0, 1로 반환
        public static float GetAxisVerticalRaw()
        {
            float axis = 0.0f;

            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
                axis += 1.0f;

            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
                axis -= 1.0f;

            return axis;
        }

        // 수평/수직 입력을 합쳐 정규화된 이동 벡터로 반환 (대각선 이동 속도 보정 포함)
        public static Vector2 GetMoveAxisRaw()
        {
            Vector2 axis = new Vector2();
            axis.x = GetAxisHorizontalRaw();
            axis.y = GetAxisVerticalRaw();

            if (axis.sqrMagnitude > 1f) return axis.normalized;
            return axis;
        }

        #endregion
    }
}