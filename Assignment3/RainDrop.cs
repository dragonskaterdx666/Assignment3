using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    public class RainDrop
    {
        #region  Variables
        private Vector3 _currentPos;
        private Vector3 _speed;
        #endregion

        #region Constructor
        public RainDrop(Vector3 currentPos, Vector3 speed)
        {
            _currentPos = currentPos;
            _speed = speed;
        }
        #endregion

        #region Properties
        public Vector3 Current => _currentPos;
        public Vector3 Speed => _speed;
        #endregion
            
        #region Methods
        public float Random(float nMin, float nMax)
        {
            float random = 0;
            Random rnd = new Random();
            
            random = (rnd.Next(100) % (nMax - nMin)) + nMin;
            return random;
        }


        public void Update(GameTime gameTime)
        {
            _speed += new Vector3(0, -1f, 0) * (float)gameTime.ElapsedGameTime.TotalSeconds * Random(0.05f, 3f);
            _currentPos += _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        #endregion
    }
}
