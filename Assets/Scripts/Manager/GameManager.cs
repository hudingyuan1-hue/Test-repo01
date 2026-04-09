using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int MaxStep;
    public int MinScore;
    [NonSerialized] public int Step = 0;
    [NonSerialized] public float Score = 0;
    public static GameManager Instance;

    [SerializeField] GameObject Lose;

    [SerializeField] GameObject Win;

    [SerializeField] int SceneIndex;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Step > MaxStep)
        {
            StartCoroutine(GameLose());
        }
        else if (Score >= MinScore)
        {
            StartCoroutine(GameWin());
        }
        if (TreeManager.Root.Children.Count == 0)
        {
            StartCoroutine(GameLose());
        }
    }

    IEnumerator GameLose()
    {
        TreeManager.Instance.Clean(TreeManager.Root);
        Lose.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneIndex);
    }

    IEnumerator GameWin()
    {
        TreeManager.Instance.Clean(TreeManager.Root);
        Win.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);
    }
}