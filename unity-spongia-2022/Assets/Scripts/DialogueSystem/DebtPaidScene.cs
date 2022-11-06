using AE.Dialogue;
using AE.Fight;
using AE.SceneManagment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebtPaidScene : DialogueScene
{
    [SerializeField] GameObject SceneObject;

    protected override void OnEnable()
    {
        base.OnEnable();
        OnSceneFinishedEvent += onSceneFinished;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        OnSceneFinishedEvent -= onSceneFinished;
    }

    private void onSceneFinished()
    {
        SceneObject.SetActive(false);
    }
}
