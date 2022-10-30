using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanel : MonoBehaviour
{
    [SerializeField] GameObject sellPrromptTransform;
    public void Close()
    {
        sellPrromptTransform.SetActive(false);
        gameObject.SetActive(false);
    }
}
