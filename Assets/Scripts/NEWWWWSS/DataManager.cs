using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.IO;
using System.Threading.Tasks;

[System.Serializable]
public class LeaderLibrarian : Librarian
{

    public void UpdateLeaderLibrarianData(LeaderLibrarianWrapper dataWrapper)
    {
        EquipKeyPageByIndex(dataWrapper.keyPageIndex);
    }


}

[System.Serializable]
public class MemberLibrarian : Librarian
{
    public HeadCustomizeData customizeData;
    public void UpdateLibrarianData(LibrarianWrapper dataWrapper)
    {
        name = dataWrapper.name;
        customizeData = dataWrapper.customizeData.ToHeadCustomizeData();

        EquipKeyPageByIndex(dataWrapper.keyPageIndex);

    }
}


public interface ILibrarian
{
    string Name { get; }

    RenderTexture PortraitTexture { get; }

    void UnEquipKeyPage();

}

[System.Serializable]
public class Librarian : ILibrarian
{
    public string name;

    public KeyPage keyPage;

    public event System.Action onKeyPageChanged;

    public GameObject characterHead;

    public RenderTexture portraitTexture;

    [Space(10f)]
    public KeyPage basicKeyPage;


    /* New Event
    public event System.Func<KeyPage> onRequestKeyPage;

    public void NewEquipKeyPage(KeyPage keyPage)
    {
        onRequestKeyPage += keyPage.ReturnThis;
    }

    public KeyPage GetEquippedKeyPage()
    {
        return onRequestKeyPage?.Invoke();
    }

    */

    // Interface members
    public string Name => name;
    public RenderTexture PortraitTexture => portraitTexture;


    public void EquipKeyPage(KeyPage keyPage)
    {

        if (this.keyPage != null)
        {
            //this.keyPage.UnEquippedLibrarian();

            this.keyPage.UnEquip();

            this.keyPage = null;
        }

        this.keyPage = keyPage;


        onKeyPageChanged?.Invoke();


        //keyPage.EquippedLibrarian(this);
        keyPage.NewEquippedLibrarian(this);

    }

    public void UnEquipKeyPage()
    {
        KeyPage equippedKeyPage = keyPage;

        //equippedKeyPage.UnEquippedLibrarian();
        equippedKeyPage.UnEquip();

        keyPage = null;

        if (equippedKeyPage != basicKeyPage) EquipKeyPage(basicKeyPage);


    }

    protected void EquipKeyPageByIndex(int keyPageIndex)
    {

        if (keyPageIndex >= 0 && keyPageIndex < DataManager.Inst.keyPages.Count)
        {
            EquipKeyPage(DataManager.Inst.keyPages[keyPageIndex]);
        }

        else
        {
            EquipKeyPage(basicKeyPage);
        }
    }
}

[System.Serializable]
public class LibrariansFloor
{
    public string floorName;

    public LeaderLibrarian leaderLibrarian;

    public List<MemberLibrarian> memberLibrarians = new List<MemberLibrarian>();

    public List<Librarian> GetLibrarians()
    {
        List<Librarian> librarians = new List<Librarian>();

        librarians.Add(leaderLibrarian);
        foreach(var member in memberLibrarians)
        {
            librarians.Add(member);
        }

        return librarians;
    }



    public void UpdateFloorData(LibrariansFloorWrapper floorWrapper)
    {
        leaderLibrarian.UpdateLeaderLibrarianData(floorWrapper.leaderLibrarian);

        for (int i = 0; i < memberLibrarians.Count; ++i)
        {
            memberLibrarians[i].UpdateLibrarianData(floorWrapper.librarians[i]);
        }

    }

}

[System.Serializable]
public class KeyPageWrapper
{

    public List<string> cardDataAddresses = new List<string>();

    public List<CardData> cardDatas = new List<CardData>();

    public KeyPageWrapper(KeyPage keyPage)
    {
        foreach (var card in keyPage.cards)
        {
            if (card != null)
            cardDataAddresses.Add(card.address);
        }

    }


    public void LoadCardDatas()
    {
        // CardData 로드
        foreach (var address in cardDataAddresses)
        {
            AsyncOperationHandle<CardData> cardOperation = Addressables.LoadAssetAsync<CardData>(address);
            cardOperation.Completed += (cardHandle) =>
            {
                cardDatas.Add(cardHandle.Result);
                Addressables.Release(cardHandle);
            };
        }
    }

    public void ToKeyPage(System.Action<KeyPage> onComplete)
    {
        KeyPage keyPage = new KeyPage();


        // CardData 로드
        foreach (var address in cardDataAddresses)
        {
            AsyncOperationHandle<CardData> cardOperation = Addressables.LoadAssetAsync<CardData>(address);
            cardOperation.Completed += (cardHandle) =>
            {
                keyPage.cards.Add(cardHandle.Result);
                Addressables.Release(cardHandle);
            };
        }


        // 초기화 완료 후 콜백 호출
        onComplete(keyPage);
    }

}


[System.Serializable]
public class KeyPageSetListWrapper
{
    public List<KeyPageSetWrapper> keyPageSetList;
}

[System.Serializable]
public class KeyPageSetWrapper
{
    public string setName;

    public List<KeyPageWrapper> keyPageList = new List<KeyPageWrapper>();

    public KeyPageSetWrapper(KeyPageSet keyPageSet)
    {
        setName = keyPageSet.setName;

        foreach(var keyPage in keyPageSet.keyPages)
        {
            keyPageList.Add(new KeyPageWrapper(keyPage));
        }
    }
}



[System.Serializable]
public class KeyPageListWrapper
{
    public List<KeyPageWrapper> keyPageList;
}

[System.Serializable]
public class LibrariansFloorListWrapper
{
    public List<LibrariansFloorWrapper> librariansFloorList;
}

[System.Serializable]
public class LibrariansFloorWrapper
{
    public string floorName;

    public LeaderLibrarianWrapper leaderLibrarian;

    public List<LibrarianWrapper> librarians = new List<LibrarianWrapper>();

    public LibrariansFloorWrapper(LibrariansFloor librariansFloor)
    {
        floorName = librariansFloor.floorName;

        leaderLibrarian = new LeaderLibrarianWrapper(librariansFloor.leaderLibrarian);

        for (int i = 0; i < librariansFloor.memberLibrarians.Count; ++i)
        {
            librarians.Add(new LibrarianWrapper(librariansFloor.memberLibrarians[i]));
        }


    }

}

[System.Serializable]
public class LeaderLibrarianWrapper
{
    public string name;

    public int keyPageIndex;

    public LeaderLibrarianWrapper(LeaderLibrarian librarian)
    {
        name = librarian.name;


        if (DataManager.Inst.keyPages.Contains(librarian.keyPage))
        {
            keyPageIndex = DataManager.Inst.keyPages.IndexOf(librarian.keyPage);
        }

        else
        {
            keyPageIndex = -1;
        }
    }
}


[System.Serializable]
public class LibrarianWrapper
{
    public string name;

    public int keyPageIndex;

    public HeadCustomizeDataWrapper customizeData;


    public LibrarianWrapper(MemberLibrarian librarian)
    {
        name = librarian.name;

        if (DataManager.Inst.keyPages.Contains(librarian.keyPage))
        {
            keyPageIndex = DataManager.Inst.keyPages.IndexOf(librarian.keyPage);
        }

        else
        {
            keyPageIndex = -1;
        }

        customizeData = new HeadCustomizeDataWrapper(librarian.customizeData);

    }
}

public class DataManager : MonoBehaviour
{

    bool isDataLoaded = false;

    public bool IsDataLoaded()
    {
        return isDataLoaded;
    }

    
    public List<KeyPage> keyPages = new List<KeyPage>();

    public List<KeyPageSet> keyPageSets = new List<KeyPageSet>();

    public List<LibrariansFloor> librariansFloors = new List<LibrariansFloor>();


    public Dictionary<KeyPage, Librarian> keyPageToLibrarianMap = new Dictionary<KeyPage, Librarian>();

    public Dictionary<KeyPage, KeyPage> inheritKeyPageToKeyPageMap = new Dictionary<KeyPage, KeyPage>();


    public void LibrarianEquipKeyPage(KeyPage keyPage, Librarian librarian)
    {
        keyPageToLibrarianMap.Add(keyPage, librarian);

    }

    public void LibrarianUnEquipKeyPage(KeyPage keyPage)
    {
        keyPageToLibrarianMap.Remove(keyPage);
        
    }

    public void KeyPageInherit(KeyPage giver, KeyPage inheritor)
    {
        inheritKeyPageToKeyPageMap.Add(giver, inheritor);
    }

    public void KeyPageUnInherit(KeyPage giver)
    {
        inheritKeyPageToKeyPageMap.Remove(giver);
    }

    public KeyPage GetInheritorKeyPage(KeyPage giver)
    {
        if (inheritKeyPageToKeyPageMap.ContainsKey(giver))
        {
            return inheritKeyPageToKeyPageMap[giver];
        }

        else
        {
            return null;
        }
    }

    public Librarian GetEquippedLibrarian(KeyPage keyPage)
    {
        if (keyPageToLibrarianMap.ContainsKey(keyPage))
        {
            return keyPageToLibrarianMap[keyPage];
        }

        else
        {
            return null;
        }
    }


    [Header("Customize FrontHair Parts")]
    public List<CustomizeHairParts> frontHairParts;

    [Header("Customize BackHair Parts")]
    public List<CustomizeHairParts> backHairParts;

    [Header("Customize Eye Parts")]
    public List<CustomizeFaceParts> eyeParts;

    [Header("Customize EyeBrow Parts")]
    public List<CustomizeFaceParts> eyeBrowParts;

    [Header("Customize Mouth Parts")]
    public List<CustomizeFaceParts> mouthParts;

    private static string KeyPageSavePath => Application.persistentDataPath + "/TestSave.json";
    private static string LibrarianSavePath => Application.persistentDataPath + "/TestLibSave.json";


    private void Start()
    {
        //Invoke("EquipT", 4f);
    }

    void EquipT()
    {

        librariansFloors[0].memberLibrarians[0].EquipKeyPage(keyPages[0]);
        /*
        foreach (KeyValuePair<KeyPage, Librarian> i in keyPageToLibrarianMap)
        {
            print("key = " + i.Key.page.name + " value = " + i.Value.name);
        }
        */
    }


    public void LoadReferenced()
    {



        LoadLibrarianData();

        foreach (var kp in keyPages)
        {
            /*
            foreach (int index in kp.referencedKeyPageIndices)
            {
                if (index >= 0 && index < keyPages.Count)
                {
                    kp.referencedKeyPages.Add(keyPages[index]);
                }
                else
                {
                    Debug.LogWarning("Invalid KeyPage index: " + index);
                }
            }
            */
        }
        keyPages[1].AddInheritKeyPage(keyPages[3]);

        isDataLoaded = true;
    }


    void LoadData()
    {

        if (File.Exists(KeyPageSavePath) == false || File.Exists(LibrarianSavePath) == false)
        {
            SaveData();
            SaveLibrarianData();
        }

        foreach (var keyPageSet in keyPageSets)
        {
            foreach (var keyPage in keyPageSet.keyPages)
            {
                keyPages.Add(keyPage);
            }
        }

        LoadKeyPageData();

        LoadReferenced();


    }

    void LoadKeyPageData()
    {   
        
        string json = File.ReadAllText(KeyPageSavePath);
        KeyPageSetListWrapper wrapper = JsonUtility.FromJson<KeyPageSetListWrapper>(json);

        List<KeyPageSetWrapper> setWrappers = wrapper.keyPageSetList;

        for (int i = 0; i < setWrappers.Count; i++)
        {           
            for (int j = 0; j < setWrappers[i].keyPageList.Count; j++)
            {
                
                KeyPage updatingKeyPage = keyPageSets[i].keyPages[j];
                KeyPageWrapper keyPageWrapper = setWrappers[i].keyPageList[j];

                foreach (var address in keyPageWrapper.cardDataAddresses)
                {
                    AsyncOperationHandle<CardData> cardOperation = Addressables.LoadAssetAsync<CardData>(address);
                    cardOperation.Completed += (cardHandle) =>
                    {
                        
                        updatingKeyPage.cards.Add(cardHandle.Result);
                        Addressables.Release(cardHandle);
                    };
                }
            }
        }


    }

    public void LoadLibrarianData()
    {

        // json을 파일 또는 PlayerPrefs에서 불러오기
        string json = File.ReadAllText(LibrarianSavePath);
        LibrariansFloorListWrapper wrapper = JsonUtility.FromJson<LibrariansFloorListWrapper>(json);


        for(int i = 0; i < wrapper.librariansFloorList.Count; ++i)
        {
            librariansFloors[i].UpdateFloorData(wrapper.librariansFloorList[i]);
        }

        /*
        librariansFloors = new List<LibrariansFloor>();

        // 모든 KeyPage 객체를 로드
        foreach (var data in wrapper.librariansFloorList)
        {

            librariansFloors.Add(data.ToLibrariansFloor());

        }
        */
    }

    [ContextMenu("Save KeyPage Json Data")]
    public void SaveData()
    {
        List<KeyPageWrapper> keyPageWrappers = new List<KeyPageWrapper>();

        foreach (var keyPage in keyPages)
        {
            keyPageWrappers.Add(new KeyPageWrapper(keyPage));
        }

        string json = JsonUtility.ToJson(new KeyPageListWrapper { keyPageList = keyPageWrappers }, true);
        File.WriteAllText(KeyPageSavePath, json);

    }

    [ContextMenu("Save KeyPageSet Json Data")]
    public void SaveKeyPageData()
    {
        List<KeyPageSetWrapper> keyPageSetWrappers = new List<KeyPageSetWrapper>();

        foreach (var keyPageSet in keyPageSets)
        {
            keyPageSetWrappers.Add(new KeyPageSetWrapper(keyPageSet));
        }

        string json = JsonUtility.ToJson(new KeyPageSetListWrapper { keyPageSetList = keyPageSetWrappers }, true);
        File.WriteAllText(KeyPageSavePath, json);

    }

    [ContextMenu("Save Librarian Json Data")]
    public void SaveLibrarianData()
    {

        List<LibrariansFloorWrapper> librariansFloorWrappers = new List<LibrariansFloorWrapper>();

        foreach(var floor in librariansFloors)
        {
            librariansFloorWrappers.Add(new LibrariansFloorWrapper(floor));
        }

        string json = JsonUtility.ToJson(new LibrariansFloorListWrapper { librariansFloorList = librariansFloorWrappers }, true);
        File.WriteAllText(LibrarianSavePath, json);

    }

    private static DataManager instance;

    public static DataManager Inst
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }

        else
        {
            Destroy(gameObject);
        }

        LoadData();

    }

}


[System.Serializable]
public class HeadCustomizeDataWrapper
{
    public int frontHairIndex;

    public int backHairIndex;

    public int eyeIndex;

    public int eyeBrowIndex;

    public int mouthIndex;

    public ColorData hairColor;

    public HeadCustomizeDataWrapper(HeadCustomizeData customizeData)
    {
        
        
        frontHairIndex = DataManager.Inst.frontHairParts.IndexOf(customizeData.frontHairParts);

        frontHairIndex = (frontHairIndex < 0) ? Random.Range(0, DataManager.Inst.frontHairParts.Count) : frontHairIndex;
        
        backHairIndex = DataManager.Inst.backHairParts.IndexOf(customizeData.backHairParts);

        backHairIndex = (backHairIndex < 0) ? Random.Range(0, DataManager.Inst.backHairParts.Count) : backHairIndex;

        eyeIndex = DataManager.Inst.eyeParts.IndexOf(customizeData.eyeParts);

        eyeIndex = (eyeIndex < 0) ? Random.Range(0, DataManager.Inst.eyeParts.Count) : eyeIndex;

        eyeBrowIndex = DataManager.Inst.eyeBrowParts.IndexOf(customizeData.eyeBrowParts);

        eyeBrowIndex = (eyeBrowIndex < 0) ? Random.Range(0, DataManager.Inst.eyeBrowParts.Count) : eyeBrowIndex;

        mouthIndex = DataManager.Inst.mouthParts.IndexOf(customizeData.mouthParts);

        mouthIndex = (mouthIndex < 0) ? Random.Range(0, DataManager.Inst.mouthParts.Count) : mouthIndex;

        hairColor = new ColorData(customizeData.hairColor);

        


    }

    public HeadCustomizeData ToHeadCustomizeData()
    {
        HeadCustomizeData customizeData = new HeadCustomizeData();

        customizeData.frontHairParts = DataManager.Inst.frontHairParts[frontHairIndex];

        customizeData.backHairParts = DataManager.Inst.backHairParts[backHairIndex];
        
        customizeData.eyeParts = DataManager.Inst.eyeParts[eyeIndex];

        customizeData.eyeBrowParts = DataManager.Inst.eyeBrowParts[eyeBrowIndex];

        customizeData.mouthParts = DataManager.Inst.mouthParts[mouthIndex];

        
        customizeData.hairColor = hairColor.ToRGB();

        return customizeData;

    }




}

[System.Serializable]
public class ColorData
{

    public float h;
    public float s;
    public float v;

    public ColorData(Color color)
    {
        Color.RGBToHSV(color, out h, out s, out v);
    }

    public Color ToRGB()
    {
        return Color.HSVToRGB(h, s, v);
    }

}

[System.Serializable]
public class HeadCustomizeData
{
    public CustomizeHairParts frontHairParts;

    public CustomizeHairParts backHairParts;

    public CustomizeFaceParts eyeParts;

    public CustomizeFaceParts eyeBrowParts;

    public CustomizeFaceParts mouthParts;

    public Color hairColor;
}


[System.Serializable]
public class CustomizeParts
{

    public Sprite partsSprite;

    public Vector3 partsPos;

}

[System.Serializable]
public class CustomizeFaceParts
{
    public CustomizeParts frontNormal;

    public CustomizeParts frontDamaged;

    public CustomizeParts frontAttack;

    public CustomizeParts sideAttack;
}

[System.Serializable]
public class CustomizeHairParts
{
    public CustomizeParts frontHair;

    public CustomizeParts sideHair;
}

