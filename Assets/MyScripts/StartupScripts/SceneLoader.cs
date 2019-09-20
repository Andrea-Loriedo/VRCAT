using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using UXF;

public class SceneLoader : MonoBehaviour
{
    public HeadAligner headAligner;
    public UIController form;
    public GameObject headTarget;
    public Transform eye;

    [HideInInspector]
    public bool finishedScene;

    public Session session;

    public string saveFolderName = "VRCAT_Data";
    bool readyToStart = false;
    bool atLeastOneTask;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        readyToStart = false;
    } 

    public void RunAllSelectedTasks()
    {   
        var selectedScenes = form.GetSelectedTasks();
        
        readyToStart = ! form.formIncomplete;
        atLeastOneTask = form.taskSelected;
        
        if(readyToStart && atLeastOneTask)
        {
            headTarget.SetActive(true);
            var formSelections = form.GetFormData();
            form.TurnOff();
            StartCoroutine(SceneSequence(selectedScenes, formSelections));
        }
        else if (!readyToStart)
        {
            form.ShowFormIncompletePopup();
        }
        else if (!atLeastOneTask)
        {
            form.ShowSelectTaskPopup();
        }
    }

    IEnumerator SceneSequence(List<AssessmentParameters> selectedScenes, FormData formSelections)
    {
        yield return new WaitUntil(() => headAligner.isAligned);

        Debug.LogFormat("Participant {0} is a {1} years old {2}.", formSelections.ppid, formSelections.age, formSelections.gender);

        // make dictionary for participant details
        Dictionary<string, object> ppDetails = new Dictionary<string, object>()
        {   
            { "PPID", formSelections.ppid },
            { "Height", eye.position.y },
            { "Age", formSelections.age },
            { "Gender", formSelections.gender },
            // { "Writing hand", formSelections.handedness }
        };

        foreach (var sceneSelection in selectedScenes)
        {
            SceneManager.LoadScene(sceneSelection.sceneName, LoadSceneMode.Single);
            finishedScene = false;

            yield return null;
            // Find the "Session" in the selected scene
            Session session = FindObjectOfType<Session>();
            // Begin the session using the params
            session.Begin(sceneSelection.taskName, formSelections.ppid, SaveFolderPath, 1, ppDetails, sceneSelection.settings);
            // disable UI
            ExperimentStartupController startUpController = FindObjectOfType<ExperimentStartupController>();

            if (startUpController != null)
            {
                startUpController.gameObject.SetActive(false);
            }

            // save participant details to .JSON file
            session.WriteDictToSessionFolder(new Dictionary<string, object>(ppDetails), "participant_details");

            yield return new WaitUntil(() => finishedScene);
        }

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif	

    }

    string SaveFolderPath
    {
        get
        {
            string saveFolderPath = UXF.Extensions.CombinePaths(Application.dataPath, "..", saveFolderName);
            if (!Directory.Exists(saveFolderPath))
                Directory.CreateDirectory(saveFolderPath);

            return saveFolderPath;
        }
    }
}