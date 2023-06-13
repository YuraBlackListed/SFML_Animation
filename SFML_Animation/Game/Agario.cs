using SFML.Graphics;
using SFML_Animation.Engine.Interfaces;
using SFML.System;

namespace SFML_Animation.Game
{
    class Agario : IUpdatable
    {
        public RenderWindow scene;
        private GameObjects.Player player;

        public Agario(RenderWindow _scene)
        {
            scene = _scene;
            Start();
        }
        private void Start()
        {
            player = new GameObjects.Player(scene, new Vector2f(100f, 100f));
        }
        public void Update(float time)
        {
            player.Update(time);
        }
        
    }
}
