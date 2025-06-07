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
        //��� �г��� ��� ���� ȭ�� ��ȯ�� ��ٸ�
        //�ʿ��� �г��� ���� (���� �ؽ�Ʈ��ų� ���� �ٲ� �Ŀ�)

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
