using System.Collections;
using UnityEngine;

public class FadeInLogo : MonoBehaviour
{
    // La duraci�n del fade-in en segundos
    public float fadeInDuration = 2.0f;

    // El Sprite Renderer del logo
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Obt�n el componente Sprite Renderer del objeto
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Aseg�rate de que el logo sea transparente al inicio
        Color color = spriteRenderer.color;
        color.a = 0;
        spriteRenderer.color = color;

        // Inicia la corrutina para el fade-in
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        // El tiempo actual
        float elapsedTime = 0f;

        // Mientras el tiempo transcurrido sea menor que la duraci�n del fade-in
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;

            // Calcula el nuevo valor de alpha
            float newAlpha = Mathf.Clamp01(elapsedTime / fadeInDuration);

            // Aplica el nuevo valor de alpha al color del sprite
            Color color = spriteRenderer.color;
            color.a = newAlpha;
            spriteRenderer.color = color;

            // Espera hasta el siguiente frame
            yield return null;
        }
    }
}