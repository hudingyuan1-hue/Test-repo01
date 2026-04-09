using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsButton : MonoBehaviour
{
    [SerializeField] GameObject optionPanel;
    [SerializeField] CanvasGroup MainMenu;
    public void OnClick()
    {
        optionPanel.SetActive(true);
        MainMenu.interactable = false;
    }
}
