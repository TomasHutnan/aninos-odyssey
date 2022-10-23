using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AE.CharacterStats;
using AE.Items;

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

    public int Money = 0;

    public List<Item> inventory = new List<Item> { };
    public Dictionary<ItemType, Item> equippedItems = new Dictionary<ItemType, Item> { };

    public bool AddItem(Item item)
    {
        inventory.Add(item);
        return true;
    }
    public bool RemoveItem(Item item)
    {
        if (inventory.Remove(item))
        {
            return true;
        }
        return false;
    }

    public bool EquipItem(Item item)
    {
        if (equippedItems[item.Type])
        {
            AddItem(equippedItems[item.Type]);
        }

        equippedItems[item.Type] = item;
        RemoveItem(item);

        return true;
    }
    public bool UnequipItem(Item item)
    {
        if (!equippedItems[item.Type] == item)
            return false;

        AddItem(item);
        equippedItems[item.Type] = null;
        return true;
    }
}
