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
using Unity.VisualScripting.FullSerializer;
using Unity.VisualScripting;
using AE.Abilities.UI;

public class EnemyGeneration 
{
    public Enemy[] GetFightChoises(ItemTier Stage,int choiseAmount = 3)
    {
        List<string> EnemyNames = new List<string>() 
        {
            "Diomedes",
"Phokas",
"Oecleus",
"Attalos",
"Charillos",
"Crathis",
"Chares",
"Asklepios",
"Kannadis",
"Euaemon",
"Basileios",
"Epaphroditus",
"Dithyrambos",
"Proxenos",
"Iphikrates",
"Coes",
"Philopoemon",
"Kapaneus",
"Talaemenes",
"Palmys",
"Telemachos",
"Kerkyon",
"Hermolycus",
"Nausithous",
"Antikles",
"Onesiphorus",
"Hieronymus",
"Posidonios",
"Neoptolemos",
"Epaenetus",
"Panites",
"Pylenor",
"Thrasymedes",
"Eryximachos",
"Anacharsis",
"Lycidas",
"Ameinokles",
"Kandaules",
"Euphenes",
"Ibanolis",
"Orion",
"Eleon",
"Peolpidas",
"Echestratos",
"Timonax",
"Belos",
"Epeius",
"Coeranus",
"Acaeus",
"Terillos",
"Blathyllos",
"Cephalos",
"Clytius",
"Xiphilinus",
"Aberkios",
"Evelthon",
"Gorgythion",
"Sosipatros",
"Aster",
"Arridaios",
"Chromis",
"Coronos",
"Oenopion",
"Androbulos",
        };
        Dictionary<ItemTier,List<int> > StageLevels = new Dictionary<ItemTier, List<int>>() 
        {
            {ItemTier.Mortal, new List<int>(){0,6}},
            {ItemTier.Earth, new List<int>(){6,11}},
            {ItemTier.Heaven, new List<int>(){11,16}},
            {ItemTier.God, new List<int>(){16,21}},
        };
        Enemy[] choices = new Enemy[choiseAmount];
        Array classes = ItemClass.GetValues(typeof(ItemClass));
       
        for (int i = 0; i < choiseAmount; i++)
        {
            ItemClass EnemyClass = (ItemClass)classes.GetValue(UnityEngine.Random.Range(0, classes.Length));
            int EnemyLevel = UnityEngine.Random.Range(StageLevels[Stage][0], StageLevels[Stage][1]);
            Enemy character = Generate(Stage, EnemyLevel, EnemyClass);
            SetLevels(character,EnemyLevel,EnemyClass);
            character.Name = EnemyNames[UnityEngine.Random.Range(0, EnemyNames.Count)];
            character.Class = EnemyClass;
            character.Money = 10 * (int)Math.Pow((int)Stage+1, 3) * (UnityEngine.Random.Range(8, 12) / 10); 
        }
        
        return choices;
       

    }
    public static int[] priority = new int[] {60,85,95,100 };

    public static Dictionary<ItemClass, Dictionary<ItemClass, List<int>>> GearChance = new Dictionary<ItemClass, Dictionary<ItemClass, List<int>>> {
               {ItemClass.Tank,new Dictionary<ItemClass,List<int>>(){{ItemClass.Tank,new List<int> { 0, priority[0] } }, { ItemClass.Fighter, new List<int> { priority[0], priority[1] } }, { ItemClass.Rogue, new List<int> { priority[1], priority[2] } }, { ItemClass.Priest, new List<int> { priority[2], priority[3] } } } },//Tank
            {ItemClass.Fighter,new Dictionary<ItemClass,List<int>>(){{ItemClass.Tank,new List<int> { priority[0], priority[1] } }, { ItemClass.Fighter, new List<int> { 0, priority[0] } }, { ItemClass.Rogue, new List<int> { priority[1], priority[2] } }, { ItemClass.Priest, new List<int> { priority[2], priority[3] } } } },//Fighter
              {ItemClass.Rogue,new Dictionary<ItemClass,List<int>>(){{ItemClass.Tank,new List<int> { priority[2], priority[3] } }, { ItemClass.Fighter, new List<int> { priority[1], priority[2] } }, { ItemClass.Rogue, new List<int> { 0, priority[0] } }, { ItemClass.Priest, new List<int> { priority[0], priority[1] } } } },//Rouge
             {ItemClass.Priest,new Dictionary<ItemClass,List<int>>(){{ItemClass.Tank,new List<int> { priority[2], priority[3] } }, { ItemClass.Fighter, new List<int> { priority[1], priority[2] } }, { ItemClass.Rogue, new List<int> { priority[0], priority[1] } }, { ItemClass.Priest, new List<int> { 0, priority[0] } } } },//Priest
        };

    public static Enemy Generate(ItemTier Stage, int EnemyLevel,ItemClass EnemyClass, bool HaveItems = true)
    {



        Enemy character = new Enemy();

        if (!HaveItems) { return character; }
   
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
            AbilityName[] choices = LevelUpAbilitiesProvider.GetChoices(character, i + 1);
            bool AbilityChosen = false;
            foreach (AbilityName choice in choices)
            {

                Ability ability = GetAbility[choice];
                foreach (AbilityTags item in ability.AbilityTags)
                {
                    //checking if the ability class matches with the enemy class
                    //checking if the speel is better then lesser
                    if (item.ToString() == EnemyClass.ToString() || ability.AbilityLevel >= Level.Paradigm)
                    {
                        UnlockedAbilities.Add(choice);
                        AbilityChosen = true;
                        character.UnlockedAbilities.Add(choice);
                        break;
                    }
                }

                if (AbilityChosen)
                {
                    break;
                }
            }
            
            if (!AbilityChosen)
            {
                UnlockedAbilities.Add(choices[UnityEngine.Random.Range(0, 4)]);
            }
        }

        var placeHolder = from entry in UnlockedAbilities orderby GetAbility[entry].AbilityLevel descending select entry;
        List<AbilityName> SortedUnlockedAbilities = placeHolder.ToList();
        int index = 0;

        //Adding all posible abilities
        for (int i = 0; i < 6; i++)
        {
            if (UnlockedAbilities.Count != 0)
            {                
                List<AbilityName> ToDelete = new List<AbilityName>(SortedUnlockedAbilities);
                List <AbilityTags> AbilityChooseOrder= new List<AbilityTags>() { AbilityTags.Attack_Ability, AbilityTags.Defense_Ability, AbilityTags.Blessing_Ability};

                foreach (AbilityName currentAbility in ToDelete)
                {
                    Ability ability = GetAbility[currentAbility];
                    //if attack, defense, blessing abilities have been chosen
                    if (index == 3)
                    {
                        AbilityName chosenAbility = SortedUnlockedAbilities[UnityEngine.Random.Range(0, SortedUnlockedAbilities.Count)];
                        character.EquippedAbilities.Add(chosenAbility);
                        SortedUnlockedAbilities.Remove(chosenAbility);
                    }
                    //if not
                    else if (ability.AbilityType == AbilityChooseOrder[index])
                    {
                        character.EquippedAbilities.Add(currentAbility);
                        SortedUnlockedAbilities.Remove(currentAbility);
                        index += 1;
                    }
                }    
            }
            else
            {
                character.EquippedAbilities.Add(AbilityName.None);
            }
        }
    }
}
