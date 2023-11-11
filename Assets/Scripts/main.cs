using UnityEngine;
using UnityEngine.SceneManagement;

public class main : MonoBehaviour
{
    public void Playgame()
    {
        SceneManager.LoadScene(1);
    }

    public void endless()
    {
        SceneManager.LoadScene(2);
    }

    public void retry()
    {
        int index = PlayerPrefs.GetInt("index");
        SceneManager.LoadScene(index);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
    public void menu()
    {
        SceneManager.LoadScene(0);
    }
}
