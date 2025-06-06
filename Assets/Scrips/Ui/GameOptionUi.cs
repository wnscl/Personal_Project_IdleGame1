using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public enum GameOptionText
{
    ToLobby,
    ToStage
}

public class GameOptionUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] texts;
    [SerializeField] private GameObject[] objs;

    private void Awake()
    {
        UiManager.Instance.lobbyUiEvent += ChangeLobbyText;
        UiManager.Instance.lobbyUiEvent += Close;
    }
    private GameObject ChoiceObject(GameOptionText choice)
    {
        GameObject sendObj = objs[(int)choice];

        return sendObj;
    }
    private void ChangeLobbyText(GameOptionText choice)
    {
        texts[(int)choice].text = choice.ToString();
    }
    public void Close(GameOptionText choice)
    {
        StartCoroutine(CloseAndOpen(ChoiceObject(choice)));
    }
    private IEnumerator CloseAndOpen(GameObject target)
    {

        float timer = 0;
        target.SetActive(false);
        while (timer < 0.3f)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        target.SetActive(true);
        yield break;
    }
}
