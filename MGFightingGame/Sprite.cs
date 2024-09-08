using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Text;

namespace MGFightingGame
{
    public class Sprite 
    {
        public Texture2D Texture { get; set; }
        Vector2 origin;
        public virtual Vector2 Origin { get => origin; set => origin = value; }
        public Vector2 Position;// { get; set; }
        public Vector2 Scale { get; set; }
        public Color Color { get; set; }
        public SpriteEffects Effects { get; set; }
        public float Rotation { get; set; }

        public float LayerDepth { get; set; }

        public virtual Rectangle SourceRectangle { get;}

        public Rectangle Hitbox
        {
            get
            {
                if(Effects == SpriteEffects.None)
                {
                    return new Rectangle((int)(Position.X - origin.X), (int)(Position.Y - origin.Y), (int)ScaledSize.X, (int)ScaledSize.Y);
                }
                
                else
                {
                    return new Rectangle((int)(Position.X + origin.X - ScaledSize.X), (int)(Position.Y - origin.Y), (int)ScaledSize.X, (int)ScaledSize.Y);
                }
                //SET ORIGIN TO BOTTOM BC OF VICTORY ANIMATION
            }
        }

        public Vector2 ScaledSize
        {
            get
            {
                return new Vector2(SourceRectangle.Width * Scale.X, SourceRectangle.Height * Scale.Y);
            }
        }

        public Sprite(Texture2D texture, Vector2 origin, Vector2 position, Vector2 scale, Color color, SpriteEffects effects, float rotation, float layerDepth)
        {
            Texture = texture;
            Origin = origin;
            Position = position;
            Scale = scale;
            Color = color;
            Effects = effects;
            Rotation = rotation;
            LayerDepth = layerDepth;
            //Pixel = new Texture2D();
        }



        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color, Rotation, Origin, Scale, Effects, LayerDepth);
        }

    }
}
