using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UXF;

public class UIController : MonoBehaviour {

	public bool testMode;

	public DropdownManager dropdownPrefab;
    public InputFieldManager inputFieldPrefab;
	public PopupController popupController;

	public string ppid;
	public string age;
	public string gender;
	public string handedness;

    public bool formIncomplete;
	public bool taskSelected;
	bool conditionsTicked;

    void Awake()
    {
        formIncomplete = true;
    } 

	public void TurnOff()
    {
        gameObject.SetActive(false);
    }

	public void TurnOn()
    {
        gameObject.SetActive(true);
    }

	public void SubmitForm()
	{
		inputFieldPrefab.SetParticipantAge();
        CheckFormComplete();
	}

	bool CheckFormComplete() 
    {
		// GetConditionsTicked();

        if (string.IsNullOrEmpty(inputFieldPrefab.age) || string.IsNullOrEmpty(dropdownPrefab.selectedGender))
        {
            formIncomplete = true;
            Debug.LogFormat("Form incomplete!");
			
			return formIncomplete;
        }
        else
        {
            formIncomplete = false;
            Debug.LogFormat("Form completed successfully");
			CheckIfTestMode();
			
			return formIncomplete;
        }
    }

	bool CheckTaskSelected(List<AssessmentParameters> tasks)
	{
		if (tasks.Count == 0)
		{
			taskSelected = false;
		}
		else
		{
			taskSelected = true;
		}
		return taskSelected;
	}

	bool CheckConditionsTicked(List<int> conditions)
	{
		if (conditions.Count < 2)
		{
			conditionsTicked = false;
		}
		else
		{
			conditionsTicked = true;
		}
		return taskSelected;
	}

	public List<AssessmentParameters> GetSelectedTasks()
	{
		// get the number of children of the TaskSelections object
		var taskParamUIs = GetComponentsInChildren<TaskParamsUI>();

		List<AssessmentParameters> taskParams = new List<AssessmentParameters>();

		foreach (var taskParamUI in taskParamUIs)
		{
			// if the child tickbox is checked, add assessment parameters to the taskparams list
			if (taskParamUI.IsChecked) taskParams.Add(taskParamUI.GetParameters());
		}

		CheckTaskSelected(taskParams);

		return taskParams;
	}

	public List<int> GetConditionsTicked()
	{
		var termsAndConditions = GetComponentsInChildren<TermsAndConditions>();

		List<int> tickedConditions = new List<int>();

		foreach (var condition in termsAndConditions)
		{
			if (condition.IsChecked) tickedConditions.Add(1);		
		}

		CheckConditionsTicked(tickedConditions);

		return tickedConditions;
	}

	public void GeneratePpid()
    {
        DateTime dt = DateTime.Now;
        ppid = dt.ToString("P_yyMMddHHmmss");
    }

	public void CheckIfTestMode()
    {
        if (!testMode)
        {
            GeneratePpid();
        }
        else
        {
            ppid = "Test";
        }
    }  

	public FormData GetFormData()
	{
		return new FormData()
		{
			ppid = ppid,
			age = inputFieldPrefab.age,
			gender = dropdownPrefab.selectedGender,
			// handedness = dropdownPrefab.selectedHand
		};
	}

	public void ShowFormIncompletePopup()
	{
		Popup existsWarning = new Popup();
		existsWarning.messageType = MessageType.Warning;
		existsWarning.message = string.Format("Form Incomplete! Please fill all the fields before starting the experiment");
		existsWarning.onOK = new Action(() => {});
		popupController.DisplayPopup(existsWarning);
	}

	public void ShowSelectTaskPopup()
	{
		Popup taskSelectionMissing = new Popup();
		taskSelectionMissing.messageType = MessageType.Error;
		taskSelectionMissing.message = string.Format("Please select at least one task before starting the experiment");
		taskSelectionMissing.onOK = new Action(() => {});
		popupController.DisplayPopup(taskSelectionMissing);
	}
}

public struct FormData
{
	public string ppid;
	public string age;
	public string gender;
	public string handedness;
}
