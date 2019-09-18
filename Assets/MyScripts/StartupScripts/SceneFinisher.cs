using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFinisher : MonoBehaviour
{

    public void FinishScene()
    {
        SceneLoader sceneLoader = FindObjectOfType<SceneLoader>();

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
