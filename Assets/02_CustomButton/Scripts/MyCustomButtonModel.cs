using System;
using System.Collections;
using UnityEngine;

namespace _02_CustomButton.Scripts
{
    public class MyCustomButtonModel
    {
        public bool Pointed { get; private set; }
        public bool Pressed { get; private set; }

        private readonly float _longPressTime;
        private float _pressedTime;
        private bool _longPressed;

        public event Action Tap;
        public event Action LongPress;

        public void Point()
        {
            Pointed = true;
        }

        public void Unpoint()
        {
            Pointed = false;
        }

        public void Press()
        {
            Pressed = true;
            _pressedTime = Time.time;
        }

        public void Release()
        {
            _longPressed = false;
            Pressed = false;
            if (!_longPressed) Tap?.Invoke();
        }

        public void CheckLongPress()
        {
            if (!_longPressed && LongPressed())
            {
                LongPress?.Invoke();
                _longPressed = true;
            }
        }

        private bool LongPressed() => Pressed && Time.time - _pressedTime > _longPressTime;
    }
}
