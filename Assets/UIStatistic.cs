using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatistic : MonoBehaviour
{

    public Text title;
    public Text value;

    public void UpdateStatistic(string titleString, string valueString)
    {
        title.text = titleString;
        value.text = valueString;
    }
}
