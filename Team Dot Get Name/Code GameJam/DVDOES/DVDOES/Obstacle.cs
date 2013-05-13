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
    class Obstacle
    {
        Rectangle collisionBox; public Rectangle GetRect() { return collisionBox; }
        Texture2D texture; public Texture2D GetTexture() { return texture; }

        public Obstacle(int x, int y, Texture2D TT)
        {
            collisionBox = new Rectangle(x, y, TT.Width, TT.Height);

            texture = TT;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, collisionBox, Color.White);
        }
    }
}
