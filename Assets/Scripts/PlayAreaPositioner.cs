using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayAreaPositioner : MonoBehaviour
{
    public float increment = 0.02f;
    public KeyCode increaseKey = KeyCode.UpArrow;
    public KeyCode decreaseKey = KeyCode.DownArrow;
    public StringEvent onValueChange;
    string prefKey = "PlayAreaHeight";

    // Start is called before the first frame update
    void Start()
    {
        UpdateHeight(PlayerPrefs.GetFloat(prefKey, 1.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(increaseKey)) Increase();
        if (Input.GetKeyDown(decreaseKey)) Decrease();
    }

    public void Increase()
    {
        float newValue = PlayerPrefs.GetFloat(prefKey, 1.0f) + increment;
        UpdateHeight(newValue);
    }

    public void Decrease()
    {
        float newValue = PlayerPrefs.GetFloat(prefKey, 1.0f) - increment;
        UpdateHeight(newValue);
    }

    void UpdateHeight(float newHeight)
    {
        onValueChange.Invoke(string.Format("{0:0.00}", newHeight));

        Vector3 pos = transform.position;
        pos.y = newHeight;
        transform.position = pos;

        PlayerPrefs.SetFloat(prefKey, newHeight);
    }
}

[System.Serializable]
public class FloatEvent : UnityEvent<float> { }

[System.Serializable]
public class StringEvent : UnityEvent<string> { }