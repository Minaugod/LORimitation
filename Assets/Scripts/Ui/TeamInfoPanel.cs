using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EnumTypes;
using System.Text;
public class TeamInfoPanel : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text characterName;

    [SerializeField]
    private TMPro.TMP_Text characterHp;

    [SerializeField]
    private Image hpGauge;

    [SerializeField]
    private TMPro.TMP_Text characterStagger;

    [SerializeField]
    private Image staggerGauge;

    [SerializeField]
    private TMPro.TMP_Text characterCardCount;

    [SerializeField]
    private RawImage characterPortrait;

    [SerializeField]
    private InfoPanel_CardInfo cardInfo;

    [SerializeField]
    private List<InfoPanel_Passive> passiveList = new List<InfoPanel_Passive>();

    [SerializeField]
    private ResistInfoPopup[] resistInfos;


    private DiceController holdDice;

    public void HideInfo()
    {
        holdDice = null;

        gameObject.SetActive(false);
    }

    public void HoldInfo(DiceController dice)
    {
        holdDice = dice;

        InfoInit(dice);
    }


    public void HoverInfo(DiceController dice)
    {

        InfoInit(dice);

    }

    public void ExitInfo()
    {
        gameObject.SetActive(false);
        if (holdDice != null) InfoInit(holdDice);
    }



    private void InfoInit(DiceController dice)
    {
        Character character = dice.character;

        KeyPage keyPage = character.stat.keyPage;

        gameObject.SetActive(true);

        characterPortrait.texture = character.renderTexture;

        int hp = character.stat.Hp;
        int stagger = character.stat.Stagger;

        float hpAmount = (float)hp / keyPage.page.hp;
        hpGauge.fillAmount = hpAmount * 0.51f;

        float staggerAmount = (float)stagger / keyPage.page.staggerResist;
        staggerGauge.fillAmount = staggerAmount * 0.5f;

        // 체력 표시
        StringBuilder sb = new StringBuilder();

        string hpString = hp.ToString();

        for (int i = 0; i < hpString.Length; i++)
        {
            sb.Append("<sprite=" + hpString[i] + ", color=#FF0000>");
        }

        characterHp.text = sb.ToString();

        // 흐트러짐 표시
        sb.Clear();
        
        string staggerString = stagger.ToString();

        for (int i = 0; i < staggerString.Length; i++)
        {
            sb.Append("<sprite=" + staggerString[i] + ", color=#FFE204>");
        }

        characterStagger.text = sb.ToString();

        characterCardCount.text = character.stat.cardCount.ToString();
        characterName.text = character.stat.characterName;


        if (dice.selectCard != null)
        {
            cardInfo.CardInit(dice.selectCard);
        }

        else
        {
            cardInfo.gameObject.SetActive(false);
        }

        // 패시브 표시
        for (int i = 0; i < passiveList.Count; i++)
        {

            if (i < character.stat.keyPage.passiveEffects.Count)
            {
                passiveList[i].gameObject.SetActive(true);
                passiveList[i].SetPassiveDesc(keyPage.passiveEffects[i].passiveName, keyPage.passiveEffects[i].passiveDesc);

            }

            else
            {
                passiveList[i].gameObject.SetActive(false);
            }

        }

        if (character.stat.IsCharacterStaggered())
        {
            for (int i = 0; i < 6; i++)
            {
                resistInfos[i].ResistInit(Resist.Fatal);
            }

        }

        else
        {
            resistInfos[0].ResistInit(keyPage.page.slashDmgResist);
            resistInfos[1].ResistInit(keyPage.page.pierceDmgResist);
            resistInfos[2].ResistInit(keyPage.page.bluntDmgResist);
            resistInfos[3].ResistInit(keyPage.page.slashStaggerResist);
            resistInfos[4].ResistInit(keyPage.page.pierceStaggerResist);
            resistInfos[5].ResistInit(keyPage.page.bluntStaggerResist);
        }

    }

}

