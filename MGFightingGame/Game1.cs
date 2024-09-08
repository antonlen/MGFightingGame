using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;

namespace MGFightingGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        Character player1;
        Character player2;
        Fighting combatManager;
        Texture2D animSpriteTextureP1;
        Texture2D animSpriteTextureP2;
        Texture2D stageTexture;
        Texture2D pixel;
        TimeSpan timeSpan;

        int floor = 412;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Dictionary<AnimationTypes, FrameHelper> frames = new Dictionary<AnimationTypes, FrameHelper>();

            frames.Add(AnimationTypes.Idle, new FrameHelper(new Rectangle[] { new Rectangle(6, 18, 43, 81), new Rectangle(55, 19, 43, 80), new Rectangle(105, 18, 43, 81), new Rectangle(154, 17, 43, 82) }, -1));
            frames.Add(AnimationTypes.Walk, new FrameHelper(new Rectangle[] { new Rectangle(205, 24, 43, 75), new Rectangle(252, 19, 43, 80), new Rectangle(301, 18, 43, 81), new Rectangle(351, 19, 43, 80), new Rectangle(401, 19, 43, 80) }, -1));
            frames.Add(AnimationTypes.Punch, new FrameHelper(new Rectangle[] { new Rectangle(170, 134, 43, 81), new Rectangle(218, 130, 51, 85), new Rectangle(274, 130, 72, 85), new Rectangle(411, 134, 43, 81) }, 2));
            frames.Add(AnimationTypes.Kick, new FrameHelper(new Rectangle[] { new Rectangle(6, 261, 49, 85), new Rectangle(62, 259, 67, 87), new Rectangle(135, 261, 49, 85) }, 1));
            frames.Add(AnimationTypes.Jump, new FrameHelper(new Rectangle[] { new Rectangle(452, 24, 43, 75), new Rectangle(503, 9, 33, 90), new Rectangle(545, 17, 29, 78), new Rectangle(582, 19, 31, 67), new Rectangle(619, 17, 29, 78), new Rectangle(656, 9, 33, 90), new Rectangle(696, 24, 43, 75) }, -1));
            frames.Add(AnimationTypes.Block, new FrameHelper(new Rectangle[] { new Rectangle(1211, 16, 43, 83) }, -1));
            frames.Add(AnimationTypes.Hit, new FrameHelper(new Rectangle[] { new Rectangle(5, 760, 43, 76), new Rectangle(53, 761, 47, 75), new Rectangle(106, 771, 49, 65), new Rectangle(163, 754, 43, 82) }, -1));
            frames.Add(AnimationTypes.Victory, new FrameHelper(new Rectangle[] { new Rectangle(6, 887, 43, 75), new Rectangle(57, 875, 43, 87), new Rectangle(108, 853, 43, 109) }, -1));
            frames.Add(AnimationTypes.KO, new FrameHelper(new Rectangle[] { new Rectangle(1165, 781, 45, 59), new Rectangle(1218, 789, 72, 42), new Rectangle(1295, 806, 74, 30), new Rectangle(1373, 789, 72, 42), new Rectangle(1450, 806, 74, 30) }, -1));
            frames.Add(AnimationTypes.Stunned, new FrameHelper(new Rectangle[] { new Rectangle(1003, 757, 51, 79), new Rectangle(1060, 755, 43, 81), new Rectangle(1110, 754, 43, 82) }, -1));

            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new Color[] { Color.White });
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            animSpriteTextureP1 = Content.Load<Texture2D>("player1sheet");
            animSpriteTextureP2 = Content.Load<Texture2D>("player2sheet");
            stageTexture = Content.Load<Texture2D>("streetFighterStage");
            player1 = new Character(animSpriteTextureP1, new Vector2(43 / 2, 81), new Vector2(100, GraphicsDevice.Viewport.Height / 2), Vector2.One, Color.White, SpriteEffects.None, 0, 0,
               frames, AnimationTypes.Walk, new Vector2(0, 0), new Vector2(0, 0), floor, Keys.A, Keys.D, Keys.W, Keys.E, Keys.R, Keys.F, pixel);
            player2 = new Character(animSpriteTextureP2, new Vector2(43 / 2, 81), new Vector2(650, GraphicsDevice.Viewport.Height / 2), Vector2.One, Color.White, SpriteEffects.FlipHorizontally, 0, 0,
               frames, AnimationTypes.Walk, new Vector2(0, 0), new Vector2(0, 0), floor, Keys.Left, Keys.Right, Keys.Up, Keys.M, Keys.OemComma, Keys.OemPeriod, pixel);
            combatManager = new Fighting(player1, player2);


            SpriteFont font = Content.Load<SpriteFont>("MessageFont");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            timeSpan += gameTime.ElapsedGameTime;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();



            player2.Update(gameTime);
            player1.Update(gameTime);
            combatManager.Update(gameTime, GraphicsDevice);


            // player2.Rotation += .1f;
            // player1.Rotation += .1f;




            //player1.Position += player1.JumpSpeed;


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();  
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Draw(stageTexture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            player1.Draw(spriteBatch);
           
                player2.Draw(spriteBatch);
            
                // TODO: Add your drawing code here

           // spriteBatch.Draw(animSpriteTextureP1, new Rectangle(0, floor, GraphicsDevice.Viewport.Width, 30), Color.Red);
     

            //spriteBatch.DrawString()

            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
