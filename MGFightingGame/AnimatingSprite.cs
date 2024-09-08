using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Text;

namespace MGFightingGame
{
    abstract class AnimatingSprite : Sprite
    {
        TimeSpan timeSpan;




        /// <summary>
        /// Abracadabra
        /// </summary>
        //MAKE THIS A PROPERTY ^
        //More info: https://sharplab.io/#v2:CYLg1APgAgTAjAWAFDNgAgIIDsCeAXACwEssBzNZAb2TVtoAcAnIgNwEM8BTNEvNLAK4BbAEadGAbhp00UAMxoAynmZk0AdQD2jYGkqlOeCQF9ptM7IW80AOWFjGF6khkyDeCzOeufdKHABOAAoAIiJgAGsQgEopF19XKAB2fntxCRlPOlN4nwBnQyzabwS6IgAzIPYAGwFuAD5U0XFootcS0tdBZsY0AF40Grq4ztoc0dlA0IAJAFEAGXmAeQBCGJHfceyLC3lMXEISUiDrbocAGkmABjQAd21gVty9NsO8gDoz8X6mhw2fLQ6H73HT/VwWLbmZ57KAAFjQAAVVHggk8vG1/MEvoxYhipoDHmDIRRnjtoQo4UpDHYeicsHwsJxbjSHGi6B1XBUgozmWleo1sWyfByEtifjyWek2sTibsrPS0ABxal81FONqyFLYjJ0CFkmQwuA3ZV4Alq54ig0pAk6kkyHI5VAUmBoADCVF2MBg6ueMmsBE41Wqmh+ATgMAAHGCDUa0MQfiFiCFo21sPhiGo2AcM+QBjz9umjkEA0HNJdiLE7b42lnC2R3pLegMAldo7rfX4prXDvXG7iO7Ruzn3kjeKi27QAPSTodHd4EhMl4MrFdrCdVnxtGvZuej+njrcDjdoB1AA===

        public abstract FrameHelper currentFrames
        {
            get;
        }

        public override Rectangle SourceRectangle { get => currentFrames.Frames[currentIndex];}
        public int currentIndex { get; set; }

        public AnimatingSprite(Texture2D texture, Vector2 origin, Vector2 position, Vector2 scale, Color color, SpriteEffects spriteEffects, float rotation, float layerDepth)
            : base(texture, origin, position, scale, color, spriteEffects, rotation, layerDepth)
        {

            currentIndex = 0;

            timeSpan = TimeSpan.Zero;
            //CurrentFrames = currentFrames;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Texture, Position, currentFrames.Frames[currentIndex], Color, Rotation, Origin, Scale, Effects, LayerDepth);
        }

        public virtual void Update(GameTime gameTime)
        {


            timeSpan += gameTime.ElapsedGameTime;


            if (timeSpan.TotalMilliseconds > 100)
            {

                currentIndex++;
                timeSpan = TimeSpan.Zero;
                if (currentIndex == currentFrames.Frames.Length)
                {
                    currentIndex = 0;
                    EndOfAnimation();
                }
                EndOfFrame();
            }


        }

        protected abstract void EndOfFrame();

        protected abstract void EndOfAnimation();
    }
}
