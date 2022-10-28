using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using System;

namespace AE.CharacterStats.UI
{
    public class LevelUpDisplay : MonoBehaviour
    {
        private LevelUpModType _modType;
        public LevelUpModType ModType { 
            get { return _modType; }
            set 
            {
                _modType = value;
                modifierLabel.text = _modType.ToString();
            }
        }

        [SerializeField] TextMeshProUGUI modifierLabel;

        public event Action<LevelUpModType> LevelUpSelectEvent;

        private void OnValidate()
        {
            modifierLabel = transform.GetComponentInChildren<TextMeshProUGUI>();
        }

        public void OnClick()
        {
            LevelUpSelectEvent?.Invoke(_modType);
        }
    }
}
