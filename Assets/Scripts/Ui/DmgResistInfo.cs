using System.Text;

public class DmgResistInfo : ResistInfoPopup
{
    public override void ResistInit(EnumTypes.Resist resist)
    {


        resistImg.sprite = ResourceManager.Instance.resistResource.GetDmgResistSprite(resistType, resist);

        StringBuilder sb = new StringBuilder();

        string resistText = ResourceManager.Instance.resistResource.FindResistText(resist);

        sb.Append(defaultTypeText + resistText);

        text = sb.ToString();

    }

    
}
