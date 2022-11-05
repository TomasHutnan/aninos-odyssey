using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AE.EventManager;

using TMPro;
using AE.Items;
using System.Text;

using static AbilityStorage;
using Abilities;
using Item = AE.Items.Item;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] GameObject tooltipParent;
    [SerializeField] RectTransform canvasRectTransform;
    [Space]
    [SerializeField] TextMeshProUGUI nameLabel;
    [SerializeField] TextMeshProUGUI typeLabel;
    [SerializeField] TextMeshProUGUI valueLabel;
    [SerializeField] TextMeshProUGUI modsLabel;
    [Space]
    [SerializeField] Vector2 offset = new Vector2(0, 0);

    private RectTransform tooltipRectTransform;

    private StringBuilder sb = new StringBuilder();

    private bool isShown;

    private void OnEnable()
    {
        tooltipParent.SetActive(false);
        isShown = false;

        EventManager.OnItemSlotEnterEvent += handleItemTooltip;
        EventManager.OnItemSlotExitEvent += hideTooltip;

        EventManager.OnAbilitySlotEnterEvent += handleAbilityTooltip;
        EventManager.OnAbilitySlotExitEvent += hideTooltip;
    }
    private void OnDisable()
    {
        EventManager.OnItemSlotEnterEvent -= handleItemTooltip;
        EventManager.OnItemSlotExitEvent -= hideTooltip;

        EventManager.OnAbilitySlotEnterEvent -= handleAbilityTooltip;
        EventManager.OnAbilitySlotExitEvent -= hideTooltip;
    }

    private void Update()
    {
        if (!isShown)
            return;

        if (tooltipRectTransform == null)
            tooltipRectTransform = tooltipParent.GetComponent<RectTransform>();

        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;
        anchoredPosition += offset;

        if (anchoredPosition.x + tooltipRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            // right side overflow
            anchoredPosition.x = canvasRectTransform.rect.width - tooltipRectTransform.rect.width + offset.x;
        }
        if (anchoredPosition.y + tooltipRectTransform.rect.height > canvasRectTransform.rect.height)
        {
            // top side overflow
            anchoredPosition.y = canvasRectTransform.rect.height - tooltipRectTransform.rect.height + offset.y;
        }

        tooltipRectTransform.anchoredPosition = anchoredPosition;
    }

    private void OnValidate()
    {
        if (tooltipParent != null)
            tooltipRectTransform = tooltipParent.GetComponent<RectTransform>();
    }

    private void handleItemTooltip(Item item)
    {
        if (item is null)
            return;

        nameLabel.text = item.Name;
        typeLabel.text = item.Type.ToString();
        valueLabel.text = $"<color=#FFD700>{ item.value} $</color>";

        sb.Length = 0;

        addModLine(item.DamageBonus, "Damage", false);
        addModLine(item.CritPercentBonus, "Crit Chance", true);

        addModLine(item.ArmorBonus, "Armor", false);
        addModLine(item.DodgeBonus, "Dodge", false);

        addModLine(item.ManaBonus, "Mana", false);

        addModLine(item.Weight, "Weight", false);

        showTooltip();
    }
    private void handleAbilityTooltip(AbilityName abilityName)
    {
        if (abilityName == AbilityName.None)
            return;

        Ability ability = AbilityStorage.GetAbility[abilityName];
        if (ability == null)
            return;

        sb.Length = 0;

        nameLabel.text = ability.name;
        typeLabel.text = ability.AffectsCaster ? "<color=#C1FFC1>SELF</color>" : "<color=#8B0000>ENEMY</color>";
        valueLabel.text = ability.ManaCost == 0 ? $"<color=#E1AD0F>{ability.StaminaCost} stamina</color>" : $"<color=#2986CC>{ability.ManaCost} mana</color>";

        sb.Append(ability.AbilityDescription);

        showTooltip();
    }

    private void showTooltip()
    {
        modsLabel.text = sb.ToString();

        nameLabel.ForceMeshUpdate();
        typeLabel.ForceMeshUpdate();
        valueLabel.ForceMeshUpdate();

        tooltipParent.SetActive(true);

        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)tooltipParent.transform);
        tooltipParent.GetComponent<VerticalLayoutGroup>().SetLayoutVertical();

        isShown = true;
    }
    private void hideTooltip()
    {
        tooltipParent.SetActive(false);
        isShown = false;
    }

    private void addModLine(float statMod, string statName, bool isPercentual)
    {
        if (statMod == 0)
            return;

        if (sb.Length > 0)
            sb.AppendLine();

        sb.Append("+");
        sb.Append(statMod);

        string divider = isPercentual ? "% " : " ";
        sb.Append(divider);

        sb.Append(statName);
    }
}
