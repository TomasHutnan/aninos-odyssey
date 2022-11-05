using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StanceController : MonoBehaviour
{

    [Header("Stances")]
    public Sprite Idle;
    public Sprite Attack;
    public Sprite Defend;

    [Header("Images")]
    public Image PrimaryImage;
    public Image SecondaryImage;

    [Header("Animation")]
    public int AnimationLength;
    public bool PrimaryIsFront = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fade() {
        Fade0(PrimaryIsFront ? PrimaryImage : SecondaryImage, PrimaryIsFront ? SecondaryImage : PrimaryImage);
        PrimaryIsFront = !PrimaryIsFront;
    }

    private void Fade0(Image a, Image b) {
        b.sprite = Attack;

        StartCoroutine(FadeIn(PrimaryImage));
        StartCoroutine(FadeOut(SecondaryImage));
    }

    private IEnumerator FadeIn(Image image) {
        for (float i = 0; i <= AnimationLength; i += Time.deltaTime) {
            image.color = new Color(1, 1, 1, i / AnimationLength);
            yield return null;
        }
    }
    private IEnumerator FadeOut(Image image) {
        for (float i = AnimationLength; i >= 0; i -= Time.deltaTime) {
            image.color = new Color(1, 1, 1, i / AnimationLength);
            yield return null;
        }
    }

}
