using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Ui_CharaCardSetting : MonoBehaviour
{


    [SerializeField]
    private Ui_CustomizeCategory ui_Category;

    [SerializeField]
    private Animator titleAnim;

    [SerializeField]
    private TMP_Text titleNameText;

    [SerializeField]
    private Ui_EquippableCardHandler equippableCardHandler;

    [SerializeField]
    private Ui_EquippedCardHandler equippedCardHandler;

    public event System.Action onDataChanged;

    private void Start()
    {
        equippableCardHandler.onCardEquipped += RefreshCardPool;
        equippedCardHandler.onCardUnEquipped += RefreshCardPool;
    }

    public void SetCardSettingInfo(Librarian librarian)
    {
 
        equippableCardHandler.InitInfo(librarian);
        equippedCardHandler.InitInfo(librarian);

    }

    public void RefreshCardPool()
    {


        onDataChanged?.Invoke();

        equippedCardHandler.RefreshCardPool();
    }

    public void OpenSettingPanel()
    {
        if (gameObject.activeSelf) return;

        gameObject.SetActive(true);

        titleNameText.text = "전투 책장";

        titleAnim.Play("TitleChanged");

        ui_Category.SelectCategory();

    }   

    public void ClosePanel()
    {

        ui_Category.DeSelectCategory();

        gameObject.SetActive(false);
    }




}
