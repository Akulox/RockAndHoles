namespace GamePlayHexes.DiceClasses
{
    public class GoodDices : Dice
        {
            public override void RemoveDice()
            {
                VarManager.Lose();
                base.RemoveDice();
            }
        }
}
    
