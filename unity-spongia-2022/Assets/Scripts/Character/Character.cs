using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using AE.CharacterStats;
using AE.Items;
using AE.GameSave;
using Abilities;
using static AbilityStorage;

public class Character : InventoryHolder
{
    public int Money;
    public event Action MoneyUpdateEvent;

    public Dictionary<ItemType, Item> EquippedItems;
    public event Action EquipmentUpdateEvent;

    public LevelUpSystem LevelUpSystem;

    public HashSet<AbilityName> UnlockedAbilities;
    public HashSet<AbilityName> EquippedAbilities;

    public int LevelUpAbilitiesCount;

    public CharacterStat Damage;
    public CharacterStat CritChance;

    public CharacterStat HealthPoints;
    public CharacterStat DamageReduction;
    public CharacterStat DodgeChance;

    public CharacterStat Stamina;
    public CharacterStat Mana;

    public CharacterStat Weight;

    public bool EquipItem(Item item)
    {
        Item equippedItem;
        bool isEquipped = EquippedItems.TryGetValue(item.Type, out equippedItem);
        if (isEquipped && equippedItem is not null)
        {
            AddItem(equippedItem);
            equippedItem.Unequip(this);
        }

        EquippedItems[item.Type] = item;
        item.Equip(this);
        RemoveItem(item);

        EquipmentUpdateEvent?.Invoke();
        return true;
    }
    public bool UnequipItem(Item item)
    {
        Item equippedItem;
        bool isEquipped = EquippedItems.TryGetValue(item.Type, out equippedItem);

        if (!isEquipped || equippedItem != item)
            return false;

        AddItem(item);
        EquippedItems[item.Type] = null;
        equippedItem.Unequip(this);

        EquipmentUpdateEvent?.Invoke();
        return true;
    }

    public bool SellItem(Item item)
    {
        if (!RemoveItem(item))
            return false;

        Money += item.value;

        MoneyUpdateEvent?.Invoke();
        return true;
    }
    public bool BuyItem(Item item, int cost)
    {
        if (Money < cost)
            return false;

        AddItem(item);
        Money -= cost;

        MoneyUpdateEvent?.Invoke();
        return true;
    }

    public Character
        (
        int _money = 0, Dictionary<ItemType, Item> _equippedItems = null, List<Item> _inventory = null,
        LevelUpSystem _levelUpSystem = null,
        HashSet<AbilityName> _unlockedAbilities = null, HashSet<AbilityName> _equippedAbilities = null,
        int _levelUpAbilitiesCount = 0,
        int _baseDamage = 20,
        int _baseCritChance = 0,
        int _baseHealthPoints = 200,
        int _baseDamageReduction = 0,
        int _baseDodgeChance = 0,
        int _baseStamina = 100,
        int _baseMana = 100,
        int _baseWeight = 80
        )
    {
        Money = _money;
        EquippedItems = _equippedItems ?? new Dictionary<ItemType, Item>();
        Inventory = _inventory ?? new List<Item>();
        sortInventory();

        LevelUpSystem = _levelUpSystem ?? new LevelUpSystem();

        UnlockedAbilities = _unlockedAbilities ?? new HashSet<AbilityName>();
        EquippedAbilities = _equippedAbilities ?? new HashSet<AbilityName>();

        LevelUpAbilitiesCount = _levelUpAbilitiesCount;

        // Stat base value
        Damage = new CharacterStat(_baseDamage);
        CritChance = new CharacterStat(_baseCritChance);

        HealthPoints = new CharacterStat(_baseHealthPoints);
        DamageReduction = new CharacterStat(_baseDamageReduction);
        DodgeChance = new CharacterStat(_baseDodgeChance);

        Stamina = new CharacterStat(_baseStamina);
        Mana = new CharacterStat(_baseMana);

        Weight = new CharacterStat(_baseWeight);

        // Stat Labels
        Damage.Label = "Damage";
        CritChance.Label = "Crit Chance";
        CritChance.IsPercentual = true;

        HealthPoints.Label = "Health";
        DamageReduction.Label = "Resistance";
        DamageReduction.IsPercentual = true;
        DodgeChance.Label = "Dodge Chance";
        DodgeChance.IsPercentual = true;

        Stamina.Label = "Stamina";
        Mana.Label = "Mana";

        Weight.Label = "Weight";
    }

    public void PostInit()
    {
        LevelUpSystem.UpdateActiveCharacter(this);

        foreach (Item item in EquippedItems.Values)
        {
            item.Equip(this);
        }
    }
}
