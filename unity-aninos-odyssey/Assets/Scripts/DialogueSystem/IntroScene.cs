using AE.Dialogue;
using AE.Fight;
using AE.SceneManagment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScene : DialogueScene
{
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
        FightData.FightType = FightType.Tutorial;
        FightData.Location = Location.Greece;

        SceneUtils.LoadScene("FightScene");
    }
}
