using AE.Dialogue;
using AE.Fight;
using AE.SceneManagment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScene : MonoBehaviour
{
    [SerializeField] DialogueManager[] acts;
    private int actIndex = 0;

    private void Awake()
    {
        foreach (DialogueManager act in acts)
            act.gameObject.SetActive(false);

    }

    private void Start()
    {
        acts[actIndex].gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            if (acts[actIndex].IsTyping)
                acts[actIndex].ShowFullLine();
            else
                acts[actIndex].NewLine();
        }
    }

    private void OnEnable()
    {
        foreach (DialogueManager act in acts)
        {
            act.OnDialogueFinishEvent += onActFinished;
        }
    }
    private void OnDisable()
    {
        foreach (DialogueManager act in acts)
        {
            act.OnDialogueFinishEvent -= onActFinished;
        }
    }

    private void onActFinished()
    {
        if (actIndex < acts.Length - 1)
        {
            actIndex++;
            acts[actIndex].gameObject.SetActive(true);
        }
        else
        {
            FightData.FightType = FightType.Tutorial;
            FightData.Location = Location.Greece;

            SceneUtils.LoadScene("FightScene");
        }
    }

    private void OnValidate()
    {
        acts = GetComponentsInChildren<DialogueManager>();
    }
}
