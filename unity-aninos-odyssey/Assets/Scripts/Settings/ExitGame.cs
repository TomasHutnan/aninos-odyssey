using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AE.Settings
{
    public class ExitGame : MonoBehaviour
    {
        public void Exit()
        {
            print("Quitting to Desktop");
            Application.Quit();
        }
    }
}
