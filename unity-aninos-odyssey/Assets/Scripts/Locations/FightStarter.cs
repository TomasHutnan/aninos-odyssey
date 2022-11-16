using AE.SceneManagment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AE.Fight.UI
{
    public class FightStarter : MonoBehaviour
    {
        [SerializeField] Location fightLocation;
        [SerializeField] FightType fightType;

        public void StartFight()
        {
            FightData.Location = fightLocation;
            FightData.FightType = fightType;

            SceneUtils.LoadScene("FightScene", true);
        }
    }
}
