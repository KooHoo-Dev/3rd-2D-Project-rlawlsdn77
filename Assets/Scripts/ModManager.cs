using Study.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace SHIFTER
{
    public class ModManager : MonoBehaviour 
    {
        public Dictionary<string, int> Mods = new Dictionary<string, int>() { { "common", 1 }, { "white", 1 }, { "black", 2 } };

        private void Update()
        {
            
        }
    }
}
