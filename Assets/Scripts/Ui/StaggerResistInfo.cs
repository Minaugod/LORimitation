using System.Text;

public class StaggerResistInfo : ResistInfoPopup
{
    public override void ResistInit(EnumTypes.Resist resist)
    {

        resistImg.sprite = ResourceManager.Instance.resistResource.GetStaggerResistSprite(resistType, resist);

        StringBuilder sb = new StringBuilder();

        string resistText = ResourceManager.Instance.resistResource.FindResistText(resist);

        sb.Append(defaultTypeText + resistText);

        text = sb.ToString();


    }
}
