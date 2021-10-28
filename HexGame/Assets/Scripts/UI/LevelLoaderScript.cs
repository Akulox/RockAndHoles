// using System.Collections;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using GamePlayHexes;
//
// namespace UI
// {
//     public class LevelLoaderScript : MonoBehaviour
//     {
//         public Animator transition;
//         public float transitionTime = 1f;
//
//         public void Restart()
//         {
//             OpenLevel(SceneManager.GetActiveScene().name);
//         }
//
//         public void OpenLevel(string level)
//         {
//             VarManager.DictionariesClear();
//             StartCoroutine(LoadLevel(level));
//         }
//
//         IEnumerator LoadLevel(string level)
//         {
//             transition.SetTrigger("Start");
//
//             yield return new WaitForSeconds(transitionTime);
//
//             SceneManager.UnloadSceneAsync("LevelMap");
//             SceneManager.LoadSceneAsync("Field", LoadSceneMode.Additive);
//             SceneManager.LoadSceneAsync(level, LoadSceneMode.Additive);
//         }
//
//         public void QuitLevel()
//         {
//             OpenLevel("LevelMap");
//         }
//     }
// }
