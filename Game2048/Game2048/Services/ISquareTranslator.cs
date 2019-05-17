using Game2048.Enums;
using Game2048.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Game2048.Services
{
    public interface ISquareTranslator
    {
        void TranslateVertically(MoveDirection direction);
        void TranslateHorizontally(MoveDirection direction);
        bool ChangeOccured { get; set; }
    }
}