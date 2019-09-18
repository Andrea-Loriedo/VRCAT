using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CustomDropdownController {

	Dropdown dropdown;

	public List<string> optionNames = new List<string>();
	
	public void AssignDropdown(Dropdown dd)
	{
		dropdown = dd;
	}
	
	public void SetOptions(List<string> options)
	{
		optionNames = options;
		dropdown.AddOptions(optionNames);
	}
	
	public string GetContents()
	{
		return optionNames[dropdown.value]; // get the selected option name
	}

	// get the default option in the dropdown (index = 0)
	public string GetDefault(List<string> options)
	{
		return optionNames[0];
	}

	public void Clear()
	{
		// set the index to 0
		dropdown.value = 0;
	}

}
