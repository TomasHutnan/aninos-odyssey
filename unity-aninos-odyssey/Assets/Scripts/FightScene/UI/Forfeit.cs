using AE.SceneManagment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forfeit : MonoBehaviour
{
    public void GiveUp()
    {
        SceneUtils.LoadScene("MenuScene");
    }
}
