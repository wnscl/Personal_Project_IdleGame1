using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUi : MonoBehaviour
{
    [SerializeField] private GameObject goldAniObj;
    [SerializeField] private TextMeshProUGUI resourceText;

    Vector3 startPos;

    public int gold;
    private int addCount = 10;
    public Coroutine myCor;

    private void Awake()
    {
        startPos = goldAniObj.transform.position;
    }

    public void InitUi(Stages stage)
    {
        addCount = 10;
        ChangeGold(false, 0);
    }
    public void UpdateResurce(Stages stage)
    {
        StageManager sm = StageManager.Instance;

        if (stage == Stages.stage1) addCount += 10;

        int amount = sm.stageCount * addCount;

        ChangeGold(true, amount);
    }
    public bool ChangeGold(bool addOrDecrease, int amount)
    {
        int newGold = gold + (addOrDecrease ? amount : -amount);

        if (newGold < 0) return false;

        gold = newGold;
        resourceText.text = $"{gold}";
        PlayGoldAnimation(amount);
        return true;
    }
    private void PlayGoldAnimation(int amount)
    {
        if (myCor != null) StopCoroutine(myCor);
        
        goldAniObj.SetActive(false);
        myCor = StartCoroutine(GoldAnimation(amount));
    }

    private IEnumerator GoldAnimation(int amount)
    {
        TextMeshProUGUI addGoldText = goldAniObj.GetComponentInChildren<TextMeshProUGUI>();
        addGoldText.text = amount.ToString();

        goldAniObj.SetActive(true);

        Vector3 endPos = new Vector3((startPos.x - 20), startPos.y, startPos.z);
        
        float timer = 0;
        while (timer < 1f)
        {
            float t = timer / 1;
            //if (t < 1) t = Mathf.Clamp(t + 0.05f, 0, 1f);
            
            goldAniObj.transform.position = Vector3.Lerp(startPos, endPos, t);
            timer += Time.deltaTime;
            yield return null;
        }
        goldAniObj.transform.position = endPos;

        myCor = null;
        goldAniObj.SetActive(false);
        yield break;
    }
}
