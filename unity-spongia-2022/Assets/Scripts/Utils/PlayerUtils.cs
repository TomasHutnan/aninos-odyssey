using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUtils : MonoBehaviour
{
    static Character _playerCharacter;
    public static Character GetCharacter()
    {
        if (_playerCharacter = null)
        {
            GameObject playerCharacterManager = GameObject.Find("PlayerCharacterManager");
            _playerCharacter = playerCharacterManager.GetComponent<Character>();
        }

        return _playerCharacter;
    }
}
