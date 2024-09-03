using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{

    public GameObject player;
    public GameObject levelEndScrene;
    public GameObject hearts;
    public TextMeshProUGUI timer;
    public SceneLoader sceneManager;
    public AudioSource levelWinSound;

    private BoxCollider2D playerCollider;
    private float startTime;
    private AudioSource deathSound;

    void Start()
    {
        playerCollider = player.GetComponent<BoxCollider2D>();
        deathSound = GetComponent<AudioSource>();
        startTime = Time.time;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col != playerCollider)
        {
            return;
        }

        levelWinSound.Play();
        levelEndScrene.SetActive(true);
        player.SetActive(false);
        hearts.SetActive(false);
        setTime((int) (Time.time - startTime));
    }

    private void setTime(int time)
    {
        int sec = time % 60;
        int min = (time - sec) / 60;
        timer.text = min + ":" + sec;
        if (sec<10)
        {
            timer.text = min + ":0" + sec;
        }
    }

    public void Death()
    {
        deathSound.Play();

        levelEndScrene.SetActive(true);
        player.SetActive(false);
        hearts.SetActive(false);
        setTime((int)(Time.time - startTime));
    }
}
