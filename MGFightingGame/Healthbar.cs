using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace MGFightingGame
{
    internal class Healthbar : Sprite
    {

        Texture2D Pixel;

        Rectangle backGroundHealth;
        public Healthbar(Texture2D texture, Vector2 position, Color color) 
            : base(texture, Vector2.Zero, position, Vector2.One, color, SpriteEffects.None, 0, 0)
        {
            Pixel = texture;
            backGroundHealth = new Rectangle((int)Position.X, (int)Position.Y, 100, 10);
        }
        
        public void Draw(SpriteBatch spriteBatch, int health)
        {
            spriteBatch.Draw(Pixel, backGroundHealth, Color.Black);
            spriteBatch.Draw(Pixel, (new Rectangle((int)Position.X, (int)Position.Y, health, 10)), Color.Green);

        }
    }
}
