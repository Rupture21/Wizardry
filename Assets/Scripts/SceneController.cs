using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void SceneChange(string name)
    {
        SceneManager.LoadScene(name);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
