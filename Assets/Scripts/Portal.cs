using Study.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace SHIFTER
{
    public class Portal : MonoBehaviour
    {
        private int nextSceneIdx;
        private int currentSceneIdx;

        private void Awake()
        {
            currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
            nextSceneIdx = Mathf.Clamp(currentSceneIdx + 1, 0, SceneManager.sceneCountInBuildSettings - 1);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Shifter")
            {
                // 영역 안에서 키를 누르는 순간을 감지합니다.
                if (SimpleInput.GetKeyDown(Key.W) || SimpleInput.GetKeyDown(Key.UpArrow))
                {
                    SceneManager.LoadScene(nextSceneIdx);
                }
            }
        }
    }
}