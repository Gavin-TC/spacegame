﻿using Spacegame.Input;
using Spacegame.Quests;
using Spacegame.Utilities;

namespace Spacegame.PlayerStuff
{
    public class Player
    {
        private HandleInput? _handleInput;

        public Inventory Inventory { get; set; }
        public Quests Quests { get; set; }

        public int px { get; set; } // Player left and right coordinate
        public int py { get; set; }

        // public int dirX; // Player direction up and down
        // public int dirY;

        public int dirX = 0;
        public int dirY = 0;
        
        public char playerChar;
        public string playerName;

        public bool interactState = false;
        
        public int speed { get; set; }

        public int health { get; set; }
        public int exp { get; set; } // Experience points
        public int level { get; set; } // Current level

        // private bool shoot = false;
        // private bool bulletActive = false;

        public Player(string playerName, int speed, int health, int exp, int level)
        {
            this.speed = speed;
            this.health = health;
            this.exp = exp;
            this.level = level;
            this.playerName = playerName;
        }
        
        public void Initialize()
        {
            px = 5;
            py = 5;

            _handleInput = new HandleInput();
            _handleInput.InputEvent += OnInput;

            var inputThread = new Thread(_handleInput.CheckInput);
            inputThread.Start();
        }

        public (int px, int py) Position()
        {
            return (px, py);
        }

        public void OnInput(ConsoleKey key)
        {
            //Console.WriteLine("PRESSING KEY " + key);
            switch (key)
            {
                case ConsoleKey.W:
                    dirY = -1;
                    break;

                case ConsoleKey.S:
                    dirY = 1;
                    break;

                case ConsoleKey.D:
                    dirX = 1;
                    break;

                case ConsoleKey.A:
                    dirX = -1;
                    break;

                case ConsoleKey.E: // interact;
                    interactState = !interactState;
                    break;
                
                // case ConsoleKey.Spacebar: // Shoot;
                //     shoot = true;
                //     break;
            }
        }
        
        public bool Obstruction()
        {
            if (dirY is -1 or 1)
            {
                if (dirY == -1 && Global.currentMap[py - 1, px] is Global.wallChar or Global.closedDoorChar
                    || dirY == 1 && Global.currentMap[py + 1, px] is Global.wallChar or Global.closedDoorChar)
                {
                    return true;
                }
            }

            if (dirX is -1 or 1)
            {
                if (dirX == 1 && Global.currentMap[py, px + 1] is Global.wallChar or Global.closedDoorChar
                    || dirX == -1 && Global.currentMap[py, px - 1] is Global.wallChar or Global.closedDoorChar)
                {
                    return true;
                }
            }

            return false;
        }
        
        // _mapRenderer.DrawCharacter(playerX, playerY, '@'); // Draw player character at current position

        public void Movement()
        {
            // Move player based on input
            if (!Obstruction())
            {
                px += dirX;
                py += dirY;
            }
        }

        // TODO: COME BACK TO THIS LATER!
        // TODO: ALLOW DIRECTIONS TO BE FLOAT VALUES! RISE/RUN!
        
        /*
        Bullet bullet;
         private void Shoot()
         {
        
             if (!bulletActive && shoot)
             {
                 bullet = new Bullet(px, py, -1, 1, 50, "Bullet", 1);
                 shoot = false;
                 bulletActive = true;
             }
             if (shoot && bulletActive)
             {
                 bullet.Move();
                 Console.WriteLine("SHOOTING");
        
                 if (bullet.CollidedWall())
                 {
                     shoot = false;
                 }
             }
         }
        */

        public void Render()
        {
            Global.currentMap[py, px] = playerChar;
        }
        
        public void Update(double deltaTime)
        {
            if (!interactState)
            {
                Movement();
                // Shoot();
            }
            dirX = 0;
            dirY = 0;
        }
    }

    public partial class Inventory
    {
        private Dictionary<string, int> items;
        
        public Inventory()
        {
            items = new Dictionary<string, int>();
        }

        // Adds an item to the inventory.
        public void AddItem(string itemName, int count)
        {
            // If we have the item, 
            if (items.ContainsKey(itemName))
            {
                items[itemName] += count;
            }
            else
            {
                items.Add(itemName, count);
            }
        }

        // Removes an item from the inventory.
        public void RemoveItem(string itemName, int count)
        {
            if (items.ContainsKey(itemName))
            {
                // If the amount being removed is >= the amount had, remove it entirely.
                if (items[itemName] <= count)
                {
                    items.Remove(itemName);
                }
                // Else, just remove [count] items.
                else
                {
                    items[itemName] -= count;
                }
            }
        }

        // Returns the number of [itemName](s).
        public int GetItemCount(string itemName)
        {
            // If we have the item, return the amount we have.
            if (items.ContainsKey(itemName))
            {
                return items[itemName];
            }
            // Else, return 0 (which is the amount we have)
            return 0;
        }
        
        // Just return the entire list of items.
        public Dictionary<string, int> GetItems()
        {
            return items;
        }
    }

    public partial class Quests
    {
        private QuestManager _questManager;

        public Quests()
        {
            _questManager = new QuestManager();
        }

        public void AcceptQuest(Quest quest)
        {
            _questManager.AddQuest(quest);
        }

        public void CompleteQuest(Quest quest)
        {
            _questManager.CompleteQuest(quest);
        }
        
        public IEnumerable<Quest> GetActiveQuests()
        {
            return _questManager.GetActiveQuests();
        }

        public IEnumerable<Quest> GetCompletedQuests()
        {
            return _questManager.GetCompletedQuests();
        }
    }
}