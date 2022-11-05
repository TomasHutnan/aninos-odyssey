using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace AE.FightManager {
    public class DisplayController : MonoBehaviour
    {

        [Header("Health")]
        public Image healthImage;
        public TextMeshProUGUI healthText;
        [Header("Stamina")]
        public Image staminaImage;
        public TextMeshProUGUI staminaText;
        [Header("Mana")]
        public Image manaImage;
        public TextMeshProUGUI manaText;
        [Header("Animation")]
        public float AnimationRate = 60;

        private RealtimeStatsHolder holder;
        private float displayedHealth, displayedStamina, displayedMana;

        // Start is called before the first frame update
        void Start()
        {
            holder = GetComponent<RealtimeStatsHolder>();
            if (holder.StatHolder.ContainsKey(Stat.HealthPoints))
                UpdateStartingValues();
        }

        public void UpdateStartingValues() {
            displayedHealth = holder.StatHolder[Stat.HealthPoints];
            displayedStamina = holder.StatHolder[Stat.Stamina];
            displayedMana = holder.StatHolder[Stat.Mana];
        }

        // Update is called once per frame
        void Update()
        {
            if (holder._fighter == null || !holder.StatHolder.ContainsKey(Stat.HealthPoints))
                return;

            displayedHealth = AnimateFloat(displayedHealth, holder.StatHolder[Stat.HealthPoints]);
            displayedStamina = AnimateFloat(displayedStamina, holder.StatHolder[Stat.Stamina]);
            displayedMana = AnimateFloat(displayedMana, holder.StatHolder[Stat.Mana]);

            healthImage.fillAmount = (float) displayedHealth / holder._fighter.HealthPoints.Value;
            staminaImage.fillAmount = (float) displayedStamina / holder._fighter.Stamina.Value;
            manaImage.fillAmount = (float) displayedMana / holder._fighter.Mana.Value;

            healthText.text = Mathf.Round(displayedHealth) + "/" + Mathf.Round(holder._fighter.HealthPoints.Value);
            staminaText.text = Mathf.Round(displayedStamina) + "/" + Mathf.Round(holder._fighter.Stamina.Value);
            manaText.text = Mathf.Round(displayedMana) + "/" + Mathf.Round(holder._fighter.Mana.Value);
        }

        public float AnimateFloat(float displayed, float target) {
            float diff = Time.deltaTime * AnimationRate;
            return displayed < target ?
            displayed + diff > target ? target : displayed + diff :
            displayed - diff < target ? target : displayed - diff;
        }
    }
}