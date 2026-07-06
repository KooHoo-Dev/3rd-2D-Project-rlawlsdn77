using Study.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SHIFTER
{
    public class OnCountUI : MonoBehaviour
    {
        [SerializeField] private GameObject Step_2_1Q;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Shifter")
            {
                Step_2_1Q.SetActive(true);
            }
        }


    }
}
