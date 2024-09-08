using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script that contains all the Pause submenu in every scene.
/// </summary>
public class PauseScript : MonoBehaviour
{
    /// <summary>
    /// The pause sub-menu.
    /// </summary>
    [SerializeField] GameObject PauseMenu;
    /// <summary>
    /// The SoundFXScript singleton.
    /// </summary>
    [SerializeField] SoundFXScript soundFXScript;
    /// <summary>
    /// Only check if Escape is pressed. If so show/hide the pause submenu.
    /// </summary>
    private void Update(){
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(PauseMenu.activeSelf==true){
                Resume();
            }
            else{
                Pause();
            }
        }
    }

    /// <summary>
    /// Function that load the MainMenu.
    /// </summary>
    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    /// <summary>
    /// Function that pauses the game.
    /// </summary>
    public void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
        if(soundFXScript!=null)
        soundFXScript.Pause();
    }
    /// <summary>
    /// Function that continue the game.
    /// </summary>
    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale=1;
        if(soundFXScript!=null)
        soundFXScript.Resume();
    }
    /// <summary>
    /// Function to reload the actual scene.
    /// </summary>
    public void Restart()
    {
        Time.timeScale=1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
