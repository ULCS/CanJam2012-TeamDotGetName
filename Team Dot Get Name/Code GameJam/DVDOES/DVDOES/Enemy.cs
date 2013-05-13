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
    enum EnemyType { BASE, ARCH, CHARGE, DRAGON };

    class Enemy
    {
        public Texture2D Texture
        {
            get { return texture; }
        }
        Texture2D texture;

        public Vector2 Origin
        {
            get { return origin; }
        }
        Vector2 origin;

        public string Name
        {
            get { return name; }
        }
        string name;

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

        public float Rotation
        {
            get { return rotation; }
        }
        float rotation;

        public int Health
        {
            get { return health; }
        }
        int health;

        public void setHealth(int i)
        {
            health += i;
        }

        public bool Angry
        {
            get { return angry; }
        }
        bool angry = false;

        EnemyType E_TYPE;
        public EnemyType getEType()
        {
            return E_TYPE;
        }


        private ContentManager content;
        private Texture2D healthBar;
        private Vector2 healthBarPosition;
        public Rectangle boundingBox;
        private Viewport viewport;
        private RoomHandler roomHandler;

        Bullet myBullet;
        public Bullet GetBullet()
        {
            return myBullet;
        }
        public void SetDmgBullet()
        {
            myBullet.SetDmg();
        }

        public Enemy(string Name, Vector2 Position, Vector2 Velocity, ContentManager Content, Viewport Viewport, RoomHandler RM, EnemyType e_type)
        {
            content = Content;
            name = Name;
            texture = content.Load<Texture2D>("Enemy\\enemyTexture");
            if (e_type == EnemyType.DRAGON)
                texture = content.Load<Texture2D>("Enemy\\dragonViking");
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            viewport = Viewport;
            position = Position;
            velocity = Velocity;
            healthBarPosition = new Vector2(position.X + (texture.Width / 2) - origin.X, (position.Y - 16) - origin.Y);
            health = 40;
            if (e_type == EnemyType.DRAGON)
                health = 200;

            healthBar = content.Load<Texture2D>("Player\\healthBar");

            roomHandler = RM;

            E_TYPE = e_type;

            if (E_TYPE == EnemyType.ARCH)
                myBullet = new Bullet(5, content.Load<Texture2D>("Weapons\\Arrow\\ArrowToTheKnee"), position, roomHandler);
            if (E_TYPE == EnemyType.DRAGON)
                myBullet = new Bullet(25, content.Load<Texture2D>("Weapons\\Arrow\\Flame"), position, roomHandler);
        }

        public void Update()
        {
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            healthBarPosition = new Vector2(position.X + (texture.Width / 2) - origin.X, (position.Y - 16) - origin.Y);

            if (!angry)
            {
                Vector2 M = new Vector2(position.X - roomHandler.player.Position.X, position.Y - roomHandler.player.Position.Y);

                rotation = (float)Math.Atan2(-M.Y, -M.X);
            }
            if (!angry && E_TYPE == EnemyType.BASE)
            {
                position.X += (float)(1 * Math.Cos(rotation));
                position.Y += (float)(1 * Math.Sin(rotation));
            }
            if (!angry && E_TYPE == EnemyType.ARCH || E_TYPE == EnemyType.DRAGON)
            {
                myBullet.Update((float)(4 * Math.Cos(rotation)), (float)(4 * Math.Sin(rotation)), position, rotation);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < health; i++)
            {
                spriteBatch.Draw(healthBar, new Vector2(healthBarPosition.X + (i * 1) - health / 2, healthBarPosition.Y), Color.White);
            }

            if (E_TYPE == EnemyType.ARCH || E_TYPE == EnemyType.DRAGON)
                myBullet.Draw(spriteBatch);

            spriteBatch.Draw(texture, position, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
        }
    }
}
