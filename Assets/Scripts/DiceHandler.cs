using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceHandler : MonoBehaviour
{

    public List<DiceController> dices = new List<DiceController>();

    [SerializeField]
    private Character character;

    [SerializeField]
    private Transform dicesTransform;

    [SerializeField]
    private Transform lightTransform;



    [SerializeField]
    public List<CostLight> lights = new List<CostLight>();

    private void DeadCharacter()
    {

        BattleManager.Instance.onNextAct -= NextAct;

        for (int i = 0; i < dices.Count; i++)
        {
            dices[i].EndGame();
        }

        for (int i = 0; i < lights.Count; i++)
        {
            lights[i].EndGame();
        }

        dices.Clear();
        lights.Clear();

        gameObject.SetActive(true);
    }

    public void Init(Character character)
    {
        this.character = character;

        character.stat.onDeadCharacter += DeadCharacter;

        BattleManager.Instance.onSpdDecide += SpeedDecide;

        BattleManager.Instance.onStartBattle += StartBattle;

        BattleManager.Instance.onNextAct += NextAct;

        BattleManager.Instance.onEndGame += DeadCharacter;
            

        AddDice();
        AddLight();
        AddLight();
        AddLight();
    }

    private void StartBattle()
    {
        gameObject.SetActive(false);

        for (int i = 0; i < dices.Count; i++)
        {

            dices[i].StartBattle();
            dices[i].diceUi.StartBattle();

        }
    }

    private void NextAct()
    {
        gameObject.SetActive(true);

        for (int i = 0; i < dices.Count; i++)
        {

            dices[i].NextAct();

        }
    }

    public void AddDice()
    {

        DiceController dice = UiManager.Instance.AddDice(character.IsEnemy);
        dice.transform.SetParent(dicesTransform);
        dice.character = this.character;



        dices.Add(dice);


    }

    public void AddLight()
    {

        CostLight light = Instantiate(UiManager.Instance.lightPrefab).GetComponent<CostLight>();
        light.transform.SetParent(lightTransform);
        lights.Add(light);

        RechargeLight();

    }

    public void ResetLightAnim()
    {
        for(int i = 0; i < lights.Count; i++)
        {
            lights[i].ResetBlink();
        }
    }
    public void RechargeLight()
    {
        if (character.stat.haveLight + 1 <= lights.Count)
        {
            lights[character.stat.haveLight].EnableLight();
            character.stat.haveLight += 1;
        }


    }

    public void RechargeAllLight()
    {
        foreach(CostLight light in lights)
        {
            light.EnableLight();
        }

        character.stat.haveLight = lights.Count;
    }

    public void SpeedDecide()
    {


        for (int i = 0; i < dices.Count; i++)
        {

            dices[i].diceSpd = character.stat.DiceSpdDecide();

            for (int j = 0; j < (dices.Count - 1); j++)
            {

                if (dices[j].diceSpd < dices[j + 1].diceSpd)
                {
                    int temp = dices[j].diceSpd;
                    dices[j].diceSpd = dices[j + 1].diceSpd;
                    dices[j + 1].diceSpd = temp;
                }

            }
        }

        for (int i = 0; i < dices.Count; i++)
        {

            dices[i].SpeedResult();

        }
    }

}
