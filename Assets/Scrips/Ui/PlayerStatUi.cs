using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class PlayerStatUi : MonoBehaviour
{
    [SerializeField] private Image[] valueBarGroup;

    [SerializeField] private TextMeshProUGUI[] statTextGroup;


    public void UpdateValueBar(IStateControl player)
    {
        EntityInfo playerInfo = player.GetEntityInfo();

        if (playerInfo.entityType != EntityType.Player)
        {
            return;
        }

        valueBarGroup[0].fillAmount = (playerInfo.currentHp / playerInfo.maxHp);
        valueBarGroup[1].fillAmount = (playerInfo.currentMp / playerInfo.maxMp);
        UpdateStatText(playerInfo);
    }
    private void UpdateStatText(EntityInfo playerInfo)
    {
        statTextGroup[0].text = $"{(playerInfo.currentHp / playerInfo.maxHp) * 100f}%";
        statTextGroup[1].text = $"{(playerInfo.currentMp /  playerInfo.maxMp) * 100f}%";
        statTextGroup[2].text = $"Atk : {playerInfo.atk}";
        statTextGroup[3].text = $"Speed : {playerInfo.atkSpeed}";
        statTextGroup[4].text = $"Def : {playerInfo.def}";
    }
}
