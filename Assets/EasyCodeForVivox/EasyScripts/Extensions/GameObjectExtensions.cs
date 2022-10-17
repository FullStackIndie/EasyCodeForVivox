using UnityEngine;

namespace EasyCodeForVivox.Extensions
{
    public static class GameObjectExtensions
    {

        /// <summary>
        /// Deactivates this Gameobject and activates another Gameobject
        /// </summary>
        /// <param name="toDeactivate">Gameobject to Deactivate</param>
        /// <param name="toActivate">Gameobject to Activate</param>
        public static void SwitchTo(this GameObject toDeactivate, GameObject toActivate)
        {
            toDeactivate.SetActive(false);
            toActivate.SetActive(true);
        }


    }
}
