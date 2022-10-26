using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using AE.CharacterStats;
using AE.Items;
using AE.GameSave;

public class Character : Vendor
{
    public CharacterStat Damage;
    public CharacterStat CritChance;

    public CharacterStat HealthPoints;
    public CharacterStat DamageReduction;
    public CharacterStat DodgeChance;

    public CharacterStat Stamina;
    public CharacterStat StaminaRegen;
    public CharacterStat Mana;

    public CharacterStat Weight;

    public Dictionary<ItemType, Item> EquippedItems;



    public bool EquipItem(Item item)
    {
        if (EquippedItems[item.Type])
        {
            AddItem(EquippedItems[item.Type]);
        }

        EquippedItems[item.Type] = item;
        RemoveItem(item);

        return true;
    }
    public bool UnequipItem(Item item)
    {
        if (!EquippedItems[item.Type] == item)
            return false;

        AddItem(item);
        EquippedItems[item.Type] = null;

        return true;
    }

    void Awake()
    {
        if (transform.name == "PlayerCharacterManager")
        {
            Money = SaveData.Money;
            Inventory = SaveData.Inventory;
            EquippedItems = SaveData.EquippedItems;
        }
        else
        {
            Money = 0;
            Inventory = new List<Item> { };
            EquippedItems = new Dictionary<ItemType, Item> { };
        }
        defaultStats();
    }

    private void defaultStats()
    {
        Damage = new CharacterStat(1);
        CritChance = new CharacterStat(0);

        HealthPoints = new CharacterStat(20);
        DamageReduction = new CharacterStat(0);
        DodgeChance = new CharacterStat(0);

        Stamina = new CharacterStat(100);
        StaminaRegen = new CharacterStat(0);
        Mana = new CharacterStat(100);

        Weight = new CharacterStat(80);
    }
}
