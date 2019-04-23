using Game2048.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Game2048.Services
{
    public interface IDragReader
    {
        MoveDirection GetDirection(PanUpdatedEventArgs panUpdatedEventArgs);
    }
}