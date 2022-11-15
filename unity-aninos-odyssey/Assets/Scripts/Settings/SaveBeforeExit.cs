using AE.GameSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveBeforeExit : MonoBehaviour
{
    public void Save()
    {
        SaveData.AutoSave();
    }
}
