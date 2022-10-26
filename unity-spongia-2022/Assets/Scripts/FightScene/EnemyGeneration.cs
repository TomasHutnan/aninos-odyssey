using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AE.Items;
using System;
using Unity.PlasticSCM.Editor.WebApi;
using AE.CharacterStats;

public class EnemyGeneration : MonoBehaviour
{
    private void Start()
    {
        Generate(ItemTier.Earth);
    }
    
    public void Generate(ItemTier Stage)
    {
        
        int[] priority = new int[] {60,85,95,100 };

        Character character = gameObject.GetComponent<Character>();
        //character.Weight = new CharacterStat(80);
        //character.CritChance = new CharacterStat(0);
        //character.HealthPoints = new CharacterStat(0);
        //character.Stamina = new CharacterStat(0);
        //character.StaminaRegen = new CharacterStat(0);
        //character.Damage = new CharacterStat(0);
        //character.DamageReduction = new CharacterStat(0);
        //character.DodgeChance = new CharacterStat(0);
        //character.Mana = new CharacterStat(0);
        //character.Inventory = new List<Item> { };
        //character.EquippedItems = new Dictionary<ItemType, Item> { };

        Array classes = ItemClass.GetValues(typeof(ItemClass));
        ItemClass EnemyClass = (ItemClass)classes.GetValue(UnityEngine.Random.Range(0, classes.Length));
        Dictionary<ItemType, Item> EnemyGear = new Dictionary<ItemType, Item>();
        //THIS IS SHITY REPLACE LATER
        Dictionary<ItemClass, Dictionary<ItemClass, List<int>>> GearChance = new Dictionary<ItemClass, Dictionary<ItemClass, List<int>>> {
               {ItemClass.Tank,new Dictionary<ItemClass,List<int>>(){{ItemClass.Tank,new List<int> { 0, priority[0] } }, { ItemClass.Fighter, new List<int> { priority[0], priority[1] } }, { ItemClass.Rogue, new List<int> { priority[1], priority[2] } }, { ItemClass.Priest, new List<int> { priority[2], priority[3] } } } },//Tank
            {ItemClass.Fighter,new Dictionary<ItemClass,List<int>>(){{ItemClass.Tank,new List<int> { priority[0], priority[1] } }, { ItemClass.Fighter, new List<int> { 0, priority[0] } }, { ItemClass.Rogue, new List<int> { priority[1], priority[2] } }, { ItemClass.Priest, new List<int> { priority[2], priority[3] } } } },//Fighter
              {ItemClass.Rogue,new Dictionary<ItemClass,List<int>>(){{ItemClass.Tank,new List<int> { priority[2], priority[3] } }, { ItemClass.Fighter, new List<int> { priority[1], priority[2] } }, { ItemClass.Rogue, new List<int> { 0, priority[0] } }, { ItemClass.Priest, new List<int> { priority[0], priority[1] } } } },//Rouge
             {ItemClass.Priest,new Dictionary<ItemClass,List<int>>(){{ItemClass.Tank,new List<int> { priority[2], priority[3] } }, { ItemClass.Fighter, new List<int> { priority[1], priority[2] } }, { ItemClass.Rogue, new List<int> { priority[0], priority[1] } }, { ItemClass.Priest, new List<int> { 0, priority[0] } } } },//Priest
        };

        foreach (ItemType item in ItemType.GetValues(typeof(ItemType)))
        {
            int Dice = UnityEngine.Random.Range(1, 100);
            ItemClass itemClass = EnemyClass;
            if (Dice > GearChance[EnemyClass][ItemClass.Tank][0] &Dice <= GearChance[EnemyClass][ItemClass.Tank][1]) { itemClass = ItemClass.Tank; }
            else if (Dice > GearChance[EnemyClass][ItemClass.Rogue][0] & Dice <= GearChance[EnemyClass][ItemClass.Rogue][1]) { itemClass = ItemClass.Rogue; }
            else if(Dice > GearChance[EnemyClass][ItemClass.Fighter][0] & Dice <= GearChance[EnemyClass][ItemClass.Fighter][1]) { itemClass = ItemClass.Fighter; }
            else if(Dice > GearChance[EnemyClass][ItemClass.Priest][0] & Dice <= GearChance[EnemyClass][ItemClass.Priest][1]) { itemClass = ItemClass.Priest; }

            Item CurrentItem = new Item(itemClass, Stage, item);
            print($"{CurrentItem.Weight},{itemClass}");
            
            EnemyGear.Add(item,CurrentItem );
            CurrentItem.Equip(character);
        }
       




    }
}
