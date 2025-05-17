using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UI;
using TMPro;
[System.Serializable]
public struct DiceResource
{
    public Sprite enemyDice;
    public Sprite teamDice;
    public Sprite staggeredDice;
}




[System.Serializable]
public class ResistResource
{

    Dictionary<Resist, string> resistTextDic = new Dictionary<Resist, string>() 
    {
        { Resist.Ineffective, "내성" },
        { Resist.Endured, "견딤" },
        { Resist.Normal, "보통" },
        { Resist.Weak, "약점" },
        { Resist.Fatal, "취약" },
    };


    public SpriteAtlas resistAtlas;

    public Sprite GetDmgResistSprite(DiceType type, Resist resist)
    {
        return resistAtlas.GetSprite(string.Format("Dmg{0}{1}", type, resist));
    }

    public Sprite GetStaggerResistSprite(DiceType type, Resist resist)
    {
        return resistAtlas.GetSprite(string.Format("Stagger{0}{1}", type, resist));
    }
    public string FindResistText(Resist resist)
    {
        return resistTextDic[resist];
    }

}

[System.Serializable]
public struct CardResource
{
    public Sprite[] cardCost;

    public SpriteAtlas diceAtlas;

    public Color limitedColor;

    public Color hardcoverColor;

    public Color paperbackColor;

    public Color attackTypeColor;

    public Color defenseTypeColor;

    public Color GetCardRarityColor(Rarity rarity)
    {

        Color rarityColor;

        switch (rarity)
        {
            case Rarity.Limited:
                rarityColor = limitedColor;
                break;

            case Rarity.Hardcover:
                rarityColor = hardcoverColor;

                break;

            default:
                rarityColor = paperbackColor;
                break;
        }

        return rarityColor;
    }

    public Sprite GetDiceSprite(DiceType type)
    {
        return diceAtlas.GetSprite(type.ToString());
    }

}


[System.Serializable]
public class CharacterBaseInfo
{
    public bool isEnemy;

    public string name;

    public KeyPage keyPage;

    public Sprite thumb;

    public Vector3 spawnPos;
}


[System.Serializable]
public class InheritingKeyPage
{
    public IKeyPage keyPage;

    public List<PassiveEffect> passiveEffects { get; private set; }

    public List<PassiveEffect> activedPassives { get; private set; }


    public List<InheritablePassive> inheritablePassives = new List<InheritablePassive>();

    [System.Serializable]
    public class InheritablePassive
    {
        public bool isActive = false;

        public PassiveEffect passiveEffect;

        public InheritablePassive(PassiveEffect passiveEffect)
        {
            this.passiveEffect = passiveEffect;
        }
    }



    public InheritingKeyPage(IKeyPage keyPage)
    {
        this.keyPage = keyPage;

        foreach (var passive in keyPage.PageData.passiveEffects)
        {
            inheritablePassives.Add(new InheritablePassive(passive));
        }

        passiveEffects = keyPage.PageData.passiveEffects;

    }
}



public interface IKeyPage
{

    KeyPageData PageData { get; }

    ILibrarian EquippedLibrarian { get; }

    List<InheritingKeyPage> InheritingKeyPages { get; }

    void RequestUnInherit(IKeyPage keyPage);


}

[System.Serializable]
public class KeyPage : IKeyPage
{
    public KeyPageData page;

    public KeyPageData PageData => page;


    public void AddInheritKeyPage(KeyPage keyPage)
    {
        inheritingKeyPages.Add(new InheritingKeyPage(keyPage));
        
        keyPage.SetInheritorKeyPage(this);
    }


    public IKeyPage inheritorKeyPage { get; private set; }

    public void SetInheritorKeyPage(IKeyPage keyPage)
    {   
        inheritorKeyPage = keyPage;
    }

    public void UnInheritCurrentInheritor()
    {
        inheritorKeyPage.RequestUnInherit(this);
        inheritorKeyPage = null;
    }

    public void RequestUnInherit(IKeyPage keyPage)
    {
        foreach(var inheritedKeyPage in inheritingKeyPages)
        {
            if (inheritedKeyPage.keyPage == keyPage)
            {
                // UnInherit'
                inheritingKeyPages.Remove(inheritedKeyPage);

                break;
            }
        }
    }


    public List<CardData> cards = new List<CardData>(9);

    private ILibrarian equippedLibrarian;

    public ILibrarian EquippedLibrarian => equippedLibrarian;

    public List<InheritingKeyPage> InheritingKeyPages => inheritingKeyPages;


    public List<PassiveEffect> GetActivedPassives()
    {
        List<PassiveEffect> passiveEffects = new List<PassiveEffect>();

        foreach(var passive in page.passiveEffects)
        {
            passiveEffects.Add(passive);
        }

        foreach(var inheritingKeyPage in inheritingKeyPages)
        {
            foreach(var passive in inheritingKeyPage.inheritablePassives)
            {
                if (passive.isActive) passiveEffects.Add(passive.passiveEffect);
            }
        }

        return passiveEffects;
    }

    public void NewEquippedLibrarian(ILibrarian librarian)
    {
        if (equippedLibrarian != null) equippedLibrarian.UnEquipKeyPage();
        equippedLibrarian = librarian;
    }

    public ILibrarian NewGetEquippedLibrarian()
    {
        return equippedLibrarian;
    }

    public void UnEquip()
    {
        equippedLibrarian = null;
    }

    public List<InheritingKeyPage> inheritingKeyPages = new List<InheritingKeyPage>(4);


    [Space(10f)]

    public List<PassiveEffect> passiveEffects = new List<PassiveEffect>();

    public Librarian GetEquippedLibrarian()
    {
        Librarian equippedLibrarian = DataManager.Inst.GetEquippedLibrarian(this);

        return equippedLibrarian;
    }

    public void SetEquippedLibrarian(Librarian librarian)
    {

        Librarian equippedLibrarian = GetEquippedLibrarian();

        if (equippedLibrarian != null) equippedLibrarian.UnEquipKeyPage();

        DataManager.Inst.LibrarianEquipKeyPage(this, librarian);
    }

    public void UnEquippedLibrarian()
    {
        DataManager.Inst.LibrarianUnEquipKeyPage(this);
        
    }
    

    public InheritMemento GetInheritMemento()
    {
        return new InheritMemento(this);
    }

    public void RestoreInheritMemento(InheritMemento memento)
    {
        inheritorKeyPage = memento.inheritorKeyPage;

        inheritingKeyPages = memento.inheritingKeyPages;
    }

    public class InheritMemento
    {
        public IKeyPage inheritorKeyPage;

        public List<InheritingKeyPage> inheritingKeyPages;


        public InheritMemento(KeyPage keyPage)
        {
            inheritorKeyPage = keyPage.inheritorKeyPage;

            inheritingKeyPages = keyPage.inheritingKeyPages;
        }
    }

    public Resist GetResist(DamageType damageType, DiceType diceType)
    {

        switch (damageType)
        {
            case DamageType.Hp:

                switch (diceType)
                {
                    case DiceType.Slash:
                        return page.slashDmgResist;

                    case DiceType.Pierce:
                        return page.pierceDmgResist;

                    case DiceType.Blunt:
                        return page.bluntDmgResist;
                }
                break;

            case DamageType.Stagger:

                switch (diceType)
                {
                    case DiceType.Slash:
                        return page.slashStaggerResist;

                    case DiceType.Pierce:
                        return page.pierceStaggerResist;

                    case DiceType.Blunt:
                        return page.bluntStaggerResist;
                }
                break;

        }

        return Resist.Normal;
   

    }

}

[System.Serializable]
public class KeyPageSet
{
    public string setName;

    public Sprite setIcon;

    public Sprite setGlowIcon;

    public List<KeyPage> keyPages = new List<KeyPage>();


}


public class ResourceManager : MonoBehaviour
{

    public List<KeyPageSet> keyPageSet = new List<KeyPageSet>();

    public CardResource cardResource;

    public DiceResource diceResource;

    public ResistResource resistResource;

    public Dictionary<Resist, float> resistDmgValue = new Dictionary<Resist, float>();

    public EmotionLevelEffect[] emotionLevelEffects;

    public List<CharacterBaseInfo> selectStageEnemy = new List<CharacterBaseInfo>();

    public List<CharacterBaseInfo> allowTeamCharacter = new List<CharacterBaseInfo>();

    public void SettingEnemyCharacter(CharacterBaseInfo[] characters)
    {
        selectStageEnemy.Clear();

        for (int i = 0; i < characters.Length; i++)
        {
            selectStageEnemy.Add(characters[i]);
        }
    }

    public void SettingTeamCharacter(TitleMember[] members)
    {
        allowTeamCharacter.Clear();

        foreach(TitleMember member in members)
        {
            if (member.IsAllow) allowTeamCharacter.Add(member.character);

        }
    }


    private static ResourceManager instance;

    public static ResourceManager Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }

            return instance;
        }
    }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }

        else
        {
            Destroy(gameObject);
        }


        resistDmgValue.Add(Resist.Ineffective, 0.2f);
        resistDmgValue.Add(Resist.Endured, 0.5f);
        resistDmgValue.Add(Resist.Normal, 1.0f);
        resistDmgValue.Add(Resist.Weak, 1.5f);
        resistDmgValue.Add(Resist.Fatal, 2.0f);

    }

}
