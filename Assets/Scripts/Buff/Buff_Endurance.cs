
public class Buff_Endurance : BuffUi
{




    int duration = 1;

    private void Start()
    {
        target.stat.ApplyBuff.AddListener(() =>
        {

            if (duration == 0)
            {
                Destroy(gameObject);
            }

            duration--;

        });


        target.behaviour.onDiceRoll += ApplyBuff;
    }



    public void ApplyBuff(Character target, EnumTypes.DiceType type)
    {


        target.stat.diceBonusValueDic[EnumTypes.DiceType.Block] += target.stat.buff.endurance;
        target.stat.diceBonusValueDic[EnumTypes.DiceType.Evade] += target.stat.buff.endurance;


    }

    private void Update()
    {

        if (target != null && duration > 0)
        {
            value.text = target.stat.nextTurnBuff.endurance.ToString();
        }

    }

}
