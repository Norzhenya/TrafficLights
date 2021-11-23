using System;
using UnityEngine;
using UnityEngine.UI;

namespace TrafficLights
{
    public class StopLight : MonoBehaviour
    {
        public event Inform LightClicked;
        
        [SerializeField] private Image lightRenderer;
        [SerializeField] private float alphaForDisabledLight = 0.4f;
        [SerializeField] private float alphaForEnabledLight = 1f;

        private void Start()
        {
            lightRenderer.color = ChangeAlpha(alphaForDisabledLight, lightRenderer.color);
        }

        public void Click()
        {
            LightClicked?.Invoke();
        }

        public void SetColor(Color color)
        {
            lightRenderer.color = color;
        }

        public void EnableLight()
        {
            lightRenderer.color = ChangeAlpha(alphaForEnabledLight, lightRenderer.color);
        }

        public void DisableLight()
        {
            lightRenderer.color = ChangeAlpha(alphaForDisabledLight, lightRenderer.color);
        }

        private Color ChangeAlpha(float amount, Color color)
        {
            color.a = amount;
            return color;
        }
    }
}