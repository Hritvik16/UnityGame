using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxPlayerHealth = 100;
    public Slider healthSlider;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(.1f, 0f, 0f);
    public Image damageImage;

    private int currentPlayerHealth;
    private bool damaged;
    private Animator animator;
    private PlayerMovementScript playerMovementScript;
    // Start is called before the first frame update
    void Start()
    {
        currentPlayerHealth = maxPlayerHealth;
        damaged = false;
        animator = GetComponent<Animator>();
        playerMovementScript = GetComponent<PlayerMovementScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (damaged)
        {
            playerMovementScript.enabled = false;
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            playerMovementScript.enabled = true;
        }
        damaged = false;
    }

    public void takeDamage(int damage)
    {
        damaged = true;
        animator.SetTrigger("damaged");
        currentPlayerHealth -= damage;
        healthSlider.value = currentPlayerHealth;
        if(currentPlayerHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        animator.SetTrigger("dead");
        playerMovementScript.enabled = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        takeDamage(5);
    }
    //IEnumerator waitForDamageAnimation()
    //{
    //    yield return new WaitForSeconds(.933f);
    //}
}
