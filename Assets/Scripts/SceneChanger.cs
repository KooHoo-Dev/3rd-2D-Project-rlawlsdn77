using UnityEngine;
using UnityEngine.SceneManagement;

namespace SHIFTER
{
    public class SceneChanger : MonoBehaviour
    {
        private int currentSceneIdx;

        private void Awake()
        {
            currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
        }

        public void ChangeSceneByName(string sceneName)
        {
            Time.timeScale = 1.0f; // 게임 재개

            if (sceneName == "Exit")
            {
                Application.Quit(); // 게임 나가기
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; // 에디터에서는 Play 모드 종료
#endif
            }
            else if (sceneName == "Restart") // 현재 씬 재시작
            {
                SceneManager.LoadScene(currentSceneIdx);
            }
            else if (sceneName == "Menu") // 메인 메뉴로 이동
            {
                SceneManager.LoadScene("Menu");
            }
            else SceneManager.LoadScene(sceneName); // 씬 이름에 맞게 이동
        }
    }
}