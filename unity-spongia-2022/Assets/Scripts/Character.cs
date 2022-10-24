using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AE.CharacterStats;
using AE.Items;
using AE.GameSave;

public class Character : MonoBehaviour
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

    public int Money;

    public List<Item> Inventory;
    public Dictionary<ItemType, Item> EquippedItems;

    public bool AddItem(Item item)
    {
        Inventory.Add(item);
        return true;
    }
    public bool RemoveItem(Item item)
    {
        if (Inventory.Remove(item))
        {
            return true;
        }
        return false;
    }

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
