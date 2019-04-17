using System;
using System.Collections.Generic;
using System.Text;

namespace Game2048.Services
{
    public interface ISquareSpawner
    {
        void SpawnSquares(int count);
    }
}
