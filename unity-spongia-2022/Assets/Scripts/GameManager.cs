using AE.GameSave;
using AE.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static Character PlayerCharacter;

    public static float ShopValueMultiplier = 10;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(transform);

            PlayerCharacter = transform.GetComponent<Character>();
        }
    }
}
