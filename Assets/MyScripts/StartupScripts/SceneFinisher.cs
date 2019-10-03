using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class SceneFinisher : MonoBehaviour
{
    bool manuallyFinished = false;

    void Start()
    {
        manuallyFinished = false;
    }

    public void FinishScene()
    {
        SceneLoader sceneLoader = FindObjectOfType<SceneLoader>();

        Session session = FindObjectOfType<Session>();

        if (!session.settings.GetBool("show_stats") || manuallyFinished)
        {
            if (sceneLoader != null)
            {
                sceneLoader.finishedScene = true;
            }
            else
            {
                #if UNITY_EDITOR
                            UnityEditor.EditorApplication.isPlaying = false;
                #else
                            Application.Quit();
                #endif	
            }
        }
    }

    public void EndSceneManually()
    {
        manuallyFinished = true;
        FinishScene();
    }

}
