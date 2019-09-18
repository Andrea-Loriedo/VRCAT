using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TermsAndConditions : MonoBehaviour {

	public Toggle toggle;

	public bool IsChecked
	{
		get { return toggle.isOn; }
	}
}
