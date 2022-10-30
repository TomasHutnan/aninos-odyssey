using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StreetManger : MonoBehaviour
{
    [SerializeField] Transform Nav;
    [SerializeField] GameObject[] navChildren;

    [SerializeField] Transform StreetsParent;
    [SerializeField] GameObject[] streets;

    [SerializeField] int deafultStreetIndex;
    [SerializeField] int currentStreetIndex;

    private void Start()
    {
        currentStreetIndex = deafultStreetIndex;
        ResloveStreetIndex();
    }

    private void ResloveStreetIndex()
    {
        if (currentStreetIndex == 0)
            navChildren[0].SetActive(false);
        else
            navChildren[0].SetActive(true);

        if (currentStreetIndex == streets.Length-1)
            navChildren[1].SetActive(false);
        else
            navChildren[1].SetActive(true);

        for (int i = 0; i < streets.Length; i++)
        {
            if (i == currentStreetIndex)
                streets[i].SetActive(true);
            else
                streets[i].SetActive(false);
        }
    }

    public void Left()
    {
        if (currentStreetIndex <= 0)
            currentStreetIndex = streets.Length - 1;
        else
            currentStreetIndex--;

        ResloveStreetIndex();
    }
    public void Right()
    {
        if (currentStreetIndex >= streets.Length - 1)
            currentStreetIndex = 0;
        else
            currentStreetIndex++;

        ResloveStreetIndex();
    }

    private void OnValidate()
    {
        if (Nav != null)
            navChildren = getDirectChildGameObjects(Nav);
        if (StreetsParent != null)
            streets = getDirectChildGameObjects(StreetsParent);
    }
    private GameObject[] getDirectChildGameObjects(Transform parent)
    {
        GameObject[] gameObjects = new GameObject[parent.childCount];
        int i = 0;
        foreach (Transform child in parent)
        {
            gameObjects[i] = child.gameObject;
            i++;
        }
        return gameObjects.ToArray();
    }
}
