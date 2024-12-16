using UnityEngine;
using UnityEngine.Playables; 
using UnityEngine.SceneManagement; 

public class TimelineToSceneLoader : MonoBehaviour
{
    public PlayableDirector timelineDirector;
    public string sceneNameToLoad;
    private const string TimelinePlayedKey = "TimelinePlayed";

    void Start()
    {
        if (PlayerPrefs.GetInt(TimelinePlayedKey) == 1)
        {
            Destroy(timelineDirector.gameObject);
            LoadNextScene();
            return;
        }
       
        if (timelineDirector != null)
        {
            timelineDirector.stopped += OnTimelineStopped;
            timelineDirector.Play();
        }
       
    }

    void OnTimelineStopped(PlayableDirector director)
    {
        if (director == timelineDirector)
        {
            
            PlayerPrefs.SetInt(TimelinePlayedKey, 1);
            PlayerPrefs.Save();
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(sceneNameToLoad))
        {
            SceneManager.LoadScene(sceneNameToLoad);
        }
        else
        {
            Debug.LogError("Yüklenecek sahne adý boþ!");
        }
    }

    void OnDestroy()
    {
        if (timelineDirector != null)
        {
            timelineDirector.stopped -= OnTimelineStopped;
        }
    }
}
