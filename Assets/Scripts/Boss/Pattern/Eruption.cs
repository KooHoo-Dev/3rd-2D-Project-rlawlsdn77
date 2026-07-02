using UnityEngine;

namespace SHIFTER
{
    public class Eruption : MonoBehaviour
    {
        private GameObject Volcano;

        private void Awake()
        {
            Volcano.SetActive(false);
        }
    }
}
