using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Assignment3
{
    public class Game1 : Game
    {
        #region Variables
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Plane _plane;
        private ParticleSystem _particleSystem;
        #endregion

        #region  Constructor
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        #endregion
     
        #region Properties
        public Plane Plane => _plane;
        #endregion
        
        #region Methods
        protected override void Initialize()
        {
            _particleSystem = new ParticleSystem(GraphicsDevice, new Vector3(0, 6, 0), 5, Vector3.Down, 5f, 3f, 0.5f, 50, 1); 
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _plane = new Plane(GraphicsDevice, Content.Load<Texture2D>("plane"));
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            _particleSystem.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _plane.Draw();
            _particleSystem.Draw();

            base.Draw(gameTime);
        }
        #endregion
    }
}
