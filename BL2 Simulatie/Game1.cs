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

        Vector2 origin, centerScreen;
        CelestialBody star, pandora;

        Formules form = new Formules();

        float screenWidth, screenHeight;

        double tijdstap = 1;
        double metersPerPixel = 467140935;
        double a = 163499327000;
        double b = 162957623016;
        double c = 13298234111;
        double aPix, bPix, cPix;

        //double massaZon = 20154851311000000000000000000;
        //double massaPandora = 6389658258000000000000000;

        //10 keer kleiner vanwege te groot getal.
        double massaZon = 2015485131100000000;
        double massaPandora = 638965825800000;
        double omloopSnelheid = 3257.536051;
 
        bool test = false;
        bool test2 = false;
        bool test3 = false;
        bool resultaatOutput = true;
        int metingNummer = 1;
        string lijn = "====================================================";



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            origin = Vector2.Zero;

            //Laptop scherm afmetingen? = (1536, 864)
            int fullscreenWidth = GraphicsDevice.DisplayMode.Width;
            int fullscreenHeight = GraphicsDevice.DisplayMode.Height;

            if (test)
            {
                Print();
                Print(lijn);
                Print("Max = " + double.MaxValue);
                Print(graphics.PreferredBackBufferWidth + ", " + graphics.PreferredBackBufferHeight);
            }

            //Standaard = (800, 480)
            //Grootste window = (1400, 720)
            graphics.PreferredBackBufferWidth = 700;
            graphics.PreferredBackBufferHeight = 700;
            graphics.ApplyChanges();

            screenWidth = graphics.PreferredBackBufferWidth;
            screenHeight = graphics.PreferredBackBufferHeight;
            centerScreen = new Vector2(screenWidth / 2, screenHeight / 2);

            int windowPositionX = (int)((fullscreenWidth / 2) - (screenWidth / 2));
            int windowPositionY = (int)((fullscreenHeight / 2) - (screenHeight / 2));

            this.Window.Position = new Point(windowPositionX, windowPositionY);
            this.IsMouseVisible = true;

            aPix = a / metersPerPixel;
            bPix = b / metersPerPixel;
            cPix = c / metersPerPixel;

            if (test)
            {
                Print();
                Print("middenScherm = " + centerScreen);
                Print("fullscreenWidth, fullscreenHeight = " + fullscreenWidth + ", " + fullscreenHeight);
                Print("screenWidth, screenHeight = " + screenWidth + ", " + screenHeight);
                Print("windowPositionX, windowPositionY = " + windowPositionX + ", " + windowPositionY);
                Print("aPix = " + aPix);
                Print("bPix = " + bPix);
                Print("cPix = " + cPix);
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //scale, rotation, sprite.
            star = new CelestialBody(0.05f, 0f, Content.Load<Texture2D>("sprites/spr_sun"), massaZon, 0, 0);
            //Door position later aan te maken, is het mogelijk om variabeles van Ster te gebruiken in de position berekening.
            star.position = new Vector2(centerScreen.X + (float)cPix, centerScreen.Y);

            pandora = new CelestialBody(0.01f, 0f, Content.Load<Texture2D>("sprites/spr_sun"), massaPandora, omloopSnelheid, 0);
            pandora.position = new Vector2(centerScreen.X + (float)aPix, centerScreen.Y);

            if (test2)
            {
                Print();
                Print("zon.position = " + star.position);
                Print("aarde.position = " + pandora.position);
            }
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
            DrawSprite(star, Color.White);
            DrawSprite(pandora, Color.Green);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        //Een methode om spriteBatch.Draw iets compacter te maken.
        public void DrawSprite(CelestialBody gameObject, Color color)
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

                double d = form.AfstandTussenHemellichamen(pandora.position, star.position) * metersPerPixel;
                double Fvel = form.Fv(pandora.mass, pandora.velocity, tijdstap);
                double Fmiddel = form.Fmpz(pandora.mass, pandora.velocity, d);

                if (test3)
                {
                    Print(lijn);
                    Print("d = " + d);
                    Print("(Fvel, Fmpz) = (" + Fvel + ", " + Fmiddel + ")");
                }

                double oudDir = pandora.direction;
                //Linksom draaien
                pandora.direction -= (form.NieuweRichting(Fvel, Fmiddel));

                double Fres = Math.Sqrt(Math.Pow(Fvel, 2) + Math.Pow(Fmiddel, 2));
                double Fx = form.Fx(Fres, pandora.direction);
                double Fy = form.Fy(Fres, pandora.direction);

                Vector2 oudPos = pandora.position;
                pandora.position.X += (float)form.FmaVerplaatsing(Fx, pandora.mass, tijdstap);
                pandora.position.Y -= (float)form.FmaVerplaatsing(Fy, pandora.mass, tijdstap);

                if (test3)
                {
                    Print("(Oude aarde.direction, Nieuwe aarde.direction) = (" + oudDir + ", " + pandora.direction + ")");
                    Print("Fres = " + Fres);
                    Print("(Fx, Fy) = (" + Fx + ", " + Fy + ")");
                    Print("Oude position, Nieuwe position = " + oudPos + ", " + pandora.position);
                    Print();
                }

                if (resultaatOutput)
                {
                    Print("Meting " + metingNummer + " = " + pandora.position);
                    metingNummer++;
                }
            }
        }
    }
}
