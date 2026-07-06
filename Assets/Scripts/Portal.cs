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

        // 포탈 안에 있는지 체크하는 플래그
        private bool isPlayerInside = false;

        private void Awake()
        {
            currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
            nextSceneIdx = (currentSceneIdx + 1) % SceneManager.sceneCountInBuildSettings;
        }

        private void Update()
        {
            // W or 윗방향키 입력 시 다음 씬으로 이동
            if (isPlayerInside)
            {
                if (SimpleInput.GetKeyDown(Key.W) || SimpleInput.GetKeyDown(Key.UpArrow))
                {
                    SceneManager.LoadScene(nextSceneIdx);
                }
            }
        }

        // 영역에 들어왔을 때
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Shifter")
            {
                isPlayerInside = true;
            }

        }

        // 영역에서 나갔을 때
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Shifter")
            {
                isPlayerInside = false;
            }
        }
    }
}