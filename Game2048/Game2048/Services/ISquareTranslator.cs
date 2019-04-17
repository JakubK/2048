using Game2048.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Game2048.Services
{
    public interface ISquareTranslator
    {
        void TranslateVertically(int direction);
        void TranslateHorizontally(int direction);
    }
}
