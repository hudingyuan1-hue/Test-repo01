using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSkipUI : MonoBehaviour
{
    public void SkipLevel()
    {
        SceneManager.LoadScene(4);
    }
}
