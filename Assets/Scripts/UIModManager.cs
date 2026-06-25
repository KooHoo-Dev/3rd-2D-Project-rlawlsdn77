using UnityEngine;

namespace SHIFTER
{
    public class UIModManager : MonoBehaviour
    {
        [SerializeField] private GameObject uiModCommon;
        [SerializeField] private GameObject uiModWhite;
        [SerializeField] private GameObject uiModBlack;

        [SerializeField] private GameObject player;

        private string lastTag = ""; 

        private void Start()
        {
            if (player == null)
            {
                player = GameObject.FindWithTag("Player");
            }

            UpdateUI("Common");
        }

        private void Update()
        {
            if (player == null) return;
            string currentTag = player.tag;
            if (currentTag != lastTag)
            {
                UpdateUI(currentTag);
                lastTag = currentTag;
            }
        }

        private void UpdateUI(string tag)
        {
            uiModCommon.SetActive(tag == "Common");
            uiModWhite.SetActive(tag == "White");
            uiModBlack.SetActive(tag == "Black");
        }
    }
}