using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AE.GameSave;

public class ResetAllPreferences : MonoBehaviour
{
    public void ResetAll()
    {
        Preferences.Defaults();
    }
}
