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
    public float Doge;
    public float Stamina;
    public float StaminaRegen;
    public float Mana;
    public float Weight;
    // Start is called before the first frame update
    void Start()
    {
         HealthPoints = transform.gameObject.GetComponent<Character>().HealthPoints.Value;
         CritChance = transform.gameObject.GetComponent<Character>().Damage.Value;
         Damage = transform.gameObject.GetComponent<Character>().Damage.Value;
         DamageReduction = transform.gameObject.GetComponent<Character>().DamageReduction.Value;
         Doge = transform.gameObject.GetComponent<Character>().Dodge.Value;
         Stamina = transform.gameObject.GetComponent<Character>().Stamina.Value;
         StaminaRegen = transform.gameObject.GetComponent<Character>().StaminaRegen.Value;
         Mana = transform.gameObject.GetComponent<Character>().Mana.Value;
         Weight = transform.gameObject.GetComponent<Character>().Weight.Value;

    }

    // Update is called once per frame
    
}
