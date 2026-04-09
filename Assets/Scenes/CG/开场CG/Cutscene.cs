using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CutsceneProcess());
    }
    IEnumerator CutsceneProcess()
    {
        // Cutscene logic here
        yield return new WaitForSeconds(5f); // Example: wait for 2 seconds
        SceneManager.LoadScene(1); // Load the main scene after the cutscene
    }
}
