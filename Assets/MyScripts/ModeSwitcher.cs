using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSwitcher : MonoBehaviour
{
    [SerializeField] private Mode mode;
    [SerializeField] private CursorController cursor;

    [SerializeField] private Transform desktop;
    [SerializeField] private Transform vr;

    [SerializeField] private GameObject desktopParent;
    [SerializeField] private GameObject vrParent;
    [SerializeField] private GameObject avatar;

    private void OnValidate()
    {
        switch(mode)
        {
            case Mode.Desktop:
                cursor.target = desktop;
                desktopParent.SetActive(true);
                vrParent.SetActive(false);
                avatar.SetActive(false);
                break;

            case Mode.VR:
                cursor.target = vr;
                desktopParent.SetActive(false);
                vrParent.SetActive(true);
                avatar.SetActive(true);
                break;
        }
    }
}

[System.Serializable]
public enum Mode 
{
  Desktop, VR  
}

