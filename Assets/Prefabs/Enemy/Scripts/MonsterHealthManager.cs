using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealthManager : MonoBehaviour
{

    public int health;
    public GameObject bloodSplater;
    private int currentHealth;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start() 
    {
        currentHealth = health;
        sprite = GetComponent<SpriteRenderer>();
    }

    public void HurtMonster(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Instantiate(bloodSplater, GetComponent<Transform>().position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(damageFlash());
        }
    }


    IEnumerator damageFlash()
    {
        sprite.color = new Color(0.8f, 0.3f, 0.3f, 1f);
        yield return new WaitForSeconds(0.5f);
        sprite.color = new Color(1f, 1f, 1f, 1f);
    }
}
