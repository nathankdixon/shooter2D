using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthManager : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;


    public void SetHealth(int currentHealth)
    {
        int h = currentHealth;
        print("UI -"+currentHealth);
        for (int i = 0; i < hearts.Length; i++)
        {
            if (h >= 2)
            {
                hearts[i].sprite = fullHeart;
                h -= 2;
            }
            else if (h >= 1)
            {
                hearts[i].sprite = halfHeart;
                h -= 2;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
