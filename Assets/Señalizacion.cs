using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Señalizacion : MonoBehaviour
{
    public float transparencyDuration = 1.0f;
    public Renderer transparentRenderer;
    private Material transparentMaterial;
    private Color originalColor;

    private void Start()
    {
        transparentMaterial = transparentRenderer.material;
        originalColor = transparentMaterial.GetColor("_TintColor"); // Utiliza "_TintColor" para obtener el color original

        StartCoroutine(ChangeTransparency());
    }

    private IEnumerator ChangeTransparency()
    {
        while (true)
        {
            // Cambiar la transparencia del objeto duplicado
            Color transparentColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
            transparentMaterial.SetColor("_TintColor", transparentColor); // Utiliza "_TintColor" para cambiar la propiedad de color

            yield return new WaitForSeconds(transparencyDuration);

            // Restaurar la transparencia al valor original
            transparentMaterial.SetColor("_TintColor", originalColor); // Utiliza "_TintColor" para restaurar la propiedad de color

            yield return new WaitForSeconds(transparencyDuration);
        }
    }
}