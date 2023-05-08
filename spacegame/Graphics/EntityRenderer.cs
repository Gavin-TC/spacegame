using Spacegame.Entities;
using Spacegame.Utilities;

namespace Spacegame.Graphics
{
    public class EntityRenderer
    {
        private Camera camera;
        
        private char[,] buffer;
        private int bufferWidth;
        private int bufferHeight;
        
        public EntityRenderer(Camera camera, int bufferWidth, int bufferHeight, int screenWidth, int screenHeight)
        {
            this.camera = camera;
            this.bufferWidth = bufferWidth;
            this.bufferHeight = bufferHeight;

            buffer = new char[this.bufferHeight, this.bufferWidth];
        }

        public void DrawEntity(int x, int y, char character)
        {
            buffer[y, x] = character;
        }
        
        public void RenderEntities(Camera camera, EntityRenderer entityRenderer, List<Entity> entities)
        {
            // Get the position of the camera
            int cameraX = camera.X;
            int cameraY = camera.Y;

            // Determine the boundaries of the screen based on the camera position and the size of the screen
            int screenWidth = entityRenderer.bufferWidth;
            int screenHeight = entityRenderer.bufferHeight;
            int screenLeft = cameraX - screenWidth / 2;
            int screenRight = cameraX + screenWidth / 2;
            int screenTop = cameraY - screenHeight / 2;
            int screenBottom = cameraY + screenHeight / 2;

            // Iterate over the list of entities and check if their position is within the boundaries of the screen
            foreach (Entity entity in entities)
            {
                int entityX = entity.x;
                int entityY = entity.y;

                if (entityX >= screenLeft && entityX <= screenRight && entityY >= screenTop && entityY <= screenBottom)
                {
                    // For each entity that is within the screen boundaries, call its Draw method with the EntityRenderer object as a parameter
                    entity.Draw(entityRenderer);
                }
            }
        }


        public void RenderEntityMap()
        {
            for (var rows = 0; rows < bufferHeight; rows++)
            {
                for (var cols = 0; cols < bufferWidth; cols++)
                {
                    Console.Write(buffer[rows, cols]);
                }
                Console.WriteLine();
            }
        }
        
        public void ClearBuffer()
        {
            for (int y = 0; y < bufferHeight; y++)
            {
                for (int x = 0; x < bufferWidth; x++)
                {
                    buffer[y, x] = ' ';
                }
            }
        }

        public void DrawBuffer()
        {
            for (int row = 0; row < camera.Height; row++)
            {
                for (int col = 0; col < camera.Width; col++)
                {
                    int mapX = camera.X + col;
                    int mapY = camera.Y + row;

                    // Only draw characters that are within the bounds of the map
                    if (mapX >= 0 && mapX < bufferWidth && mapY >= 0 && mapY < bufferHeight)
                    {
                        char tileChar = Global.currentMap[mapY, mapX];
                        DrawEntity(col, row, tileChar);
                    }
                }
            }
        }
    }
}