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
    enum TransitionDirection
    {
        Up, Down, Left, Right
    }

    class RoomHandler
    {
        public Room[,] Rooms
        {
            get { return rooms; }
        }
        private Room[,] rooms = new Room[4, 4];
        Obstacle[] objects_room_0_0 = new Obstacle[10];
        Obstacle[] objects_room_1_0 = new Obstacle[0];
        Obstacle[] objects_room_2_0 = new Obstacle[0];
        Obstacle[] objects_room_3_0 = new Obstacle[0];
        Obstacle[] objects_room_0_1 = new Obstacle[0];
        Obstacle[] objects_room_1_1 = new Obstacle[0];
        Obstacle[] objects_room_2_1 = new Obstacle[0];
        Obstacle[] objects_room_3_1 = new Obstacle[0];
        Obstacle[] objects_room_0_2 = new Obstacle[0];
        Obstacle[] objects_room_1_2 = new Obstacle[0];
        Obstacle[] objects_room_2_2 = new Obstacle[0];
        Obstacle[] objects_room_3_2 = new Obstacle[0];
        Obstacle[] objects_room_0_3 = new Obstacle[0];
        Obstacle[] objects_room_1_3 = new Obstacle[0];
        Obstacle[] objects_room_2_3 = new Obstacle[0];
        Obstacle[] objects_room_3_3 = new Obstacle[0];

        private Texture2D doorNorth;
        private Texture2D doorEast;
        private Texture2D doorSouth;
        private Texture2D doorWest;

        public Room activeRoom;
        Viewport viewport;
        public Player player;

        Texture2D oldBackground;

        Vector2 curBGPos = new Vector2(0, 0);
        Vector2 oldBGPos = new Vector2(0, 0);
        Vector2 transitionDirectionVector;

        bool transition = false;

        ContentManager content;

        public int currentX;
        public int currentY;

        public TransitionDirection transitionDirection;
        private Texture2D fence_01;
        private Texture2D fence_02;
        private Texture2D fence_03;
        private Texture2D fence_04;
        private Texture2D fence_05;
        private Texture2D fence_06;
        private Texture2D fence_07;
        private Texture2D fence_08;
        private Texture2D fence_09;
        private Texture2D fence_10;

        public RoomHandler(ContentManager CM, Viewport VP)
        {
            currentX = 0;
            currentY = 0;

            content = CM;
            viewport = VP;
            fence_01 = content.Load<Texture2D>("BGs\\Fences\\fence_01");
            fence_02 = content.Load<Texture2D>("BGs\\Fences\\fence_02");
            fence_03 = content.Load<Texture2D>("BGs\\Fences\\fence_03");
            fence_04 = content.Load<Texture2D>("BGs\\Fences\\fence_04");
            fence_05 = content.Load<Texture2D>("BGs\\Fences\\fence_05");
            fence_06 = content.Load<Texture2D>("BGs\\Fences\\fence_06");
            fence_07 = content.Load<Texture2D>("BGs\\Fences\\fence_07");
            fence_08 = content.Load<Texture2D>("BGs\\Fences\\fence_08");
            fence_09 = content.Load<Texture2D>("BGs\\Fences\\fence_09");
            fence_10 = content.Load<Texture2D>("BGs\\Fences\\fence_10");

            objects_room_0_0[0] = new Obstacle(35, 195, fence_01);
            objects_room_0_0[1] = new Obstacle(225, 195, fence_02);
            objects_room_0_0[2] = new Obstacle(300, 45, fence_03);
            objects_room_0_0[3] = new Obstacle(500, 195, fence_04);
            objects_room_0_0[4] = new Obstacle(570, 40, fence_05);
            objects_room_0_0[5] = new Obstacle(221, 377, fence_06);
            objects_room_0_0[6] = new Obstacle(221, 377, fence_07);
            objects_room_0_0[7] = new Obstacle(396, 377, fence_08);
            objects_room_0_0[8] = new Obstacle(461, 403, fence_09);
            objects_room_0_0[9] = new Obstacle(660, 377, fence_10);
            //private enum startOrient { NORTH, EAST, SOUTH, WEST };
            //CONTENT LOAD
            player = new Player("2012", new Vector2(viewport.Width / 2, viewport.Height / 2), content, viewport, this);
            Texture2D background = content.Load<Texture2D>("BGs\\Temp_BG");
            rooms[0, 0] = new Room(content.Load<Texture2D>("BGs\\Background00"), new bool[4] { false, true, false, false }, objects_room_0_0, new EnemyHandler(0, new Vector2[0] {}, viewport, content, this, new EnemyType[3] { EnemyType.ARCH, EnemyType.BASE, EnemyType.CHARGE }), viewport);
            rooms[1, 0] = new Room(content.Load<Texture2D>("BGs\\Background01"), new bool[4] { false, false, true, true }, objects_room_1_0, new EnemyHandler(3, new Vector2[3] { new Vector2(700, 150), new Vector2(500, 300), new Vector2(300, 400) }, viewport, content, this, new EnemyType[3] { EnemyType.ARCH, EnemyType.BASE, EnemyType.CHARGE }), viewport);
            rooms[2, 0] = new Room(content.Load<Texture2D>("BGs\\Background30"), new bool[4] { false, false, true, false }, objects_room_2_0, new EnemyHandler(3, new Vector2[3] { new Vector2(100, 200), new Vector2(200, 300), new Vector2(300, 400) }, viewport, content, this, new EnemyType[3] { EnemyType.ARCH, EnemyType.BASE, EnemyType.CHARGE }), viewport);
            rooms[3, 0] = new Room(content.Load<Texture2D>("BGs\\Background30"), new bool[4] { false, false, true, false }, objects_room_3_0, new EnemyHandler(3, new Vector2[3] { new Vector2(100, 200), new Vector2(200, 300), new Vector2(300, 400) }, viewport, content, this, new EnemyType[3] { EnemyType.ARCH, EnemyType.BASE, EnemyType.CHARGE }), viewport);
            rooms[0, 1] = new Room(content.Load<Texture2D>("BGs\\Background30"), new bool[4] { false, false, true, false }, objects_room_0_1, new EnemyHandler(3, new Vector2[3] { new Vector2(100, 200), new Vector2(200, 300), new Vector2(300, 400) }, viewport, content, this, new EnemyType[3] { EnemyType.ARCH, EnemyType.BASE, EnemyType.CHARGE }), viewport);
            rooms[1, 1] = new Room(content.Load<Texture2D>("BGs\\Background02"), new bool[4] { true, true, true, false }, objects_room_1_1, new EnemyHandler(3, new Vector2[3] { new Vector2(100, 200), new Vector2(200, 300), new Vector2(300, 400) }, viewport, content, this, new EnemyType[3] { EnemyType.ARCH, EnemyType.BASE, EnemyType.CHARGE }), viewport);
            rooms[2, 1] = new Room(content.Load<Texture2D>("BGs\\Background01"), new bool[4] { true, true, true, true }, objects_room_2_1, new EnemyHandler(3, new Vector2[3] { new Vector2(100, 200), new Vector2(200, 300), new Vector2(300, 400) }, viewport, content, this, new EnemyType[3] { EnemyType.ARCH, EnemyType.BASE, EnemyType.CHARGE }), viewport);
            rooms[3, 1] = new Room(content.Load<Texture2D>("BGs\\Background02"), new bool[4] { true, false, true, true }, objects_room_3_1, new EnemyHandler(3, new Vector2[3] { new Vector2(100, 200), new Vector2(200, 300), new Vector2(300, 400) }, viewport, content, this, new EnemyType[3] { EnemyType.ARCH, EnemyType.BASE, EnemyType.CHARGE }), viewport);
            rooms[0, 2] = new Room(content.Load<Texture2D>("BGs\\Background03"), new bool[4] { true, true, false, false }, objects_room_0_2, new EnemyHandler(3, new Vector2[3] { new Vector2(100, 200), new Vector2(200, 300), new Vector2(300, 400) }, viewport, content, this, new EnemyType[3] { EnemyType.ARCH, EnemyType.BASE, EnemyType.CHARGE }), viewport);
            rooms[1, 2] = new Room(content.Load<Texture2D>("BGs\\Background02"), new bool[4] { true, true, true, true }, objects_room_1_2, new EnemyHandler(3, new Vector2[3] { new Vector2(100, 200), new Vector2(200, 300), new Vector2(300, 400) }, viewport, content, this, new EnemyType[3] { EnemyType.ARCH, EnemyType.BASE, EnemyType.CHARGE }), viewport);
            rooms[2, 2] = new Room(content.Load<Texture2D>("BGs\\Background20"), new bool[4] { true, true, false, true }, objects_room_2_2, new EnemyHandler(1, new Vector2[1] { new Vector2(400, 300) }, viewport, content, this, new EnemyType[1] { EnemyType.DRAGON }), viewport);
            rooms[3, 2] = new Room(content.Load<Texture2D>("BGs\\Background01"), new bool[4] { true, false, true, true }, objects_room_3_2, new EnemyHandler(3, new Vector2[3] { new Vector2(100, 200), new Vector2(200, 300), new Vector2(300, 400) }, viewport, content, this, new EnemyType[3] { EnemyType.ARCH, EnemyType.BASE, EnemyType.CHARGE }), viewport);
            rooms[0, 3] = new Room(content.Load<Texture2D>("BGs\\Background03"), new bool[4] { false, true, false, false }, objects_room_0_3, new EnemyHandler(3, new Vector2[3] { new Vector2(100, 200), new Vector2(200, 300), new Vector2(300, 400) }, viewport, content, this, new EnemyType[3] { EnemyType.ARCH, EnemyType.BASE, EnemyType.CHARGE }), viewport);
            rooms[1, 3] = new Room(content.Load<Texture2D>("BGs\\Background20"), new bool[4] { true, false, false, true }, objects_room_1_3, new EnemyHandler(3, new Vector2[3] { new Vector2(100, 200), new Vector2(200, 300), new Vector2(300, 400) }, viewport, content, this, new EnemyType[3] { EnemyType.ARCH, EnemyType.BASE, EnemyType.CHARGE }), viewport);
            rooms[2, 3] = new Room(content.Load<Texture2D>("BGs\\Background00"), new bool[4] { false, true, false, false }, objects_room_2_3, new EnemyHandler(3, new Vector2[3] { new Vector2(100, 200), new Vector2(200, 300), new Vector2(300, 400) }, viewport, content, this, new EnemyType[3] { EnemyType.ARCH, EnemyType.BASE, EnemyType.CHARGE }), viewport);
            rooms[3, 3] = new Room(content.Load<Texture2D>("BGs\\Background20"), new bool[4] { true, false, false, true }, objects_room_3_3, new EnemyHandler(3, new Vector2[3] { new Vector2(100, 200), new Vector2(200, 300), new Vector2(300, 400) }, viewport, content, this, new EnemyType[3] { EnemyType.ARCH, EnemyType.BASE, EnemyType.CHARGE }), viewport);

                

            activeRoom = rooms[currentX, currentY];
            activeRoom.setDiscovered(true);

            doorNorth = content.Load<Texture2D>("BGs\\DoorNorth");
            doorEast = content.Load<Texture2D>("BGs\\DoorEast");
            doorSouth = content.Load<Texture2D>("BGs\\DoorSouth");
            doorWest = content.Load<Texture2D>("BGs\\DoorWest");
        }

        public void SetRoom(int X, int Y, int START, TransitionDirection TransDirection)
        {
            transitionDirection = TransDirection;
            currentX += X;
            currentY += Y;
            transition = true;
            oldBackground = activeRoom.getBackground();
            activeRoom = rooms[currentX, currentY];
            activeRoom.setDiscovered(true);

            switch (transitionDirection)
            {
                case TransitionDirection.Down:
                    curBGPos = new Vector2(0.0f, -664.0f);
                    transitionDirectionVector = new Vector2(0.0f, 10.0f);
                    player.setPosition(new Vector2(viewport.Width / 2, viewport.Height - 96));
                    break;

                case TransitionDirection.Left:
                    curBGPos = new Vector2(864.0f, 0.0f);
                    transitionDirectionVector = new Vector2(-12f, 0f);
                    player.setPosition(new Vector2(96, viewport.Height / 2));
                    break;

                case TransitionDirection.Up:
                    curBGPos = new Vector2(0.0f, 664.0f);
                    transitionDirectionVector = new Vector2(0.0f, -10.0f);
                    player.setPosition(new Vector2(viewport.Width / 2, 96));
                    break;

                case TransitionDirection.Right:
                    curBGPos = new Vector2(-864.0f, 0.0f);
                    transitionDirectionVector = new Vector2(12f, 0f);
                    player.setPosition(new Vector2(viewport.Width - 96, viewport.Height / 2));
                    break;
            }
            oldBGPos = Vector2.Zero;
        }

        public void Update(GameTime gameTime)
        {
            if (!transition)
            {
                curBGPos = Vector2.Zero;
                player.Update(gameTime);
                activeRoom.EnemyHandler.Update();
            }

            activeRoom.EnemyHandler.Update();


            if (transition)
            {
                curBGPos += transitionDirectionVector;
                oldBGPos += transitionDirectionVector;

                switch (transitionDirection)
                {
                    case TransitionDirection.Down:
                        if (curBGPos.Y >= 0.0)
                            transition = false;
                        break;

                    case TransitionDirection.Left:
                        if (curBGPos.X <= 0.0)
                            transition = false;
                        break;

                    case TransitionDirection.Up:
                        if (curBGPos.Y <= 0.0)
                            transition = false;
                        break;

                    case TransitionDirection.Right:
                        if (curBGPos.X >= 0.0)
                            transition = false;
                        break;
                }
            }

            for (int i = 0; i < activeRoom.EnemyHandler.Enemies.Count; i++)
            {
                if (activeRoom.EnemyHandler.Enemies[i].Health <= 0)
                    activeRoom.EnemyHandler.Enemies.RemoveAt(i);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(activeRoom.getBackground(), curBGPos, Color.White);

            if (transition)
                spriteBatch.Draw(oldBackground, oldBGPos, Color.White);


            if (activeRoom.CanTravel[0] && !transition)
                spriteBatch.Draw(doorNorth, new Vector2((viewport.Width / 2) - 64, 0), Color.White);

            if (activeRoom.CanTravel[1] && !transition)
                spriteBatch.Draw(doorEast, new Vector2((viewport.Width - 64), (viewport.Height / 2) - 64), Color.White);

            if (activeRoom.CanTravel[2] && !transition)
                spriteBatch.Draw(doorSouth, new Vector2((viewport.Width / 2) - 64, viewport.Height - 64), Color.White);

            if (activeRoom.CanTravel[3] && !transition)
                spriteBatch.Draw(doorWest, new Vector2(0, viewport.Height / 2 - 64), Color.White);

           

            if (!transition)
            {
                for (int i = 0; i < activeRoom.getObstacleLength(); i++)
                {
                    spriteBatch.Draw(activeRoom.getNObstacle(i).GetTexture(), activeRoom.getNObstacle(i).GetRect(), Color.White);
                }
                activeRoom.EnemyHandler.Draw(spriteBatch);
            }

            if (!transition)
                player.Draw(spriteBatch);
        }
    }
}
