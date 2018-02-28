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

        Vector2 oorsprong;
        Ster zon, groeneSter, rodeSter;

        float screenWidth, screenHeight;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            oorsprong = Vector2.Zero;
            screenWidth = graphics.PreferredBackBufferWidth;
            screenHeight = graphics.PreferredBackBufferHeight;

            Print("BackbufferWidth, BackbufferHeight = " + screenWidth + "," + screenHeight);                  

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //scale, rotation, sprite.
            zon = new Ster(0.1f, 0f, Content.Load<Texture2D>("sprites/spr_sun"));
            //Door position later aan te maken, is het mogelijk om variabeles van Ster te gebruiken in de position berekening.
            zon.position = new Vector2(screenWidth / 2, screenHeight / 2);

            Print("zon.position = " + zon.position);
            Print("zon.rotationPoint = " + zon.rotationPoint);

            groeneSter = new Ster(0.05f, 0f, Content.Load<Texture2D>("sprites/spr_sun"));
            groeneSter.position = new Vector2(screenWidth - groeneSter.scaledWidth / 2, groeneSter.scaledHeight / 2);

            rodeSter = new Ster(0.05f, 0f, Content.Load<Texture2D>("sprites/spr_sun"));
            rodeSter.position = new Vector2(rodeSter.scaledWidth / 2, rodeSter.scaledHeight / 2);
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

            MoveStars();

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
            DrawSprite(zon, Color.White);
            DrawSprite(groeneSter, Color.Aqua);
            DrawSprite(rodeSter, Color.HotPink);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        //Een methode om spriteBatch.Draw iets compacter te maken.
        public void DrawSprite(Ster gameObject, Color color)
        {
            spriteBatch.Draw(gameObject.sprite, gameObject.position, null, color, gameObject.rotationSpeed, gameObject.rotationPoint, gameObject.scale, SpriteEffects.None, 0f);
        }

        public void Print(string output = "")
        {
            Console.WriteLine();
            Console.WriteLine(output);
            Console.WriteLine();
        }

        public void MoveStars()
        {
            zon.rotationSpeed += 0.2f;

            groeneSter.rotationSpeed -= 0.1f;
            groeneSter.position.X -= 0.4f;
            groeneSter.position.Y += 0.2f;

            rodeSter.rotationSpeed += 0.5f;
            rodeSter.position.X += 0.2f;
            rodeSter.position.Y += 0.4f;
        }
    }
}
