using AE.FightManager;
using AE.GameSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AbilityStorage;

public class FightManager : MonoBehaviour
{
    // Start is called before the first frame update
    public RealtimeStatsHolder EnemyFighter;
    public RealtimeStatsHolder PlayerFighter;
    public GameObject EnemyGameObject;
    public List<AbilityName> EnemyAbilities = new List<AbilityName>();

    private void Start()
    {
        
        Character EnemyCharatcer = EnemyGeneration.Generate(SaveData.GameStage);
        EnemyCharatcer.PostInit();
        
        Character PlayerCharatcer = new Character();
        PlayerCharatcer.PostInit();
        EnemyBrain enemyBrain =  EnemyGameObject.GetComponent<EnemyBrain>();
        EnemyFighter.SetCharacter(EnemyCharatcer);
        PlayerFighter.SetCharacter(PlayerCharatcer);
        enemyBrain.Enemy = EnemyCharatcer;
        enemyBrain.Player = PlayerCharatcer;
        enemyBrain.List = EnemyAbilities;
        enemyBrain.Init();



    }
    public void NextRound()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<RealtimeStatsHolder>().NextRound();

        }
    }
}
