using System;
using System.Collections.Generic;
using UnityEngine;

namespace TrafficLights
{
    public class LinesRegulator : MonoBehaviour
    {
        public event Inform AllLineFinished;

        [SerializeField] private GameObject linePrefab;
        [SerializeField] private List<Line> lines;
        [SerializeField] private float angleForNewLine = 10f;

        private int finishedLines;

        private void OnEnable()
        {
            foreach (Line line in lines) line.LineFinished += OnFinishLine;
        }

        private void OnDisable()
        {
            foreach (Line line in lines) line.LineFinished -= OnFinishLine;
        }

        public void AddNewLine(Color color)
        {
            float angle = lines[lines.Count - 1].transform.rotation.eulerAngles.z + angleForNewLine;
            GameObject newLineObject = 
                Instantiate(linePrefab, transform.position, Quaternion.Euler(0f, 0f, angle), transform);
            Line newLine = newLineObject.GetComponent<Line>();
            newLine.SetNumber(lines.Count);
            newLine.SetColor(color);
            newLine.LineFinished += OnFinishLine;
            lines.Add(newLine);
        }

        private void OnFinishLine()
        {
            finishedLines++;
            if (finishedLines == lines.Count) AllLineFinished?.Invoke();
        }
    }
}