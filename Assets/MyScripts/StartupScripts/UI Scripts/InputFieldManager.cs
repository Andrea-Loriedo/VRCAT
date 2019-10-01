using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldManager : MonoBehaviour {

	InputFieldController ageIF = new InputFieldController();
	InputFieldController trialsIF = new InputFieldController();
	[SerializeField] InputField ageInputField;
	[SerializeField] InputField trialsInputField;

	[HideInInspector]
	public string age;
	[HideInInspector]
	public string trialsPerBlock;

	void Start () {
		ageIF.SetField(ageInputField);
		trialsIF.SetField(trialsInputField);
	}

    public string SetParticipantAge()
	{
		object content = ageIF.GetContent(age);
		age = content.ToString();
		return age;
	}

	public string SetTrialsPerBlock()
	{
		object content = trialsIF.GetContent(trialsPerBlock);
		trialsPerBlock = content.ToString();
		return trialsPerBlock;
	}

}
