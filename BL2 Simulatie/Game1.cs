using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Windows;

namespace BL2_Simulatie
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Vector2 oorsprong;
        Ster zon, aarde, aarde2;
        Kracht MPZ;

        Formules form = new Formules();

        float screenWidth, screenHeight;
        double tijdstap = 1;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            oorsprong = Vector2.Zero;

            //Standaard = (800, 480)
            int fullscreenWidth = GraphicsDevice.DisplayMode.Width;
            int fullscreenHeight = GraphicsDevice.DisplayMode.Height;

            graphics.PreferredBackBufferWidth = 1400;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            screenWidth = graphics.PreferredBackBufferWidth;
            screenHeight = graphics.PreferredBackBufferHeight;

            int windowPositionX = (int)((fullscreenWidth / 2) - (screenWidth / 2));
            int windowPositionY = (int)((fullscreenHeight / 2) - (screenHeight / 2));

            this.Window.Position = new Point(windowPositionX, windowPositionY);
            this.IsMouseVisible = true;

            //Print("fullscreenWidth, fullscreenHeight = " + fullscreenWidth + ", " + fullscreenHeight);
            //Print("screenWidth, screenHeight = " + screenWidth + ", " + screenHeight);
            //Print("windowPositionX, windowPositionY = " + windowPositionX + ", " + windowPositionY);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //scale, rotation, sprite.
            zon = new Ster(0.1f, 0f, Content.Load<Texture2D>("sprites/spr_sun"), 1000, 10, Math.PI);
            //Door position later aan te maken, is het mogelijk om variabeles van Ster te gebruiken in de position berekening.
            //zon.position = new Vector2(screenWidth / 2, screenHeight / 2);
            zon.position = new Vector2(-10, 0);

            aarde = new Ster(0.5f, 0f, Content.Load<Texture2D>("sprites/spr_earth"), 10, 10, 0);
            aarde.position = new Vector2(0, 0);

            //aarde2 = new Ster(0.5f, 0f, Content.Load<Texture2D>("sprites/spr_earth"), 10, 1000);
            //aarde2.position = new Vector2(screenWidth * 0.25f, screenHeight / 2);

            MPZ = new Kracht(Math.PI * 0.75, 1);
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
            DrawSprite(aarde, Color.White);
            //DrawSprite(aarde2, Color.White);
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
            Console.WriteLine(output);
        }

        public void MoveStars()
        {
            //zon.rotationSpeed += (float)Math.PI / 20;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                //aarde.rotationSpeed += (float)aarde.direction / 100;

                //aarde.direction += (float)form.NieuweRichting(aarde.direction, MPZ.power);
                //Print("direction = " + aarde.direction + " ==> " + (aarde.direction / Math.PI) + " PI.");

                double d = form.AfstandTussenHemellichamen(aarde.position, zon.position);
                double Fvel = form.Fv(aarde.mass, aarde.velocity, tijdstap);
                double Fmiddel = form.Fmpz(aarde.mass, aarde.velocity, d);

                Print("d = " + d);
                Print("Fvel = " + Fvel);
                Print("Fmpz = " + Fmiddel);

                Print("Oude aarde.direction = " + aarde.direction);
                aarde.direction -= (form.NieuweRichting(Fvel, Fmiddel));
                Print("Nieuwe aarde.direction = " + aarde.direction);
                //aarde.rotationSpeed = (float)aarde.direction / 100;

                double Fres = Math.Sqrt(Math.Pow(Fvel, 2) + Math.Pow(Fmiddel, 2));
                double Fx = form.Fx(Fres, aarde.direction);
                double Fy = form.Fy(Fres, aarde.direction);
                Print("Fres = " + Fres);
                Print("Fx = " + Fx);
                Print("Fy = " + Fy);

                Print("Oude position = " + aarde.position);
                aarde.position.X += (float)form.FmaVerplaatsing(Fx, aarde.mass, tijdstap);
                aarde.position.Y -= (float)form.FmaVerplaatsing(Fy, aarde.mass, tijdstap);
                Print("Nieuwe position = " + aarde.position);
                Print();
            }
        }
    }
}
