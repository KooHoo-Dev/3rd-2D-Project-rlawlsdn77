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
#if UNITY_EDITOR
                // 유니티 에디터 끄기
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                // 실제 빌드된 게임이 종료될 때 실행
                Application.Quit(); 
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