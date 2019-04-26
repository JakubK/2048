using System;
using System.Collections.Generic;
using System.Text;

namespace Game2048.Services
{
    public interface IScoreSaver
    {
        void Save(string key, int value);
    }
}