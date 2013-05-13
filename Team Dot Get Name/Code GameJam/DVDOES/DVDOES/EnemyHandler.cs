using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DVDOES
{
    class EnemyHandler
    {
        public List<Enemy> Enemies
        {
            get { return enemies; }
        }
        private List<Enemy> enemies;

        SpriteBatch spriteBatch;

        public EnemyHandler(int enemyNumber, Vector2[] enemyPositions, Viewport VP, ContentManager CM, RoomHandler RM, EnemyType[] e_type)
        {
            enemies = new List<Enemy>();

            for (int i = 0; i < enemyNumber; i++)
            {
                enemies.Add(new Enemy("Viking", enemyPositions[i], new Vector2(1, 1), CM, VP, RM, e_type[i]));
            }
        }

        public void Update()
        {
            for (int i = 0; i < enemies.Count; i++)
                enemies[i].Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < enemies.Count; i++)
                enemies[i].Draw(spriteBatch);
        }
    }
}
