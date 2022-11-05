using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AE.Items;
using System;
using AE.CharacterStats;
using AE.FightManager;
using Abilities;
using AE.Abilities;

using static AbilityStorage;
using System.Linq;

public class EnemyGeneration 
{

   
    public static Character Generate(ItemTier Stage, int EnemyLevel,ItemClass EnemyClass, bool HaveItems = true)
    {
        
        int[] priority = new int[] {60,85,95,100 };

        Character character = new Character();

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




        if (!HaveItems) { return character; }
        
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

            AE.Items.Item CurrentItem = new AE.Items.Item(itemClass, Stage, item);
            
            
            character.AddItem(CurrentItem);
            character.EquipItem(CurrentItem);
            
        }
        return character;
       

        


    }
    public static void SetLevels(Character character,int EnemyLevel, ItemClass EnemyClass)
    {
        int[] priority = new int[] { 60, 85, 95, 100 };
        Dictionary<ItemClass, Dictionary<ItemClass, List<int>>> GearChance = new Dictionary<ItemClass, Dictionary<ItemClass, List<int>>> {
               {ItemClass.Tank,new Dictionary<ItemClass,List<int>>(){{ItemClass.Tank,new List<int> { 0, priority[0] } }, { ItemClass.Fighter, new List<int> { priority[0], priority[1] } }, { ItemClass.Rogue, new List<int> { priority[1], priority[2] } }, { ItemClass.Priest, new List<int> { priority[2], priority[3] } } } },//Tank
            {ItemClass.Fighter,new Dictionary<ItemClass,List<int>>(){{ItemClass.Tank,new List<int> { priority[0], priority[1] } }, { ItemClass.Fighter, new List<int> { 0, priority[0] } }, { ItemClass.Rogue, new List<int> { priority[1], priority[2] } }, { ItemClass.Priest, new List<int> { priority[2], priority[3] } } } },//Fighter
              {ItemClass.Rogue,new Dictionary<ItemClass,List<int>>(){{ItemClass.Tank,new List<int> { priority[2], priority[3] } }, { ItemClass.Fighter, new List<int> { priority[1], priority[2] } }, { ItemClass.Rogue, new List<int> { 0, priority[0] } }, { ItemClass.Priest, new List<int> { priority[0], priority[1] } } } },//Rouge
             {ItemClass.Priest,new Dictionary<ItemClass,List<int>>(){{ItemClass.Tank,new List<int> { priority[2], priority[3] } }, { ItemClass.Fighter, new List<int> { priority[1], priority[2] } }, { ItemClass.Rogue, new List<int> { priority[0], priority[1] } }, { ItemClass.Priest, new List<int> { 0, priority[0] } } } },//Priest
        };
        Dictionary<ItemClass, List<int>> ClassLevels = new Dictionary<ItemClass, List<int>>()
        {
            {ItemClass.Tank,new List<int> { 0,0 } },
            {ItemClass.Fighter,new List<int> { 0,0 } },
            {ItemClass.Rogue,new List<int> { 0,0 } },
            {ItemClass.Priest,new List<int> { 0,0 } },
        };
        for (int i = 0; i < EnemyLevel; i++)
        {
            int Dice = UnityEngine.Random.Range(1, 100);
            int stat = UnityEngine.Random.Range(0, 2);

            if (Dice > GearChance[EnemyClass][ItemClass.Tank][0] & Dice <= GearChance[EnemyClass][ItemClass.Tank][1]) { ClassLevels[EnemyClass][stat] += 1; }
            else if (Dice > GearChance[EnemyClass][ItemClass.Rogue][0] & Dice <= GearChance[EnemyClass][ItemClass.Rogue][1]) { ClassLevels[EnemyClass][stat] += 1; }
            else if (Dice > GearChance[EnemyClass][ItemClass.Fighter][0] & Dice <= GearChance[EnemyClass][ItemClass.Fighter][1]) { ClassLevels[EnemyClass][stat] += 1; }
            else if (Dice > GearChance[EnemyClass][ItemClass.Priest][0] & Dice <= GearChance[EnemyClass][ItemClass.Priest][1]) { ClassLevels[EnemyClass][stat] += 1; }

        }
        List<AbilityName> UnlockedAbilities = character.UnlockedAbilities.ToList();
        character.LevelUpSystem = new LevelUpSystem(character, EnemyLevel, 0, ClassLevels[ItemClass.Fighter][0], ClassLevels[ItemClass.Rogue][0], ClassLevels[ItemClass.Tank][0] + ClassLevels[ItemClass.Priest][1], ClassLevels[ItemClass.Tank][1], ClassLevels[ItemClass.Rogue][1], ClassLevels[ItemClass.Fighter][1], ClassLevels[ItemClass.Priest][0]);
        for (int i = 0; i < EnemyLevel; i++)
        {
            MonoBehaviour.print("Vyberam");
            AbilityName[] choices = LevelUpAbilitiesProvider.GetChoices(character, i + 1);
            UnlockedAbilities.Add(choices[UnityEngine.Random.Range(0, 4)]);
        }
        MonoBehaviour.print("Printujem");
        for (int i = 0; i < 6; i++)
        {
            if (UnlockedAbilities.Count != 0)
            {
                MonoBehaviour.print("Pridavam");
                AbilityName chosenAbility = UnlockedAbilities[UnityEngine.Random.Range(0, UnlockedAbilities.Count)];
                character.EquippedAbilities.Add(chosenAbility);
                UnlockedAbilities.Remove(chosenAbility);
            }
            else
            {
                character.EquippedAbilities.Add(AbilityName.None);
            }
        }
    }
}
