using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskParamsUI : MonoBehaviour
{

    public string taskName;
    public string sceneName;
	public Toggle toggle;
	public Text toggleLabel;
	public InputField settingsFileName;

	public bool IsChecked
	{
		get { return toggle.isOn; }
	}

	void OnValidate()
	{
		toggleLabel.text = taskName;
	}

	public AssessmentParameters GetParameters()
	{
		string fileName = string.Format("{0}.json", settingsFileName.text);
		string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

		string jsonText = File.ReadAllText(filePath);

		UXF.Settings sessionSettings = new UXF.Settings(
				(Dictionary<string, object>) MiniJSON.Json.Deserialize(jsonText)
			);		

		return new AssessmentParameters()
		{
			sceneName = sceneName,
			taskName = taskName,
			settings = sessionSettings
		};
	}
}

public struct AssessmentParameters
{
    public string sceneName;
	public string taskName;
    public UXF.Settings settings;
}

