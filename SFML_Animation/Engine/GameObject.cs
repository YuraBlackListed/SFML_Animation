﻿using SFML.System;
using SFML.Graphics;
using SFML_Animation.Engine.Interfaces;
using System;

namespace SFML_Animation.Engine
{
    public class GameObject
    {
        private Vector2f position = new Vector2f(0, 0);
        public Vector2f Position
        {
            get { return position; }
            set { position = value; Mesh.Position = position; }
        }

        public Vector2f Velocity = new Vector2f(0, 0);

        public Shape Mesh;

        internal RenderWindow scene;

        public GameObject(RenderWindow window)
        {
            scene = window;

            if (this is IUpdatable updatable)
            {
                GameLoop.updatableObjects.Add(updatable);
            }

            if (this is IDrawable drawable)
            {
                GameLoop.drawableObjects.Add(drawable);
            }
        }

        internal void Destroy()
        {
            if (this is IUpdatable updatable)
            {
                GameLoop.updatableObjects.Remove(updatable);
            }

            if (this is IDrawable drawable)
            {
                GameLoop.drawableObjects.Remove(drawable);
            }
        }
        public Vector2f RandomPosition()
        {
            Random random = new();

            return new Vector2f(random.Next(0, (int)scene.Size.X), random.Next(0, (int)scene.Size.Y));
        }
    }

}
