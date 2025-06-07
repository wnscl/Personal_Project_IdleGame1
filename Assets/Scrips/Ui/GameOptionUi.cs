using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;


public class GameOptionUi : MonoBehaviour
{
    [SerializeField] private GameObject[] optionPanels;
    [SerializeField] private TextMeshProUGUI[] optionTexts;

    private Coroutine myCor;

    private void Awake()
    {
        //UiManager.Instance.UiActiveEvent += ActiveOptionPanel;
    }
    private void Start()
    {
        GameCondition.Instance.nowLobby += ActiveOptionPanel;
        GameCondition.Instance.nowLobby += ChangePlaceButtonText;

        GameCondition.Instance.nowBattle += ActiveOptionPanel;
        GameCondition.Instance.nowBattle += ChangePlaceButtonText;
    }

    private void ActiveOptionPanel()
    {
        //모든 패널을 잠시 끄고 화면 전환을 기다림
        //필요한 패널을 켜줌 (내부 텍스트라거나 값을 바꾼 후에)

        if (myCor != null) return;

        //ChangeButtonText(nowCondition);

        myCor = StartCoroutine(UnActiveAllPanel());
    }

    private IEnumerator UnActiveAllPanel()
    {
        float timer = 0f;
        foreach (GameObject panel in optionPanels)
        {
            panel?.SetActive(false);
        }

        while (timer < 0.5f)
        {
            yield return null;

            timer += Time.deltaTime;
        }

        foreach (GameObject panel in optionPanels)
        {
            panel?.SetActive(true);
        }
        myCor = null;
        yield break;
    }
    private void ChangePlaceButtonText()
    {
        if (GameCondition.Instance.Condition == GameState.Battle)
        {
            optionTexts[0].text = "ToLobby";
        }
        else
        {
            optionTexts[0].text = "ToStage";
        }
    }
    

}
