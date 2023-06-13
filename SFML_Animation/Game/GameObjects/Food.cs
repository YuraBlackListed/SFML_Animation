﻿using SFML.Graphics;
using SFML.System;
using System;
using SFML_Animation.Engine;
using SFML_Animation.Engine.Interfaces;

namespace SFML_Animation.Game.GameObjects
{
    class Food : GameObject, IDrawable
    {
		private CircleShape shape;

		private float radius;

		public Food(RenderWindow scene) : base(scene)
		{
			Initialize(RandomPosition());
		}
		public Food(RenderWindow scene, Vector2f position) : base(scene)
		{
			Initialize(position);
		}

		private void Initialize(Vector2f position)
		{
			Random random = new Random();
			Color randomColor = new Color((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255));
			radius = random.Next(3, 5);

			shape = new CircleShape(radius);
			shape.Position = position;
			shape.Origin = new Vector2f(radius, radius);
			shape.FillColor = randomColor;

			Mesh = shape;

			Position = position;
		}
		public new void Destroy()
        {
			base.Destroy();
			Game.Agario.foodList.Remove(this);

		}
		public void Draw()
		{
			scene.Draw(Mesh);
		}

	}
}
