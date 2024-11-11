using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseHP : BuildingHp
{
    public TMP_Text healthText;
    public GameObject loseScreen;
    public Image bloodyOverlay;
    public Image bloodyOverlay2;  // Second overlay
    public float maxOpacity = 0.8f;
    public float opacityIncreaseAmount = 0.3f; // Amount of opacity increase
    public float opacityHoldDuration = 0.5f; // Duration to hold the increased opacity
    public float fadeDownSpeed = 0.1f; // Speed at which opacity fades down

    private Coroutine fadeCoroutine1;
    private Coroutine fadeCoroutine2;

    public override void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealth(currentHealth);

        print("The Base took " + damage + " damage");

        if (bloodyOverlay != null)
        {
            if (fadeCoroutine1 != null) StopCoroutine(fadeCoroutine1);
            fadeCoroutine1 = StartCoroutine(FlashOverlayOpacity(bloodyOverlay));
        }

        if (bloodyOverlay2 != null)
        {
            if (fadeCoroutine2 != null) StopCoroutine(fadeCoroutine2);
            fadeCoroutine2 = StartCoroutine(FlashOverlayOpacity(bloodyOverlay2));
        }

        if (currentHealth <= 0)
        {
            DIE();
        }
    }

    IEnumerator FlashOverlayOpacity(Image overlay)
    {
        // Increase opacity
        Color overlayColor = overlay.color;
        overlayColor.a = Mathf.Clamp(overlayColor.a + opacityIncreaseAmount, 0, maxOpacity);
        overlay.color = overlayColor;

        // Hold increased opacity for set duration
        yield return new WaitForSeconds(opacityHoldDuration);

        // Fade down opacity
        while (overlay.color.a > 0)
        {
            overlayColor.a -= fadeDownSpeed * Time.deltaTime;
            overlay.color = overlayColor;
            yield return null;
        }
    }

    void UpdateHealth(float currentHealth)
    {
        healthText.text = currentHealth.ToString() + "+";
    }

    public override void DIE()
    {
        if (loseScreen != null)
        {
            loseScreen.SetActive(true);
        }
        else
        {
            print("loseScreen is null");
        }

        enemyList.DestroyAllEnemies();
        enemyManager.gamePaused = true;

        Destroy(gameObject);
    }
}
