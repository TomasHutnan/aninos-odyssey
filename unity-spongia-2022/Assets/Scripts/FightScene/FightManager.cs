using AE.FightManager;
using AE.GameSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    // Start is called before the first frame update
    public RealtimeStatsHolder EnemyFighter;
    public RealtimeStatsHolder PlayerFighter;

    private void Awake()
    {
       
        Character EnemyCharatcer = EnemyGeneration.Generate(SaveData.GameStage);
        EnemyCharatcer.PostInit();
        Character PlayerCharatcer = new Character();
        PlayerCharatcer.PostInit();
        EnemyFighter.SetCharacter(EnemyCharatcer);
        PlayerFighter.SetCharacter(PlayerCharatcer);
     

    }
    public void NextRound()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<RealtimeStatsHolder>().NextRound();

        }
    }
}
