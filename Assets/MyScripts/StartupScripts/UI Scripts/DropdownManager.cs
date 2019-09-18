using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DropdownManager : MonoBehaviour {

	CustomDropdownController genderDd = new CustomDropdownController();
	// CustomDropdownController handsDd = new CustomDropdownController();

	public Dropdown genderDropdown;
	// public Dropdown handsDropdown;

	[HideInInspector]
	public string selectedGender;
	// [HideInInspector]
	// public string selectedHand;

	List<string> genders = new List<string>() {"Male", "Female"};
	// List<string> hands = new List<string>() {"Right", "Left"};

	void Start () {
		genderDd.AssignDropdown(genderDropdown);
		// handsDd.AssignDropdown(handsDropdown);
		genderDd.SetOptions(genders);
		// handsDd.SetOptions(hands);
		selectedGender = genderDd.GetDefault(genders);
		// selectedHand = handsDd.GetDefault(hands);
	}

	public void GenderDropdownIndexChanged(int i)
	{
		selectedGender = genderDd.GetContents();
	}

	// public void HandednessDropdownIndexChanged(int i)
	// {
	// 	selectedHand = handsDd.GetContents();
	// }
}

