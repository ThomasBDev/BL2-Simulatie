using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BL2_Simulatie
{
    public class CelestialBody
    {
        public float scale, rotationSpeed, scaledWidth, scaledHeight;
        public double mass, velocity, direction;
        //zon.position is de plek op het scherm.
        //zon.rotationPoint is een draaipunt op de ORIGINELE sprite.
        public Vector2 position, rotationPoint;
        public Texture2D sprite;

        public CelestialBody(float scale, float rotation, Texture2D sprite, double mass, double velocity, double direction)
        {
            this.scale = scale;
            rotationSpeed = rotation;
            this.sprite = sprite;
            scaledWidth = sprite.Width * scale;
            scaledHeight = sprite.Height * scale;            
            rotationPoint = new Vector2(sprite.Width / 2, sprite.Height / 2);
            this.mass = mass * 10000000000;
            this.velocity = velocity;
            this.direction = direction;
        }
    }
}
