﻿using SFML.Window;
using System;

namespace SFML_Animation.Engine
{
    class KeyAction
    {
        public Keyboard.Key key;
        public Action action;
        public KeyAction(Keyboard.Key _key, Action invoke)
        {
            action = invoke;
            key = _key;
        }

        public void CheckInput()
        {
            HandleInput();
        }
        private void HandleInput()
        {
            if (Keyboard.IsKeyPressed(key))
            {
                action.Invoke();
            }
        }
    }
}
