using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UniRx;
using UnityEngine.EventSystems;
public class InheritPassiveView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private TMP_Text costText;

    [SerializeField] private TMP_Text nameText;

    [SerializeField] private TMP_Text descText;

    [SerializeField] private Image frameImage;

    [SerializeField] private Image inheritedImage;

    [SerializeField] private GameObject inheritAlert;

    [SerializeField] private GameObject unInheritAlert;

    [SerializeField] private RarityColorData colorData;

    [SerializeField] private Color normalColor;

    [SerializeField] private Color unInheritableColor;

    public Subject<InheritingKeyPage.InheritablePassive> inheritSubject = new Subject<InheritingKeyPage.InheritablePassive>();

    private InheritingKeyPage.InheritablePassive inheritablePassive;

    bool isInheritable = false;

    bool isInherited = false;

    public void InitPassive(InheritingKeyPage.InheritablePassive inheritablePassive)
    {
        this.inheritablePassive = inheritablePassive;

        isInherited = false;
        isInheritable = true;


        PassiveEffect passive = inheritablePassive.passiveEffect;
        Color rarityColor = colorData.GetColor(passive.rarity);

        costText.text = passive.passiveCost.ToString();
        nameText.text = passive.passiveName;
        descText.text = passive.passiveDesc;

        costText.color = rarityColor;
        nameText.color = normalColor;
        descText.color = normalColor;
        frameImage.color = rarityColor;
        inheritedImage.color = rarityColor;


        inheritedImage.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    public void InheritedPassive()
    {
        isInherited = true;

        // 활성화된 Ui 켜기
        inheritedImage.gameObject.SetActive(true);
    }


    public void UnInheritablePassive()
    {
        isInheritable = false;

        frameImage.color = unInheritableColor;

        costText.color = unInheritableColor;
        nameText.color = unInheritableColor;
        descText.color = unInheritableColor;

        // 상속불가 Ui 켜ㄱl
    }


    public void DisablePassive()
    {
        gameObject.SetActive(false);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {

        if (isInheritable)
        {
            if (isInherited)
            {
                unInheritAlert.SetActive(true);
            }

            else
            {
                inheritAlert.SetActive(true);
            }
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inheritAlert.SetActive(false);
        unInheritAlert.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isInheritable)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                inheritSubject.OnNext(inheritablePassive);
            }
        }
    }
}
