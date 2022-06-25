using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text timer;
    public Text score;
    public Text ammo;
    [Header("Specifics")]
    [SerializeField] Color noTimeColor;
    public PlayerAttack controller;

    private bool pipik = false;

    private void Start()
    {
        AudioListener.volume = GlobalFields.soundVolume;
    }

    private void Update()
    {
        if (GlobalFields.time_remaining <= 0)
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void FixedUpdate()
    {
        GlobalFields.time_remaining -= Time.deltaTime;
        timer.text = getTime();
        score.text = "Score : " + GlobalFields.points;
        ammo.text = controller.acc_amount + " / " + controller.weaponStats.amount + " |||";

        if (GlobalFields.time_remaining <= 30 && !pipik)
        {
            pipik = true;
            ShakeScale(true);
            ChangeColor(true);
        }
    }

    private void ShakeScale(bool increase)
    {
        if (increase)
        timer.transform.DOScale(2,0.5f).OnComplete(() =>
        {
            ShakeScale(false);
        });
        else
            timer.transform.DOScale(1f, 0.5f).OnComplete(() =>
            {
                ShakeScale(true);
            });
    }
    private void ChangeColor(bool inRed)
    {
        if (inRed)
        {
            timer.DOColor(noTimeColor, 0.5f).OnComplete(() =>
             {
                 ChangeColor(false);
             });
        } else
        {
            timer.DOColor(Color.white, 0.5f).OnComplete(() =>
            {
                ChangeColor(true);
            });
        }
    }

    private string getTime()
    {
        return (int) GlobalFields.time_remaining / 60 + ":" + Math.Truncate(GlobalFields.time_remaining) % 60;
    }


}
