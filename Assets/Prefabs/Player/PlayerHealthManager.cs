using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour
{
    public int startingHealth;
    public float imortalityTime;
    public GameObject gameUI;
    public GameObject gameUI2;
    public GameObject dammageSound;
    public AudioSource mainMusic;
    public LevelEnd levelEnd;
    private AudioSource dammageSoundSource;

    private UIHealthManager gameUIManager;
    private UIHealthManager gameUIManager2;
    private int currentHealth;
    private bool imortal = false;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        sprite = GetComponent<SpriteRenderer>();
        gameUIManager = gameUI.GetComponent<UIHealthManager>();
        gameUIManager2 = gameUI2.GetComponent<UIHealthManager>();
        dammageSoundSource = dammageSound.GetComponent<AudioSource>();
    }

    public void HurtPlayer(int damageAmount)
    {
        if (!imortal)
        {
            imortal = true;
            dammageSoundSource.Play();
            currentHealth -= damageAmount;
            print(currentHealth);
            gameUIManager.SetHealth(currentHealth);
            gameUIManager2.SetHealth(currentHealth);
            StartCoroutine(Imortality());
        }

        if (currentHealth <= 0)
        {
            mainMusic.Stop();
            levelEnd.Death();
            Destroy(gameObject);
        }
    }

    IEnumerator Imortality()
    {
        sprite.color = new Color(0.8f, 0.3f, 0.3f, 1f);
        yield return new WaitForSeconds(imortalityTime);
        imortal = false;
        sprite.color = new Color(1f, 1f, 1f, 1f);
    }
}
