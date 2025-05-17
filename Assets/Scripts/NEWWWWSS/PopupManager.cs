using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{

    public bool isCloseable = true;

    public Stack<Ui_Panel> openPopups = new Stack<Ui_Panel>();

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            CloseLastPopup();
        }
    }



    public void OpenedPopup(Ui_Panel panel)
    {
        if(panel != null)
        {
            openPopups.Push(panel);
        }

    }

    public void ClosePopup(Ui_Panel panel)
    {

        if (isCloseable)
        {
            if (panel != null && openPopups.Contains(panel))
            {
                var popup = openPopups.Pop();
                popup.gameObject.SetActive(false);
                print(popup);
            }
        }

    }

    public void CloseLastPopup()
    {
        if (openPopups.Count > 0)
        {
            ClosePopup(openPopups.Peek());
        }
    }

    public void CloseAllPopups()
    {
        while (openPopups.Count > 0) ClosePopup(openPopups.Peek());
    }







    private static PopupManager instance;

    public static PopupManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }

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
    }
}
