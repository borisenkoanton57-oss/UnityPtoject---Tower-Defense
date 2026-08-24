using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DamageEffect : MonoBehaviour
{
   
    [Header("Damage UI")]
    [SerializeField] private Image damageOverlay;

    [Header("Damage Sound")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip damageSound;

    [Header("Effect Settings")]
    [SerializeField] private float effectDuration = 0.4f;
    [SerializeField] private float maxAlpha = 0.65f;

    private Coroutine damageCoroutine;

    public void PlayDamageEffect()
    {
        if (damageCoroutine != null)
            StopCoroutine(damageCoroutine);

        damageCoroutine = StartCoroutine(DamageEffectCoroutine());

        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }
    }

    private IEnumerator DamageEffectCoroutine()
    {
        Color color = damageOverlay.color;

        color.a = maxAlpha;
        damageOverlay.color = color;

        yield return new WaitForSeconds(effectDuration);

        float time = 0f;

        while (time < effectDuration)
        {
            time += Time.deltaTime;

            color.a = Mathf.Lerp(maxAlpha, 0f, time / effectDuration);
            damageOverlay.color = color;

            yield return null;
        }

        color.a = 0f;
        damageOverlay.color = color;

        damageCoroutine = null;
    }
}
