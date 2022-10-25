using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRandomPlayerItem : MonoBehaviour
{
    public void CreateItem()
    {
        AE.Items.Item item = new AE.Items.Item();
        GameManager.PlayerCharacter.AddItem(item);
    }
}
