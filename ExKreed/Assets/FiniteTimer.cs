using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bdeshi.utility
{
    [System.Serializable]
    public struct FiniteTimer
    {
        public float timer;
        public float maxValue;

        public void init(float maxval, float startVal = 0)
        {
            timer = startVal;
            maxValue = maxval;
        }

        public FiniteTimer(float timerStart = 0, float maxVal = 3)
        {
            timer = timerStart;
            maxValue = maxVal;
        }

        public void updateTimer(float delta)
        {
            timer += delta;
        }

        public void safeUpdateTimer(float delta)
        {
            if(timer<maxValue)
                timer += delta;
            if (timer > maxValue)
                timer = maxValue;
        }

        public void reset()
        {
            timer = 0;
        }
        public void reset(float newMax)
        {
            maxValue = newMax;
            reset();
        }
        public void complete()
        {
            timer = maxValue;
        }

        public bool isComplete => timer >= maxValue;

        public bool exceedsRatio(float ratioToExceed)
        {
            return Ratio >= ratioToExceed;
        }

        public float Ratio => Mathf.Clamp01(timer / maxValue);

        public float ReverseRatio => 1 - Ratio;
    }
}
