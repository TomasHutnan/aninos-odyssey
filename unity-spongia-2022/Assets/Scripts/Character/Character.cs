using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using AE.CharacterStats;
using AE.Items;
using AE.GameSave;
using Abilities;
public class Character : InventoryHolder
{
    public int Money;
    public event Action MoneyUpdateEvent;

    public Dictionary<ItemType, Item> EquippedItems;
    public event Action EquipmentUpdateEvent;

    public LevelUpSystem LevelUpSystem;

    public List<Ability> UnlockedAbilities = new List<Ability>();
    public List<Ability> EquippedAbilities = new List<Ability>();

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
        List<Ability> _unlockedAbilities = null, List<Ability> _equippedAbilities = null,
        int _baseDamage = 10,
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

        LevelUpSystem = _levelUpSystem ?? new LevelUpSystem(this);
        LevelUpSystem.UpdateActiveCharacter(this);

        UnlockedAbilities = _unlockedAbilities ?? new List<Ability>();
        EquippedAbilities = _equippedAbilities ?? new List<Ability>();

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
}
