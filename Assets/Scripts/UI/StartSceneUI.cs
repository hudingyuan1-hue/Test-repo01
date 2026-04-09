using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneUI : MonoBehaviour
{
    [SerializeField] int SceneIndex = 5;
    public void StartScene()
    {
        SceneManager.LoadScene(SceneIndex);
    }
}
