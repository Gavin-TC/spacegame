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

            buffer = new char[this.bufferWidth, this.bufferHeight];
        }

        public void DrawEntity(int x, int y, char character)
        {
            if (x >= 0 && x < bufferHeight && y >= 0 && y < bufferWidth)
            {
                buffer[y, x] = character;
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