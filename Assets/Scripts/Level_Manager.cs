using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level_Manager : MonoBehaviour
{
    //Menu
    public GameObject Options;
    public GameObject mainMenu;
    public GameObject soundOn;
    public GameObject soundOff;
    public GameObject locomotionSwipe;
    public GameObject locomotionLead;
    public fatguy ft;

    public void PlayButton()
    {
        ft.start = true;
        mainMenu.SetActive(false);
        ft.setState = 1;
    }
    public void SoundSwitcher(bool soundActive)
    {
        if (soundActive)
        {
            soundOff.SetActive(false);
            soundOn.SetActive(true);
            AudioListener.volume = 1;
        }
        else
        {
            soundOff.SetActive(true);
            soundOn.SetActive(false);
            AudioListener.volume = 0;
        }
    }
    public void LocomotionSwitcher(bool b)
    {
        if (b)
        {
            locomotionLead.SetActive(false);
            locomotionSwipe.SetActive(true);
            ft.movement_Mode = true; 
        }
        else
        {
            locomotionLead.SetActive(true);
            locomotionSwipe.SetActive(false);
            ft.movement_Mode = false;
        }
    }

    public void OptionsSwitcher(bool b)
    {
        if(b)
        {
            Options.SetActive(false);
            mainMenu.SetActive(true);
        }
        else
        {
            Options.SetActive(true);
            mainMenu.SetActive(false);
        }
    }
    public void LoadLevel(int x)
    {
        SceneManager.LoadScene(x);
    }
}
