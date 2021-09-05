using DiceClasses;

public class ExplosiveDice : Dice
{
    public readonly int[,] Directions = {{1, 0}, {1, 1}, {0, 1}, {-1, 0}, {-1, -1}, {0, -1}}; 
    public override void RemoveDice()
    {
        if (onFire)
        {
            for (int d = 0; d < 6; d++)
            {
                if (CanGetDiceValue($"{row + Directions[d, 0]}_{col + Directions[d, 1]}"))
                {
                    VarManager.Dices[$"{row + Directions[d, 0]}_{col + Directions[d, 1]}"].onFire = true;
                    VarManager.Dices[$"{row + Directions[d, 0]}_{col + Directions[d, 1]}"].RemoveDice();
                }
            }
        }
        base.RemoveDice();
    }
}
