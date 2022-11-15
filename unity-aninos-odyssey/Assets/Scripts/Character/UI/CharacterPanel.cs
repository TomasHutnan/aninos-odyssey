using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterPanel : MonoBehaviour
{
    [SerializeField] GameObject sellPrromptTransform;

    [SerializeField] TextMeshProUGUI tabName;

    [SerializeField] Transform tabsHolder;
    [SerializeField] GameObject[] tabs;

    [SerializeField] int currentTabIndex = 0;

    private void Start()
    {
        ResloveTabIndex();
    }

    private void ResloveTabIndex()
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            if (i == currentTabIndex)
                tabs[i].SetActive(true);
            else
                tabs[i].SetActive(false);
        }

        tabName.text = tabs[currentTabIndex].name;
    }

    public void LeftTab()
    {
        if (currentTabIndex == 0)
            currentTabIndex = tabs.Length - 1;
        else
            currentTabIndex--;

        ResloveTabIndex();
    }
    public void RightTab()
    {
        if (currentTabIndex > tabs.Length - 2)
            currentTabIndex = 0;
        else
            currentTabIndex++;

        ResloveTabIndex();
    }
    private void OnValidate()
    {
        if (tabsHolder != null)
        {
            tabs = new GameObject[tabsHolder.childCount];
            int i = 0;
            foreach (Transform child in tabsHolder)
            {
                tabs[i] = child.gameObject;
                i++;
            }
        }
    }

    public void Close()
    {
        sellPrromptTransform.SetActive(false);
        gameObject.SetActive(false);
    }
}
