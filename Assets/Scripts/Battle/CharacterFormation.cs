using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFormation : MonoBehaviour
{
    [SerializeField]
    private Transform[] enemyFormationArr;

    [SerializeField]
    private Transform[] teamFormationArr;

    Queue<Transform> enemyFormations = new Queue<Transform>();

    Queue<Transform> teamFormations = new Queue<Transform>();

    public Transform GetEnemyFormation()
    {
        if (enemyFormations.Count > 0) return enemyFormations.Dequeue();
        else return null;

    }

    public Transform GetTeamFormation()
    {
        if (teamFormations.Count > 0) return teamFormations.Dequeue();
        else return null;
    }


    private void EnqueueFormation()
    {
        foreach(Transform formation in enemyFormationArr)
        {
            enemyFormations.Enqueue(formation);
        }

        foreach (Transform formation in teamFormationArr)
        {
            teamFormations.Enqueue(formation);
        }
    }


    private static CharacterFormation instance;

    public static CharacterFormation Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
        EnqueueFormation();
    }

}
