using System;
using System.Collections.Generic;
using UnityEngine;

namespace TrafficLights
{
    public class LightsRegulator : MonoBehaviour
    {
        [SerializeField] private GameObject lightPrefab;
        [SerializeField] private List<StopLight> stopLights;
        [SerializeField] private float distanceBetweenLights = 105f;
        private int activeLightNumber;
        private bool isLightStarted;

        private void OnEnable()
        {
            foreach (StopLight stopLight in stopLights)
            {
                stopLight.LightClicked += OnLightClick;
            }
        }
        
        private void OnDisable()
        {
            foreach (StopLight stopLight in stopLights)
            {
                stopLight.LightClicked -= OnLightClick;
            }
        }

        public void AddNewLight(Color color)
        {
            Vector3 position =
                stopLights[stopLights.Count - 1].transform.position + new Vector3(distanceBetweenLights, 0f, 0f);
            GameObject newLightObject = Instantiate(lightPrefab, position, Quaternion.identity, transform);
            StopLight newLight = newLightObject.GetComponent<StopLight>();
            stopLights.Add(newLight);
            newLight.SetColor(color);
            newLight.LightClicked += OnLightClick;
        }

        public void StartLight()
        {
            isLightStarted = true;
            stopLights[activeLightNumber].EnableLight();
            GlobalEvents.Instance.SendLightChange(activeLightNumber);
        }

        private void SwitchLight()
        {
            stopLights[activeLightNumber].DisableLight();
            if (activeLightNumber < stopLights.Count - 1) activeLightNumber++;
            else activeLightNumber = 0;
            stopLights[activeLightNumber].EnableLight();
            GlobalEvents.Instance.SendLightChange(activeLightNumber);
        }

        private void OnLightClick()
        {
            if (!isLightStarted) return;
            SwitchLight();
        }
    }
}