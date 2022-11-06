using AE.Dialogue;
using AE.Fight;
using AE.SceneManagment;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueScene : MonoBehaviour
{
    [SerializeField] DialogueManager[] acts;
    private int actIndex = 0;

    public event Action OnSceneFinishedEvent;

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

    protected virtual void OnEnable()
    {
        foreach (DialogueManager act in acts)
        {
            act.OnDialogueFinishEvent += onActFinished;
        }
    }
    protected virtual void OnDisable()
    {
        foreach (DialogueManager act in acts)
        {
            act.OnDialogueFinishEvent -= onActFinished;
        }
    }

    private void onActFinished()
    {
        acts[actIndex].gameObject.SetActive(false);
        if (actIndex < acts.Length - 1)
        {
            actIndex++;
            acts[actIndex].gameObject.SetActive(true);
        }
        else
        {
            SkipAll();
        }
    }

    public void SkipAll()
    {
        OnSceneFinishedEvent?.Invoke();
    }

    private void OnValidate()
    {
        acts = GetComponentsInChildren<DialogueManager>();
    }
}
