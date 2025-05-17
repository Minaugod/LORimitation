using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LibrarianManager : MonoBehaviour
{



    public LibrariansFloor[] librariansFloors;

    private void Start()
    {

        /*
        foreach (LibrariansFloor floor in librariansFloors)
        {

            foreach(Librarian librarian in floor.librarians)
            {
                librarian.keyPage = librarian.basicKeyPage;
            }

        }
        */
    }

    private static LibrarianManager instance;

    public static LibrarianManager Inst
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

    }

}
