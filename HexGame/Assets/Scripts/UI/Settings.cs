using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Settings : MonoBehaviour
    {
        public DataManager dataManager;

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
            dataManager = GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>();
            sounds.GetComponent<Image>().sprite = dataManager.data.sounds ? soundOn : soundOff;
            music.GetComponent<Image>().sprite = dataManager.data.music ? musicOn : musicOff;
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
            dataManager.data.sounds = !dataManager.data.sounds;
            dataManager.SaveField();
            sounds.GetComponent<Image>().sprite = dataManager.data.sounds ? soundOn : soundOff;
        }

        public void Music()
        {
            dataManager.data.music = !dataManager.data.music;
            dataManager.SaveField();
            music.GetComponent<Image>().sprite = dataManager.data.music ? musicOn : musicOff;
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
}
