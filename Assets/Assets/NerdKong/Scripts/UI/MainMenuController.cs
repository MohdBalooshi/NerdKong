using UnityEngine;
using TMPro;

namespace NerdKong.UI
{
    public class MainMenuController : MonoBehaviour
    {
        public TMP_Text titleText;
        public TMP_Text subtitleText;
        public TMP_Text versionText;

        public void PlayQuick()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("CategorySelect");
        }

        public void OpenSettings()
        {
            Debug.Log("Settings TODO");
        }

        public void ExitGame()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
    }
}
