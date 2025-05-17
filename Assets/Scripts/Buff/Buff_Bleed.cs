public class Buff_Bleed : BuffUi
{



    private void Start()
    {
        target.stat.ApplyBuff.AddListener(() =>
        {

            target.stat.nextTurnBuff.bleed = target.stat.buff.bleed;

        });

        target.behaviour.onDiceRoll += ApplyBuff;
    }

    private void ApplyBuff(Character character, EnumTypes.DiceType type)
    {
        if (target.stat.buff.bleed > 0)
        {
            if (type is EnumTypes.DiceType.Slash or EnumTypes.DiceType.Pierce or EnumTypes.DiceType.Blunt)
            {

                BattleEffectAlert alert = BattleEffectAlertPool.Instance.GetAlert();

                alert.InitBuffEffect(target, icon.sprite, buffDesc);

                target.stat.TakeJustDamage(target.stat.buff.bleed);

                target.stat.buff.bleed = target.stat.buff.bleed / 2;
                target.stat.nextTurnBuff.bleed = target.stat.buff.bleed;

            }
        }

    }

    private void OnDestroy()
    {
        target.behaviour.onDiceRoll -= ApplyBuff;
    }



    private void Update()
    {

        if (target != null && target.stat.buff.bleed > 0 || target.stat.nextTurnBuff.bleed > 0)
        {
            value.text = target.stat.nextTurnBuff.bleed.ToString();
        }

        if (target.stat.buff.bleed <= 0 && target.stat.nextTurnBuff.bleed <= 0) Destroy(gameObject);

    }
    

}
