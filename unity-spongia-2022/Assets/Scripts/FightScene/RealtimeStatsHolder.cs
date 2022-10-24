using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RealtimeStatsHolder : MonoBehaviour
{
    public float HealthPoints;
    public float CritChance;
    public float Damage;
    public float DamageReduction;
    public float DodgeChance;
    public float Stamina;
    public float StaminaRegen;
    public float Mana;
    public float Weight;
    public GameObject Fighter;
    // Start is called before the first frame update
    void Start()
    {
         HealthPoints = Fighter.GetComponent<Character>().HealthPoints.Value;
         CritChance = Fighter.GetComponent<Character>().Damage.Value;
         Damage = Fighter.GetComponent<Character>().Damage.Value;
         DamageReduction = Fighter.GetComponent<Character>().DamageReduction.Value;
         DodgeChance = Fighter.GetComponent<Character>().DodgeChance.Value;
         Stamina = Fighter.GetComponent<Character>().Stamina.Value;
         StaminaRegen = Fighter.GetComponent<Character>().StaminaRegen.Value;
         Mana = Fighter.GetComponent<Character>().Mana.Value;
         Weight = Fighter.GetComponent<Character>().Weight.Value;

    }

    // Update is called once per frame
    
}
