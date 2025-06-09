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
    [SerializeField] private GameObject[] lobbyUis;
    [SerializeField] private GameObject[] InvenUis;
    [SerializeField] private GameObject[] ForgeUis;
    [SerializeField] private GameObject[] StoreUis;
    [SerializeField] private GameObject[] BattleUis;
    //�ϵ��ڵ��� �ʿ� ���� ���ϴ� ui������Ʈ�ν��Ͻ��� ����Ʈ�� ����
    //������ ��Ȳ�� ���� ���ϴ� ui�鸸 �� �� �ְ�
    [SerializeField] private TextMeshProUGUI[] optionTexts;
    private Dictionary<ScreenState, GameObject[]> panels; //��ųʸ��� ���� �迭�� ������ ui�� ���� ã�� �� �ֵ���

    private Coroutine myCor;

    private void Awake()
    {
        panels = new Dictionary<ScreenState, GameObject[]>();
        panels.Add(ScreenState.Lobby, lobbyUis);
        panels.Add(ScreenState.Inventory, InvenUis);
        panels.Add(ScreenState.Forge, ForgeUis);
        panels.Add(ScreenState.Store, StoreUis);
        panels.Add(ScreenState.Battle, BattleUis);
    }
    public void ChangeOptionUi(ScreenState state)
    {
        ActiveOptionPanel(state);
        ChangePlaceButtonText();
    }
    private GameObject[] GetUis(ScreenState state)
    {
        panels.TryGetValue(state, out GameObject[] panel);
        return panel;
    }
    private void ActiveOptionPanel(ScreenState state)
    {
        //��� �г��� ��� ���� ȭ�� ��ȯ�� ��ٸ�
        //�ʿ��� �г��� ���� (���� �ؽ�Ʈ��ų� ���� �ٲ� �Ŀ�)

        if (myCor != null) return;
        myCor = StartCoroutine(CloseAndOpenUiGroup(GetUis(state)));
    }
    private IEnumerator CloseAndOpenUiGroup(GameObject[] uiGroup)
    {
        float timer = 0f;
        UnActiveAllUi(); //��� ui����

        while (timer < 0.5f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        foreach (GameObject ui in uiGroup) //�ʿ��� ui�� Ű��
        {
            if (ui != null)
            {
                ui?.SetActive(true);
            }
        }
        myCor = null;
        yield break;
    }
    private void UnActiveAllUi()    
    {
        foreach (var pair in panels)
        //��ųʸ��� foreach�� ��ȸ�ϸ� key��, value���� ������ ã�Ƴ���
        //�� �ѽ־� ��ȸ�Ѵ�.
        {
            GameObject[] uiGroup = pair.Value;  //�׷��� ���� �־��� �� .value .key�� �ѽ��� Ű,�� �� �ϳ��� ������ �� �ִ� ��
            //ScreenState a = key.Key;
            foreach (GameObject ui in uiGroup)
            {
                if (ui != null)
                {
                    ui.SetActive(false);
                }
            }
        }
    }
    private void ChangePlaceButtonText()
    {
        if (GameCondition.Instance.Condition == ScreenState.Battle)
        {
            optionTexts[0].text = "ToLobby";
        }
        else
        {
            optionTexts[0].text = "ToStage";
        }
    }
    

}
