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
    class Bullet
    {
        public int Damage
        {
            get { return damage; }
        }
        int damage;
        public void SetDmg()
        {
            damage = damage / 2;
        }

        public float Rotation
        {
            get { return rotation; }
        }
        float rotation;

        public Texture2D Texture
        {
            get { return texture; }
        }
        Texture2D texture;

        public Vector2 Position
        {
            get { return position; }
        }
        Vector2 position;

        public Vector2 Velocity
        {
            get { return velocity; }
        }
        Vector2 velocity;

        public Rectangle BoundingBox
        {
            get { return boundingBox; }
        }
        Rectangle boundingBox;

        bool reset = false;
        RoomHandler roomHandler;
        int originalDamage;

        public Bullet(int Damage, Texture2D Texture, Vector2 Position, RoomHandler RM)
        {
            damage = Damage;
            originalDamage = Damage;
            texture = Texture;
            position = Position;
            roomHandler = RM;
        }

        public void Update(float VX, float VY, Vector2 start, float rotate)
        {
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            if (reset == false)
            {
                position.X = start.X; position.Y = start.Y;
                rotation = rotate;

                velocity.X = VX;
                velocity.Y = VY;

                reset = true;

                damage = originalDamage;
            }

            position.X += velocity.X; position.Y += velocity.Y;

            if (position.X > 800 || position.X < 0)
                reset = false;
            if (position.Y > 600 || position.Y < 0)
                reset = false;

            if (boundingBox.Intersects(roomHandler.player.boundingBox))
            {
                position.X = start.X; position.Y = start.Y;
                roomHandler.player.SetHealth(-damage);

                reset = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0f);
        }
    }
}
