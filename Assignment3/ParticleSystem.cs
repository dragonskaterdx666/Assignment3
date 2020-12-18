using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Assignment3
{
    public class ParticleSystem
    {
        #region Variables
        private GraphicsDevice _gd;
        private BasicEffect _effect;
        private List<RainDrop> _rainDrops;
        private Vector3 _normal, _center;
        private Random _rnd;

        private float _radius;
        private float _initialPos; 
        private float _initialSpeed;
        private float _initialSpeedOffset;
        private int _particleNum;
        private int _particleNumOffset;
        #endregion
        
        #region Constructor
        public ParticleSystem(GraphicsDevice gd, Vector3 center, float radius, Vector3 normal, float initialPos, float initialSpeed, float initialSpeedOffset, int particleNum, 
            int particleNumOffset)
        {
            float aspectRatio = (float)gd.Viewport.Width / gd.Viewport.Height;

            _gd = gd;
            _rainDrops = new List<RainDrop>();
            _rnd = new Random();
            _initialPos = initialPos;
            _initialSpeed = initialSpeed;
            _initialSpeedOffset = initialSpeedOffset;
            _particleNum = particleNum;
            _particleNumOffset = particleNumOffset;
            _center = center; 
            _normal = normal;
            _radius = radius;
    
            
            _effect = new BasicEffect(gd);
            _effect.View = Matrix.CreateLookAt(new Vector3(0.0f, 2.0f, 2.0f), Vector3.Zero, Vector3.Up);
            _effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), aspectRatio, 0.01f, 1000f);
            _effect.VertexColorEnabled = true;
        }
        #endregion
        
        #region Methods
        /// <summary>
        /// Updates the particles
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            int totalParticles = _particleNum + (int)(_rnd.NextDouble() * _particleNumOffset - _particleNumOffset);
            
            for (int i = 0; i < totalParticles; i++)
            {
                Vector3 position, speed;
                
                position = computeParticlePosition();
                speed = computeParticleSpeed();
                
                _rainDrops.Add(new RainDrop(position, speed));
            }

            for (int i = _rainDrops.Count - 1; i >= 0; i--)
            {
                 if (_rainDrops[i].Current.Y < 0) 
                     _rainDrops.RemoveAt(i);
            }
            

            for (int i = 0; i < _rainDrops.Count; i++)
            {
                _rainDrops[i].Update(gameTime);
            }
            
        }

        /// <summary>
        /// Computes the particle's position
        /// </summary>
        /// <returns></returns>
        public Vector3 computeParticlePosition()
        {
            float angle = (float)_rnd.NextDouble() * 2 * (float)Math.PI;
            float diameter = (float)_rnd.NextDouble() * _radius;
            return _center + new Vector3(diameter * (float)Math.Cos(angle), 0, diameter * (float)Math.Sin(angle)); 
        }

        /// <summary>
        /// Computes the particle's speed
        /// </summary>
        /// <returns></returns>
        public Vector3 computeParticleSpeed()
        {
            float x = (float)_rnd.NextDouble() * _initialPos - _initialPos;
            float y = 0f;
            float z = (float)_rnd.NextDouble() * _initialPos - _initialPos;

            Vector3 direction = _normal + new Vector3(x, y, z);
            direction.Normalize(); 
            
            float norm = _initialSpeed + (float)_rnd.NextDouble() * 2 * _initialSpeedOffset - _initialSpeedOffset;
            
            return direction * norm;
        }
        
        /// <summary>
        /// Draws the particles
        /// </summary>
        public void Draw()
        {
            VertexPositionColor[] line = new VertexPositionColor[2 * _rainDrops.Count];        
            float size = 0.1f;

            _effect.World = Matrix.Identity;
            _effect.CurrentTechnique.Passes[0].Apply();
            
            //draws the rain
            for (int i = 0; i < _rainDrops.Count; i++)
            {
                line[2 * i + 0] = new VertexPositionColor(_rainDrops[i].Current, Color.White);
                line[2 * i + 1] = new VertexPositionColor(_rainDrops[i].Current + Vector3.Normalize(_rainDrops[i].Speed) * size, Color.Purple);
            }
            
            _gd.DrawUserPrimitives(PrimitiveType.LineList, line, 0, _rainDrops.Count); 
        }
        #endregion
    }
}

