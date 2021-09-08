using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelWindowOpen : MonoBehaviour
    {
        public DataManager dataManager;

        public Sprite levelOpened;
        public Sprite levelClosed;
        public GameObject levelButton;
        public Text text;
        public Animator animator;
        public DataManager.Level currentLevel;
        private bool _open;
        private bool _levelUnlocked;

        public void IsOpen(string level)
        {
            MenuCamera.active = _open;
            _open = !_open;
            if (_open)
            {
                currentLevel = dataManager.FindLevelByName(level);
                _levelUnlocked = dataManager.UnlockedProperties(currentLevel.toUnlock);
                levelButton.GetComponent<Image>().sprite = _levelUnlocked ? levelOpened : levelClosed;
                text.text = GameObject.FindGameObjectWithTag("LocalizationManager").GetComponent<LocalizationManager>()
                    .GetLocalizedValue(level);

            }

            animator.SetBool("isOpen", _open);
        }

        public void OpenLevel()
        {
            if (_levelUnlocked)
            {
                FindObjectOfType<LevelLoaderScript>().OpenLevel(currentLevel.name);
            }
        }
    }
}
