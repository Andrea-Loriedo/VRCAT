using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldController {

	public InputField InputField;
	public object content;

	public void SetField(InputField field)
	{
		InputField = field;
	}

	public object GetContent(object ct)
	{
		content = InputField.text;
		return content;
	}
}
