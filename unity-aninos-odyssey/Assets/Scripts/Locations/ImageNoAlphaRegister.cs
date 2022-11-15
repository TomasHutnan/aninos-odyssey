using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageNoAlphaRegister : MonoBehaviour
{
    [SerializeField] Image image;

    private void Start()
    {
        image.alphaHitTestMinimumThreshold = 0.01f;
    }

    private void OnValidate()
    {
        image = GetComponent<Image>();
    }
}
