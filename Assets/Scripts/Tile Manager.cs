using System.Collections.Generic;
using UnityEngine;

namespace SHIFTER
{
    public class TileManager : MonoBehaviour
    {
        private ModManager modManager;

        [Header("Tiles")]
        [SerializeField] public List<GameObject> tiles;

        private void Awake()
        {
            modManager = GetComponentInChildren<ModManager>();
            
        }
    }
}
