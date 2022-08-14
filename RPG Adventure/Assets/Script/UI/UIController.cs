using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject GameOverScreen;

    private void Start()
    {
        EventService.Instance.OnPlayerDied += EnableGameOverScreen;
    }
    private void OnDisable()
    {
        EventService.Instance.OnPlayerDied -= EnableGameOverScreen;
    }
    public void OnReplayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameOverScreen.SetActive(false);
    }
    public void OnQuitButtonClick()
    {
        Application.Quit();
    }

    public void EnableGameOverScreen()
    {
        GameOverScreen.SetActive(true);
    }
}
