using UnityEngine;

namespace Utility
{
    public static class EmissionChanger
    {
        public static void ChangeEmissionColor(Renderer renderer, float newIntensity)
        {
            float emissionValueBase = 1 * (Mathf.Pow(Mathf.Exp(1), newIntensity * Mathf.Log(2f)) / 255);

            Color color = renderer.material.color;
            float emissionValue = emissionValueBase / color.maxColorComponent; //RGBの値の中の最大値

            renderer.material.EnableKeyword("_EMISSION");
            renderer.material.SetColor("_EmissionColor",
                new Color(color.r * emissionValue, color.g * emissionValue, color.b * emissionValue));
        }

    }
}