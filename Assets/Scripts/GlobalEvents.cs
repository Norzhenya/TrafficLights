using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrafficLights
{
    public delegate void Inform();

    public delegate void LightInfo(int lightNumber);

    public class GlobalEvents : MonoBehaviour
    {
        public static GlobalEvents Instance;

        public event Inform VehicleCrush;
        public event LightInfo LightChange;

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }
            else
            {
                Debug.LogWarning("GlobalEvents already exist");
                Destroy(this);
            }
        }

        public void SendVehicleCrush()
        {
            VehicleCrush?.Invoke();
        }

        public void SendLightChange(int lightNumber)
        {
            LightChange?.Invoke(lightNumber);
        }
    }
}