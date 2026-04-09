using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseLevelSettingUI : MonoBehaviour
{
    [SerializeField] CanvasGroup chooseLevel;
    public void Exit()
    {
        chooseLevel.interactable = true;
        gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit();
        }
    }
}
