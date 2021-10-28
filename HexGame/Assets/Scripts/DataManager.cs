using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UI;

public class DataManager : MonoBehaviour
{
    public Item data;

    private void Awake()
    {
        LoadField();
    }
    
    
    [ContextMenu("Save")]
    public void SaveField()
    {
        string path = Application.persistentDataPath + "/DataPlus.json";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    
    [ContextMenu("Load")]
    public void LoadField()
    {
        string path = Application.persistentDataPath + "/DataPlus.json";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);
        
        data = formatter.Deserialize(stream) as Item;
        stream.Close();
    }

    [System.Serializable]
    public class Item
    {
        public string language;
        public bool noAds;
        public bool sounds;
        public bool music;
        public bool firstDialoguePassed;
        public Level[] levels;
    }
    
    [System.Serializable]
    public class Level
    {
        public string name;
        public int id;
        public int[] toUnlock;
        public bool dialoguePassed;
        public bool levelCompleted;
    }
    
    public bool UnlockedProperties(int[] levels)
    {
        bool unlock = true;
        foreach (var level in levels)
        {
            unlock &= FindLevelById(level).levelCompleted;
        }
        return unlock;
    }
    
    public Level FindLevelById(int requiredLevel)
    {
        return data.levels[requiredLevel];
    }
}