using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLevelUI : MonoBehaviour
{
    [SerializeField] CanvasGroup chooseLevel;
    [SerializeField] GameObject ChooseLevelSetting;
    // Start is called before the first frame update

    public void Setting()
    {
        chooseLevel.interactable = false;
        ChooseLevelSetting.SetActive(true);
    }
    public void Level1()
    {
        SceneManager.LoadScene(2);
    }
    public void Level2()
    {
        SceneManager.LoadScene(5);
    }
    public void Level3()
    {
        SceneManager.LoadScene(6);
    }
    public void Level4()
    {
        SceneManager.LoadScene(7);
    }
}
