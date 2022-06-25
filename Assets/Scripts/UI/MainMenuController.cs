using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject UI;
    public Dropdown dropDown;
    public Text score;

    public GameObject settings;

    public Slider sensSlider;
    public Slider soundVolume;

    private bool settingsPressed = false;
    private int selectedTime = 60;

    private void Start()
    {
        // setup sensitivity
        if (GlobalFields.sensitivity == 0)
        {
            sensSlider.value = 5;
            GlobalFields.sensitivity = 5;
        } else
        {
            sensSlider.value = GlobalFields.sensitivity;
        }

        // setup sound volume
        if (GlobalFields.soundVolume == 0)
        {
            soundVolume.value = 1;
            GlobalFields.soundVolume = 1;
        } else
        {
            soundVolume.value = GlobalFields.soundVolume;
        }

        Cursor.lockState = CursorLockMode.None;
        if (GlobalFields.points != 0)
        {
            score.text += GlobalFields.points;
            GlobalFields.points = 0;
        } else
        {
            score.text = "";
        }
    }

    public void getTime()
    {
        selectedTime = (dropDown.value + 1) * 60;
        print(selectedTime);
    }

    public void play()
    {
        UI.SetActive(false);
        Camera.main.transform.DOMoveY(2, 1).SetEase(Ease.InCubic).OnComplete(() => {
            GlobalFields.time_remaining = selectedTime;
            SceneManager.LoadScene(1);
        });
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void setSensitivity()
    {
        GlobalFields.sensitivity = (int) sensSlider.value;
    }

    public void setSoundVolume()
    {
        GlobalFields.soundVolume = soundVolume.value;
    }

    public void OpenSettings()
    {
        if (settingsPressed)
        {
            settings.transform.DOMoveZ(settings.transform.position.z + 2f, 0.5f).OnComplete(() =>
             {
                 settingsPressed = false;
             });
        } else
        {
            settings.transform.DOMoveZ(settings.transform.position.z - 2f, 0.5f).OnComplete(() =>
            {
                settingsPressed = true;
            });
        }
    }
}
