using AE.EventManager;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static AbilityStorage;

public class FighterSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] TextMeshProUGUI nameLabel;
    [SerializeField] TextMeshProUGUI levelLabel;
    [SerializeField] TextMeshProUGUI classLabel;
    [SerializeField] TextMeshProUGUI rewardLabel;
    [SerializeField] TextMeshProUGUI experienceLabel;

    public event Action<Enemy> OnEnemySelectEvent;

    private Enemy _enemy;
    public Enemy Enemy
    {
        get { return _enemy; }
        set
        {
            print("SET ENEMY");
            _enemy = value;

            if (_enemy == null)
                return;

            print("AFTER NULL CHECK");
            nameLabel.text = _enemy.Name;
            levelLabel.text = $"Level {_enemy.LevelUpSystem.Level}";
            classLabel.text = $"Class:  {_enemy.Class}";
            rewardLabel.text = $"Reward:  {_enemy.Money} $";
            experienceLabel.text = $"Experience:  {_enemy.ExpGain} exp";
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && _enemy != null)
        {
            if (eventData.button == PointerEventData.InputButton.Left
                || eventData.button == PointerEventData.InputButton.Right)
            {
                OnEnemySelectEvent?.Invoke(_enemy);
            }
        }
    }
}
