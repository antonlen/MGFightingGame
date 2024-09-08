using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGFightingGame
{

    public enum FightResult
    {
        Player1Lose,
        Player2Lose,
        Continue
    }

    internal class Fighting
    {
        public Character player1;
        public Character player2;
        bool isAttackingP1 = false;
        bool isAttackingP2 = false;
        //bool parried = false;
        //public bool stunned = false;     
       

        TimeSpan timeSpan;

        public Fighting(Character player1, Character player2 )
        {
            this.player1 = player1;
            this.player2 = player2;
        }


        public FightResult Update(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
 
            KeyboardState keyboardState = Keyboard.GetState();   

            if ((player1.currentIndex == player1.currentFrames.VIPFrame && player1.currentAnimation == AnimationTypes.Punch) && player1.stunned == false)
            {
                if ((player1.Hitbox.Intersects(player2.Hitbox) && isAttackingP1 == false))
                { 
                    isAttackingP1 = true;
                    if (player2.currentAnimation == AnimationTypes.Block)
                    {
                        player1.parried = true;
                        player1.currentAnimation = AnimationTypes.Stunned;
                    }

                    else
                    {
                        player1.parried = false;
                    }
                   
                    if (player1.parried == false)
                    {
                        player2.Health -= 4;

                        if (player2.Health <= 0)
                        {
                            return FightResult.Player1Lose;
                        }
                    }
                }
            }
            else if ((player1.currentIndex == player1.currentFrames.VIPFrame && player1.currentAnimation == AnimationTypes.Kick) && player1.stunned == false)
            {

                if (player1.Hitbox.Intersects(player2.Hitbox) && isAttackingP1 == false)
                {
                    isAttackingP1 = true;
                    if (player2.currentAnimation == AnimationTypes.Block)
                    {
                        player1.parried = true;
                        player1.currentAnimation = AnimationTypes.Stunned;
                    }

                    else
                    {
                        player1.parried = false;
                    }

                    if (player1.parried == false)
                    {
                        player2.Health -= 4;

                        if (player2.Health <= 0)
                        {
                            return FightResult.Player1Lose;
                        }
                    }
                }
            }

            else
            {
                isAttackingP1 = false;
            }

            if ((player2.currentIndex == player2.currentFrames.VIPFrame && player2.currentAnimation == AnimationTypes.Punch) && player2.stunned == false)
            {
                if (player2.Hitbox.Intersects(player1.Hitbox) && isAttackingP2 == false)
                {
                    isAttackingP2 = true;
                    if (player1.currentAnimation == AnimationTypes.Block)
                    {
                        player2.parried = true;
                        player2.currentAnimation = AnimationTypes.Stunned;
                    }

                    else
                    {
                        player2.parried = false;
                    }

                    if (player2.parried == false)
                    {
                        player1.Health -= 4;

                        if (player1.Health <= 0)
                        {
                            return FightResult.Player1Lose;
                            
                        }
                    }
                }
            }

            else if ((player2.currentIndex == player2.currentFrames.VIPFrame && player2.currentAnimation == AnimationTypes.Kick) && player2.stunned == false)
            {
                if (player2.Hitbox.Intersects(player1.Hitbox) && isAttackingP2 == false)
                {
                    isAttackingP2 = true;
                    if (player1.currentAnimation == AnimationTypes.Block)
                    {
                        player2.parried = true;
                        player2.currentAnimation = AnimationTypes.Stunned;
                    }

                    else
                    {
                        player2.parried = false;
                    }

                    if (player2.parried == false)
                    {
                        player1.Health -= 4;

                        if (player1.Health <= 0)
                        {
                            return FightResult.Player1Lose;
                        }
                    }
                }
            }

            else
            {
                isAttackingP2 = false;
            }

            if(player1.parried == true)
            {
                
                timeSpan += gameTime.ElapsedGameTime;
                player1.stunned = true;
            }

           

            if (player2.parried == true)
            {
                timeSpan += gameTime.ElapsedGameTime;
                player2.stunned = true;
            } 
            
            if(timeSpan.TotalSeconds >= 2 && player2.stunned == true)
            {

                player2.stunned = false;
                player2.parried = false;
                if(player2.Health <= 0)
                {

                }

                else
                {
                    player2.currentAnimation = AnimationTypes.Idle;
                }

                
                timeSpan = TimeSpan.Zero;
            }

            if(timeSpan.TotalSeconds >= 2 && player1.stunned == true)
            {
                player1.stunned = false;
                player1.parried = false;

                if(player1.Health <= 0)
                {

                }
                else
                {
                    player1.currentAnimation = AnimationTypes.Idle;
                }
           

                timeSpan = TimeSpan.Zero;
            }

            if(player2.Health <= 0 && player1.currentAnimation != AnimationTypes.Victory)
            {
                player1.currentAnimation = AnimationTypes.Victory;
                player2.currentAnimation = AnimationTypes.KO;
                player1.currentIndex = 0;
                player2.currentIndex = 0;
            }

            if(player1.Health <= 0 && player2.currentAnimation != AnimationTypes.Victory)
            {
                player2.currentAnimation = AnimationTypes.Victory;
                player1.currentAnimation = AnimationTypes.KO;
                
                player1.currentIndex = 0;
                player2.currentIndex = 0;
            }

            if(player1.currentAnimation == AnimationTypes.Victory || player2.currentAnimation == AnimationTypes.Victory)
            {
                if (keyboardState.IsKeyDown(Keys.T))
                {
                    player1.Health = 100;
                    player2.Health = 100;
                    player1.Position = new Vector2(100, graphicsDevice.Viewport.Height / 2);
                    player2.Position = new Vector2(650, graphicsDevice.Viewport.Height / 2);
                    player1.currentIndex = 0;
                    player2.currentIndex = 0;
                    player1.currentAnimation = AnimationTypes.Idle;
                    player2.currentAnimation = AnimationTypes.Idle;
                }
            }
            return FightResult.Continue;
        }
    }
}
