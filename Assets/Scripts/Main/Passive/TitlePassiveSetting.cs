using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TitlePassiveSetting : MonoBehaviour
{

    public TitleEquippedPassive[] equippedPassives;

    public TitleEquipablePassive[] equipablePassives;

    public List<PassiveEffect> holdPassives = new List<PassiveEffect>();


    private CharacterBaseInfo character;

    public event Action OnSaveSetting;

    public void DisplayPassiveInfo(CharacterBaseInfo character)
    {
        gameObject.SetActive(true);
        holdPassives.Clear();
        this.character = character;
        
        for (int i = 0; i < equippedPassives.Length; i++)
        {

            if (i < character.keyPage.passiveEffects.Count)
            {
                equippedPassives[i].InitPassiveInfo(character.keyPage.passiveEffects[i]);
                holdPassives.Add(character.keyPage.passiveEffects[i]);
            }

            else
            {
                equippedPassives[i].UnEquipPassive();
            }
        }

        RefreshEquipable();

    }

    private void RefreshEquipable()
    {
        foreach(TitleEquipablePassive equipable in equipablePassives)
        {
            equipable.IsEquipable(holdPassives);
        }
    }

    private void Awake()
    {
        foreach(TitleEquippedPassive equipped in equippedPassives)
        {
            equipped.OnPassiveClicked += UnEquipPassive;
        }

        foreach(TitleEquipablePassive equipable in equipablePassives)
        {
            equipable.OnPassiveClicked += EquipPassive;
        }

        ////
        RefreshEquipable();
    }

    public void EquipPassive(PassiveEffect passive)
    {
        holdPassives.Add(passive);

        int index = holdPassives.Count - 1;

        equippedPassives[index].InitPassiveInfo(passive);

        RefreshEquipable();
    }

    public void UnEquipPassive(PassiveEffect passive)
    {
        holdPassives.Remove(passive);
        RefreshEquipable();
    }

    public void SavePassive()
    {
        character.keyPage.passiveEffects.Clear();

        foreach(PassiveEffect passive in holdPassives)
        {
            character.keyPage.passiveEffects.Add(passive);
        }

        gameObject.SetActive(false);
        OnSaveSetting.Invoke();
    }


}
