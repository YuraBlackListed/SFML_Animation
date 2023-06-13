﻿using SFML.System;
using SFML.Graphics;
using SFML.Window;
using SFML_Animation.Engine.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace SFML_Animation.Engine
{
    class GameLoop
    {
        private int foodVolume;
        private int playerAmount;

        public bool running = false;

        private RenderWindow scene;

        private Clock clock = new Clock();

        public static List<IUpdatable> updatableObjects= new();
        public static List<IDrawable> drawableObjects = new();

        private Vector2u mapSize;

        private Game.Agario game;

        private InputHandler input;

        private GameLoop()
        {
            foodVolume = 200;
            playerAmount = 6;

            mapSize = new Vector2u(800, 800);
        }

        public void Run()
        {
            Start();
            while (running)
            {
                Render();
                CheckInput();
                Update();
            }
        }
        private void Start()
        {
            running = true;

            scene = new RenderWindow(new VideoMode(mapSize.X, mapSize.Y), "Game window");

            input = new InputHandler(scene);

            game = new Game.Agario(scene);

            scene.DispatchEvents();
        }
        private void Update()
        {
            Time deltaTime = clock.Restart();
            float seconds = deltaTime.AsSeconds();

            foreach (IUpdatable updatable in updatableObjects)
            {
                updatable.Update(seconds);
            }

            game.Update(seconds);

            scene.DispatchEvents();
        }
        private void Render()
        {
            scene.Clear(Color.Black);

            foreach (IDrawable drawable in drawableObjects)
            {
                drawable.Draw();
            }

            scene.Display();
        }
        private void CheckInput()
        {
            input.CheckInput();
        }

        public static GameLoop NewGameLoop()
        {
            GameLoop gameloop = new GameLoop();

            gameloop.LoadInformationFromFile();

            return gameloop;
        }
        public void LoadInformationFromFile()
        {
            string filePath = @"congifg.cfg";
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                StreamReader reader = new StreamReader(fs);

                string data;
                for (data = "1"; data != null; data = reader.ReadLine())
                {
                    string[] dataSplit = data.Split(':');

                    switch (dataSplit[0])
                    {
                        case "foodVolume":
                            if (int.TryParse(dataSplit[1], out int newFoodVolume))
                                foodVolume = newFoodVolume;
                            break;
                        case "mapSize":
                            if (uint.TryParse(dataSplit[1], out uint x))
                                mapSize.X = x;
                            if (uint.TryParse(dataSplit[2], out uint y))
                                mapSize.Y = y;
                            break;
                        case "playerAmount":
                            if (int.TryParse(dataSplit[1], out int newPlayerAmount))
                                playerAmount = newPlayerAmount;
                            break;
                    }
                }
            }
        }
    }
}
