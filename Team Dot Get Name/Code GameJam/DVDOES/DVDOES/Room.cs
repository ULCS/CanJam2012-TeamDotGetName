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
    class Room
    {
        private enum startOrient { NORTH, EAST, SOUTH, WEST };
        private bool[] canTravel; public bool[] CanTravel { get { return canTravel; } }
        private Texture2D background; public Texture2D getBackground() { return background; }
        private Obstacle[] roomObjects; public int getObstacleLength() { return roomObjects.Length; }
        public Obstacle getNObstacle(int n) { return roomObjects[n]; }
        private EnemyHandler enemyHandler; public EnemyHandler EnemyHandler { get { return enemyHandler; } }
        private Rectangle northDoorBox; public Rectangle NorthDoorBox { get { return northDoorBox; } }
        private Rectangle eastDoorBox; public Rectangle EastDoorBox { get { return eastDoorBox; } }
        private Rectangle southDoorBox; public Rectangle SouthDoorBox { get { return southDoorBox; } }
        private Rectangle westDoorBox; public Rectangle WestDoorBox { get { return westDoorBox; } }
        private Viewport viewport;
        private bool discovered; public bool Discovered { get { return discovered; } }
        
        public void setDiscovered(bool Discovered)
        { 
            discovered = Discovered;
        }

        public Room(Texture2D BG, bool[] CT, Obstacle[] RO, EnemyHandler EnemyHandler, Viewport Viewport)
        {
            background = BG;
            canTravel = CT;
            roomObjects = RO;
            enemyHandler = EnemyHandler;
            viewport = Viewport;
            discovered = false;

            northDoorBox = new Rectangle((viewport.Width / 2) - 33, 64, 66, 66);
            eastDoorBox = new Rectangle(viewport.Width - 64, (viewport.Height / 2) - 33, 66, 66);
            southDoorBox = new Rectangle((viewport.Width / 2) - 33, viewport.Height - 64, 66, 66);
            westDoorBox = new Rectangle(64, (viewport.Height / 2) - 33, 66, 66); 

        }
    }
}
