using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    public GameObject ItemSlotsGrid;

    private Character c;
    void Start()
    {
        c = PlayerUtils.GetCharacter();

        c.InventoryUpdateEvent -= RefreshUI;
    }

    private void OnDisable()
    {
        c.InventoryUpdateEvent -= RefreshUI;
    }

    void RefreshUI()
    {
        
    }
}
