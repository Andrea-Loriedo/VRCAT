using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldManager : MonoBehaviour {

	InputFieldController ageIF = new InputFieldController();
	public InputField ageInputField;

	[HideInInspector]
	public string age;

	void Start () {
		ageIF.SetField(ageInputField);
	}

    public string SetParticipantAge()
	{
		object content = ageIF.GetContent(age);
		age = content.ToString();
		return age;
	}

}
