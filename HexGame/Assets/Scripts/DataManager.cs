using System;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
        path = Application.persistentDataPath + "/DataPlus.json";
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
        public bool first_dialogue_passed;
        public Level[] levels;
    }
    
    [System.Serializable]
    public class Level
    { 
        public string name;
        public string[] toUnlock;
        public bool dialogue_passed;
        public bool level_completed;
    }
    
    public bool UnlockedProperties(string[] levels)
    {
        bool unlock = true;
        foreach (var level in levels)
        {
            if (FindLevelByName(level) != null) unlock &= FindLevelByName(level).level_completed;
        }
        return unlock;
    }
    
    public Level FindLevelByName(string requiredLevel)
    {
        foreach (var level in data.levels)
        {
            if (requiredLevel == level.name) return level;
        }
        return data.levels[0];
    }
}