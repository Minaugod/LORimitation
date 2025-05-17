using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;
public class InheritedKeyPageView : MonoBehaviour
{

    private KeyPage settingKeyPage;

    [SerializeField] private Image frameImage;

    [SerializeField] private Image iconImage;

    [SerializeField] private Image glowIconImage;

    [SerializeField] private TMP_Text keyPageName;

    [SerializeField] private RarityColorData colorData;

    [SerializeField] private InheritPassiveView[] passiveViews;

    public Subject<InheritingKeyPage.InheritablePassive> inheritSubject = new Subject<InheritingKeyPage.InheritablePassive>();


    public void InitSettingKeyPage(KeyPage settingKeyPage)
    {
        this.settingKeyPage = settingKeyPage;
    }

    public void InitView(InheritingKeyPage inheritingKeyPage)
    {


        KeyPageData pageData = inheritingKeyPage.keyPage.PageData;

        iconImage.sprite = pageData.iconSprite;
        glowIconImage.sprite = pageData.glowIconSprite;
        keyPageName.text = string.Format("{0}의 책장", pageData.pageName);

        Color rarityColor = colorData.GetColor(pageData.rarity);

        frameImage.color = rarityColor;
        glowIconImage.color = rarityColor;
        keyPageName.color = rarityColor;



        List<PassiveEffect> activedPassives = settingKeyPage.GetActivedPassives();
         
        for (int i = 0; i < passiveViews.Length; i++)
        {

            if(inheritingKeyPage.inheritablePassives.Count > i)
            {
                InheritingKeyPage.InheritablePassive inheritablePassive = inheritingKeyPage.inheritablePassives[i];

                passiveViews[i].InitPassive(inheritablePassive);

                if (inheritablePassive.isActive == false)
                {
                    // 중복 패시브 이미 있음
                    if (activedPassives.Contains(inheritablePassive.passiveEffect)) passiveViews[i].UnInheritablePassive();
                }

                else
                {
                    passiveViews[i].InheritedPassive();
                }
            }

            else passiveViews[i].DisablePassive();

        }

        gameObject.SetActive(true);
    }

    public void DisableView()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {

        foreach(var passiveView in passiveViews)
        {
            passiveView.inheritSubject
                .AsObservable()
                .Subscribe(passive =>
                {
                    this.inheritSubject.OnNext(passive);
                });
        }

    }

}
