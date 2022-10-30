using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AE.EventManager;

using TMPro;
using AE.Items;
using System.Text;

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

        EventManager.OnItemSlotEnterEvent += ShowTooltip;
        EventManager.OnItemSlotExitEvent += HideTooltip;
    }
    private void OnDisable()
    {
        EventManager.OnItemSlotEnterEvent -= ShowTooltip;
        EventManager.OnItemSlotExitEvent -= HideTooltip;
    }

    private void Update()
    {
        if (!isShown)
            return;

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

    private void ShowTooltip(Item item)
    {
        if (item is null)
            return;

        tooltipParent.SetActive(true);
        isShown = true;

        nameLabel.text = item.Name;
        typeLabel.text = item.Type.ToString();
        valueLabel.text = item.value.ToString() + "$";

        sb.Length = 0;

        AddModLine(item.DamageBonus, "Damage", false);
        AddModLine(item.CritPercentBonus, "Crit Chance", true);

        AddModLine(item.ArmorBonus, "Armor", false);
        AddModLine(item.DodgeBonus, "Dodge", false);

        AddModLine(item.ManaBonus, "Mana", false);


        AddModLine(item.Weight, "Weight", false);

        modsLabel.text = sb.ToString();
    }
    private void HideTooltip()
    {
        tooltipParent.SetActive(false);
        isShown = false;
    }

    private void AddModLine(float statMod, string statName, bool isPercentual)
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
