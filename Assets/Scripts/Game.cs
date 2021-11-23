using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace TrafficLights
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private LightsRegulator lightsRegulator;
        [SerializeField] private LinesRegulator linesRegulator;
        [SerializeField] private GameObject startButton;
        [SerializeField] private GameObject addButton;
        [SerializeField] private GameObject winBlock;
        [SerializeField] private GameObject loseBlock;

        private void OnEnable()
        {
            linesRegulator.AllLineFinished += GameWin;
            GlobalEvents.Instance.VehicleCrush += GameLose;
        }
        
        private void OnDisable()
        {
            linesRegulator.AllLineFinished -= GameWin;
            GlobalEvents.Instance.VehicleCrush -= GameLose;
        }

        public void AddNewColor()
        {
            Color newColor = Random.ColorHSV();
            lightsRegulator.AddNewLight(newColor);
            linesRegulator.AddNewLine(newColor);
        }

        public void StartGame()
        {
            addButton.SetActive(false);
            startButton.SetActive(false);
            lightsRegulator.StartLight();
        }

        private void GameWin()
        {
            winBlock.SetActive(true);
        }

        private void GameLose()
        {
            loseBlock.SetActive(true);
        }
    }
}