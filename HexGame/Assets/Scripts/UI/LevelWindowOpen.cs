using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class LevelWindowOpen : MonoBehaviour
    {
        public SceneController sceneController;
        public DataManager dataManager;

        public Sprite levelOpened;
        public Sprite levelClosed;
        public GameObject levelButton;
        public Text text;
        public Animator animator;
        public static DataManager.Level currentLevel;
        private bool _levelUnlocked;

        public void Open(int level)
        {
            MenuCamera.active = false;
            currentLevel = dataManager.FindLevelById(level);
            _levelUnlocked = dataManager.UnlockedProperties(currentLevel.toUnlock);
            levelButton.GetComponent<Image>().sprite = _levelUnlocked ? levelOpened : levelClosed;
            text.text = GameObject.FindGameObjectWithTag("LocalizationManager").GetComponent<LocalizationManager>()
                .GetLocalizedValue(currentLevel.name);
            animator.SetBool("isOpen", true);
        }
        public void Close()
        {
            MenuCamera.active = true;
            animator.SetBool("isOpen", false);
        }

        public void OpenLevel()
        {
            if (_levelUnlocked)
            {
                SceneController.CurrentScene = currentLevel.id + SceneController.levelCountStart;
                sceneController.OpenLevel();
                animator.SetBool("isOpen", false);
            }
        }
    }
}
