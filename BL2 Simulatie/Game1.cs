using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace BL2_Simulatie
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D zonSprite;
        Vector2 oorsprong, zonPositie, zonOrigin;

        
        float zonScale = 0.1f;
        float zonRotation = 0f;

        float screenWidth, screenHeight, zonWidth, zonHeight;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            screenWidth = graphics.PreferredBackBufferWidth;
            screenHeight = graphics.PreferredBackBufferHeight;

            Print("BackbufferWidth, BackbufferHeight = " + screenWidth + "," + screenHeight);

            oorsprong = Vector2.Zero;
            zonOrigin = Vector2.Zero;
            zonPositie = Vector2.Zero;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            zonSprite = Content.Load<Texture2D>("sprites/spr_sun");
            zonWidth = zonSprite.Width * zonScale;
            zonHeight = zonSprite.Height * zonScale;

            Print("zonWidth, zonHeight = " + zonWidth + ", " + zonHeight);

            //zonPositie is de plek op het scherm.
            zonPositie = new Vector2(screenWidth / 2, screenHeight / 2);
            Print("zonPositie = " + zonPositie);

            //zonOrigin is een draaipunt op de ORIGINELE sprite.
            //Daarom moet je hier zonSprite.Width / 2 gebruiken en niet zonWidth / 2.
            zonOrigin = new Vector2(zonSprite.Width / 2, zonSprite.Height / 2);
            Print("zonOrigin = " + zonOrigin);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MoveSun();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(zonSprite, zonPositie, null, Color.White, zonRotation, zonOrigin, zonScale, SpriteEffects.None, 0f);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void Print(string output = "")
        {
            Console.WriteLine();
            Console.WriteLine(output);
            Console.WriteLine();
        }

        public void MoveSun()
        {
            //zonPositie.X += 1;
            //zonPositie.Y += 2;

            //Roteert om de linkerbovenhoek van een png.
            zonRotation += 0.2f;
        }
    }
}
