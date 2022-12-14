using AE.Items;
using AE.Fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AE.SceneManagment;

public class FightersGrid : MonoBehaviour
{
    [SerializeField] FighterSlot[] fighterSlots;
    [SerializeField] ItemTier enemyStage;
    [SerializeField] Location fightLocation;
    [SerializeField] FightType fightType;

    private void Start()
    {
        Enemy[] enemies = EnemyGeneration.GetFightChoises(enemyStage, fighterSlots.Length);

        for (int i = 0; i < fighterSlots.Length; i++)
        {
            fighterSlots[i].Enemy = enemies[i];
        }
    }

    private void OnEnable()
    {
        foreach (FighterSlot fighterSlot in fighterSlots)
        {
            fighterSlot.OnEnemySelectEvent += OnEnemySelection;
        }
    }
    private void OnDisable()
    {
        foreach (FighterSlot fighterSlot in fighterSlots)
        {
            fighterSlot.OnEnemySelectEvent -= OnEnemySelection;
        }
    }

    public void OnEnemySelection(Enemy enemy)
    {
        FightData.PreCreatedEnemy = enemy;
        FightData.Location = fightLocation;
        FightData.FightType = fightType;

        SceneUtils.LoadScene("FightScene", true);
    }

    private void OnValidate()
    {
        fighterSlots = GetComponentsInChildren<FighterSlot>();
    }
}
