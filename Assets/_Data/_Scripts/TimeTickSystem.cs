using System;
using UnityEngine;
using UnityEngine.Events;

public class TimeTickSystem : MonoBehaviour
{
        public static event UnityAction<int> OnTick;
        
        private const float TickTimerMax = 2f;
        private int _tick;
        private float _tickTimer;

        private void Awake()
        {
                _tick = 0;
        }

        private void Update()
        {
                _tickTimer += Time.deltaTime;
                if (_tickTimer >= TickTimerMax)
                {
                        _tickTimer -= TickTimerMax;
                        _tick++;
                        OnTick?.Invoke(_tick);
                }
        }
}