using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TimerUI : MonoBehaviour
    {
        private TMP_Text timerText;
        private float timerTime = 0f;
        private string timerString = "Timer: ";
        private void Start()
        {
            timerText = GetComponent<TMP_Text>();
        }

        private void Update()
        {
            timerTime += Time.deltaTime;
            timerText.text = timerString + timerTime.ToString("F2");

        }
    }
}