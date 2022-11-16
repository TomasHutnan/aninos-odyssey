using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AE.GameSave;

namespace AE.SceneManagment
{
    public class SceneUtils : MonoBehaviour
    {
        public static void LoadScene(string sceneName, bool saveGame = false)
        {
            SceneManager.LoadScene(sceneName);
            if (saveGame)
            {
                SaveData.AutoSave();
            }
        }
    }
}
