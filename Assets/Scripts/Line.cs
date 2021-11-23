using System;
using System.Collections.Generic;
using UnityEngine;

namespace TrafficLights
{
    public class Line : MonoBehaviour
    {
        public event Inform LineFinished;

        [SerializeField] private int lineNumber;
        [SerializeField] private SpriteRenderer finishDot;
        [SerializeField] private List<Vehicle> vehicles;

        private bool isActive;

        private void OnEnable()
        {
            GlobalEvents.Instance.LightChange += OnLightChange;

            foreach (Vehicle vehicle in vehicles)
            {
                vehicle.VehicleFinish += OnVehicleFinish;
            }
        }

        private void OnDisable()
        {
            GlobalEvents.Instance.LightChange -= OnLightChange;

            foreach (Vehicle vehicle in vehicles)
            {
                vehicle.VehicleFinish -= OnVehicleFinish;
            }
        }

        public void SetColor(Color color)
        {
            finishDot.color = color;
            foreach (Vehicle vehicle in vehicles)
            {
                vehicle.GetComponent<SpriteRenderer>().color = color;
            }
        }

        public void SetNumber(int number)
        {
            lineNumber = number;
        }

        private void OnLightChange(int lightNumber)
        {
            Debug.Log(name);
            bool match = lightNumber == lineNumber;
            if (match != isActive) SetLineActive(match);
        }

        private void SetLineActive(bool active)
        {
            isActive = active;
            foreach (Vehicle vehicle in vehicles)
            {
                vehicle.IsActive = active;
            }
        }

        private void OnVehicleFinish(Vehicle vehicle)
        {
            vehicles.Remove(vehicle);
            vehicle.VehicleFinish -= OnVehicleFinish;
            Destroy(vehicle.gameObject);
            if (vehicles.Count == 0)
            {
                LineFinished?.Invoke();
            }
        }
    }
}