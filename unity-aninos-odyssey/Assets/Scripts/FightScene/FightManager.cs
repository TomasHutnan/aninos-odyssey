using AE.Fight;
using AE.FightManager;
using AE.GameSave;
using AE.Items;
using AE.SceneManagment;
using System;
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
    public Character EnemyCharatcer;
    public Character PlayerCharatcer;
    private void Start()
    {
        Array classes = ItemClass.GetValues(typeof(ItemClass));
        ItemClass EnemyClass = (ItemClass)classes.GetValue(UnityEngine.Random.Range(0, classes.Length));
        if (FightData.PreCreatedEnemy != null)
        {
            EnemyCharatcer = FightData.PreCreatedEnemy;
            FightData.PreCreatedEnemy = null;
        }
        else if(FightData.FightType == FightType.Tutorial)
        {
            EnemyCharatcer = EnemyGeneration.Generate(SaveData.GameStage, 5, EnemyClass,false);
            EnemyCharatcer.PostInit();
            EnemyGeneration.SetLevels(EnemyCharatcer, 0, EnemyClass);
            EnemyCharatcer.HealthPoints.BaseValue = 100;
            EnemyCharatcer.Money = 300;
        }
        else
        {
            EnemyCharatcer = EnemyGeneration.Generate(SaveData.GameStage, 5, EnemyClass);
            EnemyCharatcer.PostInit();
            EnemyGeneration.SetLevels(EnemyCharatcer, 5, EnemyClass);
        }
    
        PlayerCharatcer = SaveData.PlayerCharacter;
        
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
    public void EndRound()
    {
        PlayerFighter.NextRound();
        EnemyFighter.GetComponent<EnemyBrain>().MakeMove();
    }
    public void Victory()
    {
        print("VICTORY");
        print(EnemyCharatcer.EquippedItems.Values.Count);
        foreach (Item item in EnemyCharatcer.EquippedItems.Values)
        {
            print($"ADDING ITEM: {item.Name}");
            SaveData.PlayerCharacter.AddItem(item);
        }
        SaveData.PlayerCharacter.Money += EnemyCharatcer.Money;
        int expGain = 80 + (EnemyCharatcer.LevelUpSystem.Level) * 50;
        SaveData.PlayerCharacter.LevelUpSystem.addExp(expGain);
        SaveData.AutoSave();
        SceneUtils.LoadScene("GameScene", true);
    }
    public void Defeat()
    {
        print("Defeat");
        SceneUtils.LoadScene("MenuScene");
    }
    public void Update()
    {
        if (PlayerFighter.StatHolder[Stat.HealthPoints]<= 0)
        {
            PlayerFighter.StatHolder[Stat.HealthPoints] = 0;
           
            Defeat();
        }
        if(EnemyFighter.StatHolder[Stat.HealthPoints]<= 0)
        {
            EnemyFighter.StatHolder[Stat.HealthPoints] = 0;
            Victory();
        }
    }
}
