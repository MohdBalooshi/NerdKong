using UnityEngine;
using UnityEngine.SceneManagement;

namespace NerdKong.App
{
    public class SceneLoader : MonoBehaviour
    {
        public void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);
        public void QuitApp()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
    }
}
