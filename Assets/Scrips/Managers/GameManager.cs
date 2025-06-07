using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState1
{
    OnStage,
    OffStage,
}

public class GameManager : MonoBehaviour
{
    bool isRunning = false;
    bool isStageRunning = false;

    public GameState1 nowState;


    private void Awake()
    {
        isRunning = true;
        //StartCoroutine(GameRepeater());
    }
    private void Start()
    {
        
    }

    private IEnumerator GameRepeater()
    {
        while (isRunning)
        {
            switch (nowState)
            {
                case GameState1.OnStage:
                    yield return new WaitUntil(() => StageStart());
                    yield return new WaitUntil(() => isStageRunning);
                    break;

                case GameState1.OffStage:
                    yield return new WaitUntil(() => StageEnd());
                    yield return new WaitUntil(() => isStageRunning);
                    break;  
            }
        }
        yield break;
    }

    private bool StageStart()
    {
        isStageRunning = true;

        return true;
    }
    private bool StageEnd()
    {
        return true;
    }

}
