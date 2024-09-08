using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Text;

namespace MGFightingGame
{
 
    public enum AnimationTypes
    {
        Idle,
        Walk,
        Punch,
        Kick,
        Jump,
        Hit,
        Victory,
        KO,
        Block,
        Stunned
    }

    class Character : AnimatingSprite
    {


        //Keys leftKey = Keys.A;
        //Keys rightKey = Keys.D;
        //Keys jumpKey = Keys.W;
        //Keys punchKey = Keys.J;
        //Keys kickKey = Keys.K;
        //Keys blockKey = Keys.L;

        public Keys LeftKey;
        public Keys RightKey;
        public Keys JumpKey;
        public Keys PunchKey;
        public Keys KickKey;
        public Keys BlockKey;
        

        public Vector2 Speed;
        public Vector2 JumpSpeed;
        int floor;
        public AnimationTypes currentAnimation;
        public override FrameHelper currentFrames { get { return Frames[currentAnimation]; } }
        public override Vector2 Origin
        {
            get
            {
                if (Effects == SpriteEffects.FlipHorizontally)
                {
                    return new Vector2(Hitbox.Width - base.Origin.X, base.Origin.Y);
                }
                return base.Origin;
            }
            set => base.Origin = value;
        }
        public Dictionary<AnimationTypes, FrameHelper> Frames;

        public bool isJumping;

        public int Health = 100;
        public int Player2Health = 100;

        //public Character player2;

        public bool stunned;

        public bool parried;

        TimeSpan blockTime;
        TimeSpan blockWait;
        bool blockEnd;

        public Healthbar Healthbar;

        public Texture2D Pixel;
        //bool isBlocked = false;

        const int timeBetweenBlocks = 2000;
        public Character(Texture2D texture, Vector2 origin, Vector2 position, Vector2 scale, Color color, SpriteEffects spriteEffects, float rotation, float layerDepth,
                         Dictionary<AnimationTypes, FrameHelper> frames, AnimationTypes chosenFrames, Vector2 speed, Vector2 jumpSpeed, int floor, Keys leftKey,
                         Keys rightKey, Keys jumpKey, Keys punchKey, Keys kickKey, Keys blockKey, Texture2D pixel)
            : base(texture, origin, position, scale, color, spriteEffects, rotation, layerDepth)
        {
            Speed = speed;
            JumpSpeed = jumpSpeed;
            Frames = frames;
            currentAnimation = chosenFrames;
            this.floor = floor;
            LeftKey = leftKey;
            RightKey = rightKey;
            JumpKey = jumpKey;
            PunchKey = punchKey;
            KickKey = kickKey;
            BlockKey = blockKey;
            Pixel = pixel;

            blockWait = new(0, 0, 0, 0, timeBetweenBlocks);

            Healthbar = new Healthbar(Pixel, new Vector2(position.X, position.Y - 200), Color.Green);
        }
        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            Position += Speed;
            Position.Y += JumpSpeed.Y;
            //isBlocked = false;

            bool canJump = true;
            if (Hitbox.Bottom >= floor)
            {

                if (JumpSpeed.Y > 0)
                {
                    isJumping = false;
                    JumpSpeed.Y = 0;
                    Speed.X = 0;
                }
            }

            else
            {
                JumpSpeed.Y += 0.5f;
                canJump = false;
            }
            if (!stunned)
            {
                if (currentAnimation == AnimationTypes.Block)
                {
                    blockWait = TimeSpan.Zero;
                    blockTime += gameTime.ElapsedGameTime;

                }
                if (blockTime.TotalSeconds >= 0.5)
                {
                    if (blockEnd == true)
                    {
                        currentAnimation = AnimationTypes.Idle;
                        blockEnd = false;
                    }
                    blockWait += gameTime.ElapsedGameTime;

                }

                if (currentAnimation == AnimationTypes.Idle || currentAnimation == AnimationTypes.Walk)
                {
                    if (keyboardState.IsKeyDown(LeftKey) && !isJumping)
                    {
                        Speed.X = -3;
                        if (currentAnimation != AnimationTypes.Walk)
                        {
                            currentAnimation = AnimationTypes.Walk;

                            currentIndex = 0;
                        }
                    }



                    else if (keyboardState.IsKeyDown(RightKey) && !isJumping)
                    {
                        Speed.X = 3;
                        if (currentAnimation != AnimationTypes.Walk)
                        {
                            currentAnimation = AnimationTypes.Walk;
                            currentIndex = 0;
                        }
                    }

                    if (keyboardState.IsKeyDown(JumpKey) && canJump)
                    {
                        isJumping = true;
                        Speed.X = 0;
                        JumpSpeed.Y = -10;
                        currentAnimation = AnimationTypes.Jump;
                        currentIndex = 0;
                        if (keyboardState.IsKeyDown(RightKey))
                        {
                            Speed.X = 3;
                        }

                        else if (keyboardState.IsKeyDown(LeftKey))
                        {
                            Speed.X = -3;
                        }

                    }


                    else if (keyboardState.IsKeyDown(PunchKey) && !isJumping)
                    {

                        Speed.X = 0;
                        currentAnimation = AnimationTypes.Punch;
                        currentIndex = 0;
                        //if(Hitbox.Intersects(player2.Hitbox))
                        //{

                        //}
                    }

                    else if (keyboardState.IsKeyDown(KickKey) && !isJumping)
                    {
                        Speed.X = 0;
                        currentAnimation = AnimationTypes.Kick;
                        currentIndex = 0;
                    }

                    else if (keyboardState.IsKeyDown(BlockKey) && !isJumping && blockWait.TotalSeconds >= 2)
                    {
                        //isBlocked = true;
                        blockEnd = true;
                        blockTime = TimeSpan.Zero;
                        Speed.X = 0;
                        currentAnimation = AnimationTypes.Block;
                        currentIndex = 0;
                    }

                    if(currentAnimation == AnimationTypes.Victory)
                    {
                        if(keyboardState.IsKeyDown(Keys.R))
                        {
                            
                        }
                    }


                }



                //if(Health <= 0)
                //{
                //    currentAnimation = AnimationTypes.Victory;
                //}
            }

           
            if (!((currentAnimation == AnimationTypes.KO || currentAnimation == AnimationTypes.Victory || currentAnimation == AnimationTypes.Stunned) && currentIndex == currentFrames.Frames.Length - 1))
            {
                base.Update(gameTime);
            }
        }


        protected override void EndOfAnimation()
        {
            //animSprite.currentAnimation = AnimatingSprite.AnimationTypes.Idle;
            //player1.Update();
            if (currentAnimation == AnimationTypes.Walk)
            {
                Speed.X = 0;
            }

            if (currentAnimation == AnimationTypes.Jump)
            {
                isJumping = false;
                //JumpSpeed.Y = 0;
            }
            if (currentAnimation == AnimationTypes.Block) //&& isBlocked)
            {
                return;
            }
            currentAnimation = AnimationTypes.Idle;

        }

        protected override void EndOfFrame()
        {
            base.Origin = new Vector2(base.Origin.X, SourceRectangle.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Texture, Hitbox, Color.Red);
            base.Draw(spriteBatch);
            //spriteBatch.Draw(Texture, new Rectangle(Position.ToPoint() - new Point(5), new Point(10, 10)), Color.Black);
            //spriteBatch.Draw(Pixel, (new Rectangle(50, 50, 100, 10)), Color.Black);
            Healthbar.Draw(spriteBatch, Health);

        }
    }
}
