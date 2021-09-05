using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Animator animator;
    public GameObject sounds;
    public GameObject music;
    public Sprite soundOn;
    public Sprite soundOff;
    public Sprite musicOn;
    public Sprite musicOff;
    private bool isOpen;

    private void Start()
    {
        sounds.GetComponent<Image>().sprite = DataManager.Data.sounds ? soundOn : soundOff;
        music.GetComponent<Image>().sprite = DataManager.Data.music ? musicOn : musicOff;
    }

    public void SettingsButton()
    {
        isOpen = !isOpen;
        MenuCamera.active = !isOpen;
        animator.SetBool("isOpen", isOpen);
        FindObjectOfType<LevelWindowOpen>().animator.SetBool("isOpen", false);
        if (!isOpen) FindObjectOfType<QuestionManager>().EndQuestion();
        FindObjectOfType<LocalizationManager>().animator.SetBool("isOpen", false);
    }
    
    public void Sounds()
    {
        DataManager.Data.sounds = !DataManager.Data.sounds;
        DataManager.SaveField();
        sounds.GetComponent<Image>().sprite = DataManager.Data.sounds ? soundOn : soundOff;
    }
    
    public void Music()
    {
        DataManager.Data.music = !DataManager.Data.music;
        DataManager.SaveField();
        music.GetComponent<Image>().sprite = DataManager.Data.music ? musicOn : musicOff;
    }
    
    public void Language(bool opened)
    {
        FindObjectOfType<LocalizationManager>().IsOpen(opened);
    }
    
    public void Ads()
    {
        
    }

    public void CloseTheGame()
    {
        Application.Quit();
    }
}
