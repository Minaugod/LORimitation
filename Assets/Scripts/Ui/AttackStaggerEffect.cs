using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using EnumTypes;
using System.Text;
public class AttackStaggerEffect : AttackDmgEffectBase
{


    public override Resist CalculateResist(Character character, DiceType type)
    {

        Resist characterResist;

        switch (type)
        {
            case DiceType.Slash:
                characterResist = character.stat.keyPage.page.slashStaggerResist;
                break;

            case DiceType.Pierce:
                characterResist = character.stat.keyPage.page.pierceStaggerResist;
                break;

            case DiceType.Blunt:
                characterResist = character.stat.keyPage.page.bluntStaggerResist;
                break;

            default:
                characterResist = Resist.Normal;
                break;
        }

        if (character.stat.IsCharacterStaggered())
        {
            characterResist = Resist.Fatal;

        }

        return  characterResist;
    }

    public override void DisplayEffect(DiceType type, Resist characterResist, int value)
    {
        resistText.text = ResourceManager.Instance.resistResource.FindResistText(characterResist);

        resistImg.sprite = ResourceManager.Instance.resistResource.GetStaggerResistSprite(type, characterResist);

       StringBuilder sb = new StringBuilder();

        string valueString = value.ToString();

        for (int i = 0; i < valueString.Length; i++)
        {
            sb.Append("<sprite=" + valueString[i] + ", color=#FFF000>");

        }

        valueText.text = sb.ToString();
    }
}
