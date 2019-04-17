using System;
using System.Collections.Generic;
using System.Text;
using Game2048.Enums;
using Xamarin.Forms;

namespace Game2048.Services
{
    public class DragReader : IDragReader
    {
        private double PanX;
        private double PanY;

        public DragReader()
        {
            PanX = 0;
            PanY = 0;
        }

        public MoveDirection GetDirection(PanUpdatedEventArgs panUpdatedEventArgs)
        {
            if (panUpdatedEventArgs.StatusType == GestureStatus.Completed)
            {
                if (Math.Sqrt(PanX * PanX + PanY * PanY) > 100)
                {
                    if (PanX > 0)
                    {
                        if (Math.Abs(PanX) > Math.Abs(PanY))
                        {
                            System.Diagnostics.Debug.WriteLine("Drag went right");
                            return MoveDirection.Right;
                        }
                    }
                    else
                    {
                        if (Math.Abs(PanX) > Math.Abs(PanY))
                        {
                            System.Diagnostics.Debug.WriteLine("Drag went left");
                            return MoveDirection.Left;
                        }
                    }
                    if (PanY > 0)
                    {
                        if (Math.Abs(PanX) < Math.Abs(PanY))
                        {
                            System.Diagnostics.Debug.WriteLine("Drag went down");
                            return MoveDirection.Bottom;
                        }
                    }
                    else
                    {
                        if (Math.Abs(PanX) < Math.Abs(PanY))
                        {
                            System.Diagnostics.Debug.WriteLine("Drag went up");
                            return MoveDirection.Top;
                        }
                    }
                }
            }
            else
            {
                PanX = panUpdatedEventArgs.TotalX;
                PanY = panUpdatedEventArgs.TotalY;

                return MoveDirection.None;
            }

            return MoveDirection.None;
        }
    }
}
