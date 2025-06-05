using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    OnStage,
    OffStage,
}

public class GameManager : MonoBehaviour
{
    bool isRunning = false;
    bool isStageRunning = false;

    public GameState nowState;


    private void Awake()
    {
        isRunning = true;
        StartCoroutine(GameRepeater());
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
                case GameState.OnStage:
                    yield return new WaitUntil(() => StageStart());
                    yield return new WaitUntil(() => isStageRunning);
                    break;

                case GameState.OffStage:
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
