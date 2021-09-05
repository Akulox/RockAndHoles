using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationManager : MonoBehaviour
{
    private Dictionary<string, string> localizedText;

    public delegate void ChangeLangText();
    public event ChangeLangText OnLanguageChanged;
    public Animator animator;
    
    void Awake()
    {
        DataManager.LoadField();
        LoadLocalizedText(DataManager.Data.language);
    }
    
    public void ChangeLang(string lang)
    {
        DataManager.Data.language = lang;
        DataManager.SaveField();
        LoadLocalizedText(lang);
    }
    public void IsOpen(bool isOpen)
    {
        animator.SetBool("isOpen", isOpen);
        GameObject.FindGameObjectWithTag("GameStarter")?.GetComponent<GameIntroduce>().LangChosen();
    }
    public void LoadLocalizedText(string langName)
    {
        string path = Application.streamingAssetsPath + "/Languages/" + langName + ".json";
        string dataAsJson;
 
        if (Application.platform == RuntimePlatform.Android)
        {
            var reader = new WWW(path);
            while (!reader.isDone) { }
 
            dataAsJson = reader.text;
        }
        else
        {
            dataAsJson = File.ReadAllText(path);
        }
        localizedText = JSONLanguageParser(dataAsJson);

        DataManager.Data.language = langName;

        OnLanguageChanged?.Invoke();
    }
 
    public string GetLocalizedValue(string key)
    {
        if (localizedText.ContainsKey(key))
        {
            return localizedText[key];
        } 
        throw new Exception("Localized text with key \"" + key + "\" not found");
    }

    private Dictionary<string, string> JSONLanguageParser(string dataAsJSON)
    {
        Dictionary<string,string> file = new Dictionary<string,string>();
        int state = 0;
        string key = "";
        string value = "";
        
        foreach (char letter in dataAsJSON)
        {
            if (state == 0)
            {
                if (letter == '"') state++;
                continue;
            }
            if (state == 1)
            {
                if (letter != '"') { key += letter;
                    continue;
                }
                state++;
                continue;
            }
            if (state == 2)
            {
                if (letter == '"') state++;
                continue;
            }
            if (state == 3)
            {
                if (letter != '"')
                {
                    value += letter;
                    continue;
                }
                file.Add(key, value);
                key = value = "";
                state = 0;
            }
        }
        return file;
    }
}
