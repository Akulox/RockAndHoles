using DiceClasses;

public class EvilDice : Dice
{
    public override bool CheckDiceProperties()
    {
        return false;
    }
}
