using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public PlayableDirector timeline;
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void PlayTimeline()
    {
        if (timeline != null)
        {
            timeline.Play();
        }
        else
        {
            Debug.LogWarning("Nenhuma Timeline atribuída ao TimelineController!");
        }
    }
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
