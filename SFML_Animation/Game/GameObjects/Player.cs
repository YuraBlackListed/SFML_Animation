using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML_Animation.Engine;
using SFML_Animation.Engine.Interfaces;
using SFML.Window;

namespace SFML_Animation.Game.GameObjects
{
    class Player : GameObject, IUpdatable, IDrawable
    {
        public float size = 30;
        private float time;

        private int curretTextureIndex;
        private int playerSpeed = 400;

        Dictionary<string, Texture[]> textures = new Dictionary<string, Texture[]>
        {
            { "Idle", new Texture[2] },
            { "Up", new Texture[2] },
            { "Down", new Texture[2] },
            { "Left", new Texture[2] },
            { "Right", new Texture[2] }
        };

        public Player(RenderWindow scene, Vector2f position) : base(scene)
        {
            Initialize(position);
        }
        private void Initialize(Vector2f position)
        {
            GetTextures("Idle");
            GetTextures("Up");
            GetTextures("Down");
            GetTextures("Left");
            GetTextures("Right");

            curretTextureIndex = 0;
            Mesh = new Sprite(textures["Idle"][0]);

            Mesh.Position = Position;
            Mesh.Origin = new Vector2f(Mesh.Texture.Size.X / 2, Mesh.Texture.Size.Y / 2);

            Position = position;
            Mesh.Scale = new Vector2f(10f, 10f);

            InputHandler.CreateAction(Keyboard.Key.W, MoveUp);
            InputHandler.CreateAction(Keyboard.Key.S, MoveDown);
            InputHandler.CreateAction(Keyboard.Key.A, MoveLeft);
            InputHandler.CreateAction(Keyboard.Key.D, MoveRight);
        }
        private void GetTextures(string key)
        {
            for (int i = 0; i < textures[key].Length; i++)
            {
                textures[key][i] = new Texture($"Sprites/{key}/" + i + ".png");
            }
        }
        private void SwapTexture(string direction)
        {
            if (curretTextureIndex == 0)
            {
                curretTextureIndex = 1;
            }
            else
            {
                curretTextureIndex = 0;
            }
            switch (direction)
            {
                case "Idle":
                    Mesh = new Sprite(textures["Idle"][curretTextureIndex]);
                    break;
                case "Up":
                    Mesh = new Sprite(textures["Up"][curretTextureIndex]);
                    break;
                case "Down":
                    Mesh = new Sprite(textures["Down"][curretTextureIndex]);
                    break;
                case "Left":
                    Mesh = new Sprite(textures["Left"][curretTextureIndex]);
                    break;
                case "Right":
                    Mesh = new Sprite(textures["Right"][curretTextureIndex]);
                    break;
            }
            Mesh.Scale = new Vector2f(10f, 10f);
        }
        public void Update(float _time)
        {
            time = _time;
            Position += Velocity * playerSpeed * time;
            Velocity = new Vector2f(0f, 0f);
            //SwapTexture("Idle");
        }
        private void MoveUp()
        {
            SwapTexture("Up");
            Velocity = new Vector2f(Velocity.X, -1);
        }
        private void MoveDown()
        {
            SwapTexture("Down");
            Velocity = new Vector2f(Velocity.X, 1);
        }
        private void MoveLeft()
        {
            SwapTexture("Left");
            Velocity = new Vector2f(-1, Velocity.Y);
        }
        private void MoveRight()
        {
            SwapTexture("Right");
            Velocity = new Vector2f(1, Velocity.Y);
        }
        //copypaaaasssttttaaaaaaaa
        public new void Destroy()
        {
            base.Destroy();
        }
        public void Draw()
        {
            scene.Draw(Mesh);
        }
        
    }
}
