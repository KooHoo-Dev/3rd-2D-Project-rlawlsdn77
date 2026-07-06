using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SHIFTER
{
    public class CountManager : MonoBehaviour
    {
        private BulletCount BulletCount;

        [SerializeField] private TMP_InputField InputFieldWhite;
        [SerializeField] private TMP_InputField InputFieldBlack;
        [SerializeField] private GameObject WrongLog;

        public int InputWhite { get; private set; }
        public int InputBlack { get; private set; }

        private void CountWhiteBlack(int white, int black)
        {
            white = BulletCount.CountWhite;
            black = BulletCount.CountBlack;
        }

        public void InputToWhite(string inputwhite)
        {
            InputWhite = int.Parse(inputwhite);
        }

        public void InputToBlack (string inputblack) 
        {
           InputBlack = int.Parse(inputblack);
        }

        public void CheckWhiteBlack()
        {
            if (BulletCount.CountWhite == InputWhite && BulletCount.CountBlack == InputBlack) 
            {
                gameObject.SetActive(false);
            }

            else
            {
                WrongLog.SetActive(true);
            }
        }


    }
}
