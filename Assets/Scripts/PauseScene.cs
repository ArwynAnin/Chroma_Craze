using UnityEngine;

public class PauseScene : main
{
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private AudioSource click;

    private bool gameIsPaused;

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        click.Play();
        if (gameIsPaused) Resume();
        else Pause();
    }

    public void Resume()
    {
        gameIsPaused = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void Pause()
    {
        gameIsPaused = true;
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
