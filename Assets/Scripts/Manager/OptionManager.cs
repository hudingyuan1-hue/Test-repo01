using System.Collections;
using System.Collections.Generic;
//using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    [SerializeField] GameObject optionAudio;
    [SerializeField] GameObject optionGame;
    [SerializeField] GameObject optionGraphics;
    [SerializeField] CanvasGroup MainMenu;
    GameObject currentOption = null;
    // Start is called before the first frame update
    void Start()
    {
        ChangeToGame();
    }

    public void ChangeToAudio()
    {
        if (currentOption != optionAudio)
        {
            currentOption?.SetActive(false);
            currentOption = optionAudio;
            optionAudio.SetActive(true);
        }
    }

    public void ChangeToGame()
    {
        if (currentOption != optionGame)
        {
            currentOption?.SetActive(false);
            currentOption = optionGame;
            optionGame.SetActive(true);
        }
    }

    public void ChangeToGraphics()
    {
        if (currentOption != optionGraphics)
        {
            currentOption?.SetActive(false);
            currentOption = optionGraphics;
            optionGraphics.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            MainMenu.interactable = true;
        }
    }
}
