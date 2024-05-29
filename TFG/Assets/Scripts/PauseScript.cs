using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    [SerializeField] SoundFXScript soundFXScript;
    public void Update(){
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
    public void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
        if(soundFXScript!=null)
        soundFXScript.Pause();
    }
    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale=1;
        if(soundFXScript!=null)
        soundFXScript.Resume();
    }
    public void Restart()
    {
        Time.timeScale=1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
