using AE.Fight;
using AE.SceneManagment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScene : DialogueScene
{
    [SerializeField] GameObject Scene;
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

    protected override void Start()
    {
        base.Start();
        if (FightData.FightType == FightType.Tutorial)
            Scene.SetActive(true); 
        else
            Scene.SetActive(false);
    }

    private void onSceneFinished()
    {
        Scene.SetActive(false);
    }
}
