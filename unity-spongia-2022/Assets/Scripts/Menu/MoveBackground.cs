using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AE.MainMenu
{
    public class MoveBackground : MonoBehaviour
    {
        Vector2 StartPos;

        [SerializeField] int moveModifier;
        [SerializeField] float moveDuration = 2f;

        private void Start()
        {
            StartPos = transform.position;
        }

        private void Update()
        {
            Vector2 pz = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            float posX = Mathf.Lerp(transform.position.x, StartPos.x + (pz.x * moveModifier), moveDuration * Time.deltaTime);
            float posY = Mathf.Lerp(transform.position.y, StartPos.y + (pz.y * moveModifier), moveDuration * Time.deltaTime);

            transform.position = new Vector2(posX, posY);
        }
    }
}
