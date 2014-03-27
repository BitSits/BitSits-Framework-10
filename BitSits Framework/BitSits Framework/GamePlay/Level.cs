using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace BitSits_Framework
{
    class Level : IDisposable
    {
        #region Fields


        public int Score { get; private set; }

        public bool IsLevelUp { get; private set; }
        public bool ReloadLevel { get; private set; }
        int levelIndex;

        GameContent gameContent;

        BasicEffect basicEffect;
        //Render render = new Render();


        #endregion

        #region Initialization


        public Level(ScreenManager screenManager, int levelIndex)
        {
            this.gameContent = screenManager.GameContent;
            this.levelIndex = levelIndex;

            basicEffect = new BasicEffect(screenManager.GraphicsDevice);
            basicEffect.VertexColorEnabled = true;

            //Render._device = screenManager.GraphicsDevice;
            //Render._batch = screenManager.SpriteBatch;
            //Render._font = debugFont;
        }


        private void LoadTiles(int levelIndex)
        {
            // Load the level and ensure all of the lines are the same length.
            int width;
            List<string> lines = new List<string>();
            lines = gameContent.content.Load<List<string>>("Levels/level" + levelIndex.ToString("0"));

            width = lines[0].Length;
            // Loop over every tile position,
            for (int y = 0; y < lines.Count; ++y)
            {
                if (lines[y].Length != width)
                    throw new Exception(String.Format(
                        "The length of line {0} is different from all preceeding lines.", lines.Count));

                for (int x = 0; x < lines[0].Length; ++x)
                {
                    // to load each tile.
                    char tileType = lines[y][x];
                    LoadTile(tileType, x, y);
                }
            }
        }


        private void LoadTile(char tileType, int x, int y)
        {
            switch (tileType)
            {
                case '.': break;

                // Unknown tile type character
                default:
                    throw new NotSupportedException(String.Format(
                        "Unsupported tile type character '{0}' at position {1}, {2}.", tileType, x, y));
            }
        }


        public void Dispose() { }


        #endregion

        #region Update and HandleInput


        public void Update(GameTime gameTime)
        {

        }


        public void HandleInput(InputState input, int playerIndex)
        {
            if (input.IsMouseLeftButtonClick()) IsLevelUp = true;
            if (input.IsMouseRightButtonClick()) ReloadLevel = true;
        }


        #endregion

        #region Draw


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(gameContent.gameFont, "Level : " + (levelIndex + 1),
                new Vector2(400, 20), Color.White);

            //basicEffect.Techniques[0].Passes[0].Apply();
            //render.FinishDrawShapes();
            //render.FinishDrawString();
        }


        #endregion
    }
}
