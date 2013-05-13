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
    class Player
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
        Vector2 position; public void setPosition(Vector2 newPosition) { position = newPosition; }

        public Vector2 Velocity
        {
            get { return velocity; }
        }
        Vector2 velocity;

        public int Health
        {
            get { return health; }
        }
        int health;

        public void SetHealth(int i)
        {
            health += i;
        }

        public float Energy
        {
            get { return energy; }
        }
        float energy;

        public bool[] canMove = { true, true, true, true };

        private List<Bullet> bullets;
        private Texture2D healthBar;
        private Texture2D energyBar;
        private Texture2D map;
        public Rectangle boundingBox;
        public Rectangle[] boundings = new Rectangle[4];
        private GamePadState currentGamepadState;
        private GamePadState previousGamepadState;
        private ContentManager content;
        private Viewport viewport;
        private SpriteFont healthFont;
        private Sword sword;
        private Texture2D swordTexture;
        private RoomHandler roomHandler;
        public float rotation;
        private bool displayMap;
        private int delay;
        private Vector2 swordPosition;
        bool shield = false;
        Texture2D shieldTexture;

        public Player(string Name, Vector2 Position, ContentManager Content, Viewport Viewport, RoomHandler RM)
        {
            content = Content;
            name = Name;
            texture = content.Load<Texture2D>("Player\\Sheep");
            healthBar = content.Load<Texture2D>("Player\\healthBar");
            energyBar = content.Load<Texture2D>("Player\\energyBar");
            map = content.Load<Texture2D>("Map");
            position = Position;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            healthFont = content.Load<SpriteFont>("HealthFont");
            swordTexture = content.Load<Texture2D>("Weapons\\Swords\\Basic");
            sword = new Sword("Basic", new Vector2(position.X, position.Y), swordTexture, DamageType.Piercing, 5, content, this);
            roomHandler = RM;
            displayMap = false;
            delay = 30;
            swordPosition = position;

            bullets = new List<Bullet>();

            health = 100;
            energy = 100;
            velocity = new Vector2(5f, 5f);
            viewport = Viewport;

            shieldTexture = content.Load<Texture2D>("Weapons\\Shield");
        }

        public void Update(GameTime gameTime)
        {
            previousGamepadState = currentGamepadState;
            currentGamepadState = GamePad.GetState(PlayerIndex.One);

            canMove[0] = true; canMove[1] = true; canMove[2] = true; canMove[3] = true;

            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            boundings[0] = new Rectangle((int)boundingBox.X - (int)origin.X, boundingBox.Y - (int)origin.Y - 6, texture.Width, 6); //North
            boundings[1] = new Rectangle((int)boundingBox.X + (int)origin.X, (boundingBox.Y - (int)origin.Y), 6, texture.Height); //East
            boundings[2] = new Rectangle((int)boundingBox.X - (int)origin.X, boundingBox.Y + (int)origin.Y, texture.Width, 6); //South
            boundings[3] = new Rectangle((int)boundingBox.X - (int)origin.X - 6, boundingBox.Y - (int)origin.Y, 6, texture.Height); //West

            for (int i = 0; i < roomHandler.activeRoom.getObstacleLength(); i++)
            {
                for (int j = 0; j < boundings.Length; j++)
                {
                    if (boundings[j].Intersects(roomHandler.activeRoom.getNObstacle(i).GetRect()))
                    {
                        canMove[j] = false;
                    }
                }
            }

            if (currentGamepadState.ThumbSticks.Left.X > 0 && canMove[1])
                position.X += currentGamepadState.ThumbSticks.Left.X * velocity.X;
            if (currentGamepadState.ThumbSticks.Left.X < 0 && canMove[3])
                position.X += currentGamepadState.ThumbSticks.Left.X * velocity.X;

            if (currentGamepadState.ThumbSticks.Left.Y > 0 && canMove[0])
                position.Y -= currentGamepadState.ThumbSticks.Left.Y * velocity.Y;
            if (currentGamepadState.ThumbSticks.Left.Y < 0 && canMove[2])
                position.Y -= currentGamepadState.ThumbSticks.Left.Y * velocity.Y;

            if (!canMove[0])
                position.Y += 1;
            if (!canMove[2])
                position.Y -= 1;
            if (!canMove[1])
                position.X -= 1;
            if (!canMove[3])
                position.X += 1;

            rotation = (float)Math.Atan2(-currentGamepadState.ThumbSticks.Right.Y, currentGamepadState.ThumbSticks.Right.X);

            position.X = MathHelper.Clamp(position.X, 64 + origin.X, (viewport.Width - 64) - origin.X);
            position.Y = MathHelper.Clamp(position.Y, 64 + origin.Y, (viewport.Height - 64) - origin.Y);

            if (previousGamepadState.IsButtonUp(Buttons.A) && currentGamepadState.IsButtonDown(Buttons.A))
            {
                health--;
            }

            if (health > 200)
                health = 200;

            if (previousGamepadState.IsButtonUp(Buttons.X) && currentGamepadState.IsButtonDown(Buttons.X))
            {
                if (boundingBox.Intersects(roomHandler.activeRoom.NorthDoorBox) && roomHandler.activeRoom.CanTravel[0] == true)
                {
                    roomHandler.SetRoom(0, -1, 0, TransitionDirection.Down);
                }
                if (boundingBox.Intersects(roomHandler.activeRoom.EastDoorBox) && roomHandler.activeRoom.CanTravel[1] == true)
                {
                    roomHandler.SetRoom(1, 0, 0, TransitionDirection.Left);
                }
                if (boundingBox.Intersects(roomHandler.activeRoom.SouthDoorBox) && roomHandler.activeRoom.CanTravel[2] == true)
                {
                    roomHandler.SetRoom(0, 1, 0, TransitionDirection.Up);
                }
                if (boundingBox.Intersects(roomHandler.activeRoom.WestDoorBox) && roomHandler.activeRoom.CanTravel[3] == true)
                {
                    roomHandler.SetRoom(-1, 0, 0, TransitionDirection.Right);
                }
            }

            if (previousGamepadState.IsButtonUp(Buttons.Start) && currentGamepadState.IsButtonDown(Buttons.Start))
            {
                if (displayMap)
                    displayMap = false;
                else
                    displayMap = true;
            }
            if (delay < 0)
                delay = 30;
            else
                delay--;

            CheckSwordCollisions(currentGamepadState);

            sword.Update();

            if (currentGamepadState.Triggers.Left == 1.0f && energy > 15)
            {
                shield = true;
            }
            else
            {
                shield = false;
                energy += 0.1f;
                if (energy >= 100)
                    energy = 100;
            }
        }

        private void CheckSwordCollisions(GamePadState currentGamePadState)
        {
            Console.WriteLine(rotation);
            for (int i = 0; i < roomHandler.activeRoom.EnemyHandler.Enemies.Count; i++)
            {
                if (sword.BoundingBox.Intersects(roomHandler.activeRoom.EnemyHandler.Enemies[i].boundingBox))
                {
                    roomHandler.activeRoom.EnemyHandler.Enemies[i].setHealth(-1);
                }
                if (currentGamepadState.ThumbSticks.Right.Y >= 0.5f && currentGamepadState.ThumbSticks.Right.X >= -0.5f && currentGamepadState.ThumbSticks.Right.X <= 0.5f)
                {
                    sword.SetBoundingBox(new Rectangle((int)position.X, (int)position.Y - 82, 5, 50));

                    if ((roomHandler.activeRoom.EnemyHandler.Enemies[i].getEType() == EnemyType.ARCH || roomHandler.activeRoom.EnemyHandler.Enemies[i].getEType() == EnemyType.DRAGON) && roomHandler.activeRoom.EnemyHandler.Enemies[i].GetBullet().BoundingBox.Intersects(roomHandler.player.boundings[0]) && shield)
                    {
                        roomHandler.activeRoom.EnemyHandler.Enemies[i].SetDmgBullet();
                        energy -= 5;
                    }
                }
                else if (currentGamepadState.ThumbSticks.Right.X >= 0.5f && currentGamepadState.ThumbSticks.Right.Y >= -0.5f && currentGamepadState.ThumbSticks.Right.Y <= 0.5f)
                {
                    sword.SetBoundingBox(new Rectangle((int)position.X + 32, (int)position.Y, 50, 5));

                    if ((roomHandler.activeRoom.EnemyHandler.Enemies[i].getEType() == EnemyType.ARCH || roomHandler.activeRoom.EnemyHandler.Enemies[i].getEType() == EnemyType.DRAGON) && roomHandler.activeRoom.EnemyHandler.Enemies[i].GetBullet().BoundingBox.Intersects(roomHandler.player.boundings[1]) && shield)
                    {
                        roomHandler.activeRoom.EnemyHandler.Enemies[i].SetDmgBullet();
                        energy -= 5;
                    }
                }
                else if (currentGamepadState.ThumbSticks.Right.Y <= -0.5f && currentGamepadState.ThumbSticks.Right.X >= -0.5f && currentGamepadState.ThumbSticks.Right.X <= 0.5f)
                {
                    sword.SetBoundingBox(new Rectangle((int)position.X, (int)position.Y + 32, 5, 50));

                    if ((roomHandler.activeRoom.EnemyHandler.Enemies[i].getEType() == EnemyType.ARCH || roomHandler.activeRoom.EnemyHandler.Enemies[i].getEType() == EnemyType.DRAGON) && roomHandler.activeRoom.EnemyHandler.Enemies[i].GetBullet().BoundingBox.Intersects(roomHandler.player.boundings[2]) && shield)
                    {
                        roomHandler.activeRoom.EnemyHandler.Enemies[i].SetDmgBullet();
                        energy -= 5;
                    }
                }
                else if (currentGamepadState.ThumbSticks.Right.X <= -0.5f && currentGamepadState.ThumbSticks.Right.Y >= -0.5f && currentGamepadState.ThumbSticks.Right.Y <= 0.5f)
                {
                    sword.SetBoundingBox(new Rectangle((int)position.X - 82, (int)position.Y, 50, 5));

                    if ((roomHandler.activeRoom.EnemyHandler.Enemies[i].getEType() == EnemyType.ARCH || roomHandler.activeRoom.EnemyHandler.Enemies[i].getEType() == EnemyType.DRAGON) && roomHandler.activeRoom.EnemyHandler.Enemies[i].GetBullet().BoundingBox.Intersects(roomHandler.player.boundings[3]) && shield)
                    {
                        roomHandler.activeRoom.EnemyHandler.Enemies[i].SetDmgBullet();
                        energy -= 5;
                    }
                }
                else
                {
                    sword.SetBoundingBox(new Rectangle((int)position.X + 32, (int)position.Y, 50, 5));

                    if ((roomHandler.activeRoom.EnemyHandler.Enemies[i].getEType() == EnemyType.ARCH || roomHandler.activeRoom.EnemyHandler.Enemies[i].getEType() == EnemyType.DRAGON) && roomHandler.activeRoom.EnemyHandler.Enemies[i].GetBullet().BoundingBox.Intersects(roomHandler.player.boundings[1]) && shield)
                    {
                        roomHandler.activeRoom.EnemyHandler.Enemies[i].SetDmgBullet();
                        energy -= 5;
                    }
                }
            }

            if (energy <= 0)
                energy = 0;

            if (health <= 0)
            {
                position = new Vector2(viewport.Width / 2, viewport.Height / 2);
                roomHandler.currentX = 0;
                roomHandler.currentY = 0;
                roomHandler.activeRoom = roomHandler.Rooms[0, 0];
                health = 100;
                energy = 100;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw Health bars
            int offset = 0;
            for (int i = 0; i < health; i++)
            {
                if (i % 10 == 0 && i != 0)
                {
                    offset += 5;
                }
                spriteBatch.Draw(healthBar, new Vector2((64 + (i * 1)) + offset, 560), Color.White);
            }

            //Draw energy bar
            for (int i = 0; i < (int)energy; i++)
            {
                spriteBatch.Draw(energyBar, new Vector2(64 + (i * 1), 580), Color.White);
            }

            //Draw player
            spriteBatch.Draw(texture, position, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(healthFont, Convert.ToString(roomHandler.currentX), Vector2.Zero, Color.White);
            spriteBatch.DrawString(healthFont, Convert.ToString(roomHandler.currentY), new Vector2(0, 20), Color.White);
            if (roomHandler.activeRoom.EnemyHandler.Enemies.Count != 0)
            {
                spriteBatch.Draw(content.Load<Texture2D>("Player\\swordBar"), new Rectangle((int)sword.BoundingBox.X, (int)sword.BoundingBox.Y, sword.BoundingBox.Width, sword.BoundingBox.Height), Color.White);
            }
            
            sword.Draw(spriteBatch);
            if (shield)
                spriteBatch.Draw(shieldTexture, position, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);

            if (displayMap)
            {
                spriteBatch.Draw(map, Vector2.Zero, Color.White);
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (roomHandler.Rooms[i, j].Discovered)
                            spriteBatch.Draw(content.Load<Texture2D>("MapSquare" + i + j), new Vector2(i * 200, j * 150), Color.White);

                        if (roomHandler.activeRoom == roomHandler.Rooms[i, j])
                            spriteBatch.Draw(content.Load<Texture2D>("PlayerLoc"), new Vector2(i * 200, j * 150), Color.White);
                    }
                }
            }
        }
    }
}
