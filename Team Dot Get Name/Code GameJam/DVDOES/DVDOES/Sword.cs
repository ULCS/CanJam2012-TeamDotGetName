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
    enum DamageType
    {
        Slashing = 0,
        Piercing = 1,
        Blunt
    }

    class Sword
    {
        public string Name
        {
            get { return name; }
        }
        string name;

        public int Damage
        {
            get { return damage; }
        }
        int damage;

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
        public void setPosition(Vector2 newPosition) { position = newPosition; }

        public DamageType DamageType
        {
            get { return damageType; }
        }
        DamageType damageType;

        public Rectangle BoundingBox
        {
            get { return boundingBox; }
        }
        Rectangle boundingBox;
        Rectangle otherBoundingBox;
        bool runOnce = true;
        public void SetBoundingBox(Rectangle newRect)
        {
            boundingBox = newRect;
        }

        private ContentManager content;
        private float rotation;
        private Vector2 origin;
        private Player player;
        private bool active; public bool Active { get { return active; } } public void setActive(bool newActive) { active = newActive; }

        public Sword(string Name, Vector2 Position, Texture2D Texture, DamageType DamageType, int Damage, ContentManager Content, Player Player)
        {
            content = Content;
            player = Player;
            name = Name;
            position = Position;
            damageType = DamageType;
            damage = Damage;
            texture = Texture;
            origin = Vector2.Zero;
            active = false;
        }

        public void Update()
        {
            position = player.Position;
            //boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            if (runOnce)
            {
                otherBoundingBox = boundingBox;
                runOnce = !runOnce;
            }
            rotation = player.rotation;

            if (GamePad.GetState(PlayerIndex.One).Triggers.Right != 0)
            {
                position.X += (float)(15 * Math.Cos(rotation));
                position.Y += (float)(15 * Math.Sin(rotation));
                active = true;
            }
            else
                active = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, otherBoundingBox, null, Color.White, rotation, origin, SpriteEffects.None, 0f);
        }
    }
}
