using Study.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SHIFTER
{
    public class Portal : MonoBehaviour
    {
        private SceneChanger sceneChanger;
        [field: SerializeField] private GameObject player;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name == "Shifter")
            {
                if (SimpleInput.GetKeyDown(Key.W) || SimpleInput.GetKeyDown(Key.UpArrow))
                {
                    sceneChanger.ChangeSceneByName("STAGE 2");
                }
            }
        }
    }
}
