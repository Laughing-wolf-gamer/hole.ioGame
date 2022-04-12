using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class AceGamePlayAndStop : EditorWindow
{
    [MenuItem("DayDreamz Games/PlayAndStop _F1")]
    static void PlayAndStop()
    {
        if (EditorApplication.isPlaying == false)
        {
            EditorApplication.isPlaying = true;
        }
        else if (EditorApplication.isPlaying == true)
        {
            EditorApplication.isPlaying = false;
        }
    }

/* 
    [MenuItem("Ace Games/Play in EditMode _F8")]
    static void EditMode()
    {
        FindObjectOfType<GameController>().inLevelEditingMode = true;
        PlayAndStop();
    }
*/
    [MenuItem("DayDreamz Games/TottleUI _F4")]
    static void TottleUIc()
    {
        FindObjectOfType<Canvas>().enabled = !FindObjectOfType<Canvas>().enabled;
    }

    [MenuItem("DayDreamz Games/PauseAndresume _F3")]
    static void pauseAndResume()
    {
        if (EditorApplication.isPaused == false)
        {
            EditorApplication.isPaused = true;
        }
        else if (EditorApplication.isPaused == true)
        {
            EditorApplication.isPaused = false;
        }

    }

/* 
    [MenuItem("Ace Games/Write Sound Strings")]
    static void SoundStrings()
    {

        SoundController SC = FindObjectOfType<SoundController>();

        if (SC != null)
        {
            SC.SetUpData();
        }

    }*/

    [MenuItem("DayDreamz Games/Delete Prefs")]
    static void DeletePrefs()
    {
        PlayerPrefs.DeleteAll();
        EditorPrefs.DeleteAll();

    }
/* 
    [MenuItem("Ace Games/Spawn 1")]
    static void car262()
    {
        PlayerPrefs.SetInt("PlayerIndex", 0);
        PlayAndStop();
    }

    [MenuItem("Ace Games/Spawn 2 ")]
    static void car26()
    {
        PlayerPrefs.SetInt("PlayerIndex", 1);
        PlayAndStop();
    }

    [MenuItem("Ace Games/Spawn 3")]
    static void car35()
    {
        PlayerPrefs.SetInt("PlayerIndex", 2);
        PlayAndStop();
    }


    //	[MenuItem ("Ace Games/RandomCarAndRevel _F8")]
    static void carRandom()
    {
        EditorApplication.isPlaying = false;
        PlayerPrefs.SetInt("PlayerIndex", Random.Range(0, 5));

        int lvlID = Random.Range(2, 41);

        EditorSceneManager.OpenScene(EditorBuildSettings.scenes[lvlID].path);

        PlayAndStop();
    }
*/

}
