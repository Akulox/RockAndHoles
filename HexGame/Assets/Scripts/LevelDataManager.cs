using UnityEngine;

public class LevelDataManager : MonoBehaviour
{
    public GameObject cellArea;
    public GameObject diceArea;
    public LevelData[] levelData;
    
    public class LevelData
    {
        public string name;

        public GameObject[] cells;
        public GameObject[] dices;
    }

    public void SaveLevel()
    {
        
    }
}
