using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TMP_Text StepText;
    [SerializeField] TMP_Text ScoreText;

    int step => GameManager.Instance.Step;
    float? score => GameManager.Instance?.Score;

    void Update()
    {
        StepText.SetText($"Step: {step}");
        ScoreText.SetText($"Score: {score?.ToString("G4")}");
    }
}
