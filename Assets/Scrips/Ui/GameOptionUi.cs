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
    //하드코딩할 필요 없이 원하는 ui오브젝트인스턴스의 포인트를 만들어서
    //게임의 상황에 따라 원하는 ui들만 쓸 수 있게
    [SerializeField] private TextMeshProUGUI[] optionTexts;
    private Dictionary<ScreenState, GameObject[]> panels; //딕셔너리를 통해 배열로 묶어준 ui를 쉽게 찾을 수 있도록

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
        //모든 패널을 잠시 끄고 화면 전환을 기다림
        //필요한 패널을 켜줌 (내부 텍스트라거나 값을 바꾼 후에)

        if (myCor != null) return;
        myCor = StartCoroutine(CloseAndOpenUiGroup(GetUis(state)));
    }
    private IEnumerator CloseAndOpenUiGroup(GameObject[] uiGroup)
    {
        float timer = 0f;
        UnActiveAllUi(); //모든 ui끄기

        while (timer < 0.5f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        foreach (GameObject ui in uiGroup) //필요한 ui만 키기
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
        //딕셔너리를 foreach로 순회하면 key값, value값을 쌍으로 찾아낸다
        //즉 한쌍씩 순회한다.
        {
            GameObject[] uiGroup = pair.Value;  //그래서 값을 넣어줄 때 .value .key로 한쌍의 키,값 중 하나를 선택할 수 있는 것
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
