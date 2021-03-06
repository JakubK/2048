﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Game2048.Enums;
using Game2048.ViewModels;
using Splat;

namespace Game2048.Services
{
    public class SquareTranslator : ISquareTranslator
    {
        private IBoardContainer boardContainer;

        private ObservableCollection<SquareViewModel> Squares
        {
            get { return boardContainer.Squares; }
        }

        private int Width
        {
            get { return boardContainer.Width; }
        }

        private int Height
        {
            get { return boardContainer.Height; }
        }

        public bool ChangeOccured { get; set; }
        private MoveDirection lastMove;

        public SquareTranslator(IBoardContainer board = null)
        {
            this.boardContainer = board ?? Locator.Current.GetService<IBoardContainer>();
            lastMove = MoveDirection.None;
            ChangeOccured = true;
        }

        public void TranslateHorizontally(MoveDirection direction)
        {
            if(!ChangeOccured)
            {
                if((direction == MoveDirection.Right && lastMove == MoveDirection.Right) || (direction == MoveDirection.Left && lastMove == MoveDirection.Left))
                {
                    return;
                }
            }

            ChangeOccured = false;
            var rows = Squares.GroupBy(r => r.Y).ToList();
            foreach (var row in rows)
            {
                if (direction == MoveDirection.Left)
                    PushRow(row.OrderBy(x => x.X).ToList(), direction);
                else
                    PushRow(row.OrderByDescending(x => x.X).ToList(), direction);
            }
        }

        private void PushRow(List<SquareViewModel> row, MoveDirection direction)
        {
            int pos = 0;
            int desiredX = direction == MoveDirection.Right ? (Width - 1) : 0;
            if(desiredX != row[0].XRequest)
            {
                row[0].XRequest = desiredX;
                ChangeOccured = true;
            }

            for (int i = 1; i < row.Count; i++)
            {
                if (row[i].Value == row[pos].Value && !row[pos].CreatedInThisTurn)
                {
                    row[pos].Value *= 2;
                    boardContainer.Score += row[pos].Value;

                    row[pos].CreatedInThisTurn = true;

                    Squares.ElementAt(Squares.IndexOf(row[i])).ToBeRemoved = true;
                    row[i].XRequest = direction == MoveDirection.Right ? (Width - 1) - pos : pos;
                    row.RemoveAt(i);

                    i--;

                    ChangeOccured = true;
                }
                else
                {
                    pos++;
                    desiredX = direction == MoveDirection.Right ? (Width - 1) - pos : pos;
                    if(desiredX != row[i].XRequest)
                    {
                        row[i].XRequest = desiredX;
                        ChangeOccured = true;
                    }
                }
            }

            foreach (SquareViewModel item in row)
            {
                item.CreatedInThisTurn = false;
            }

            lastMove = direction;
        }

        public void TranslateVertically(MoveDirection direction)
        {
            if (!ChangeOccured)
            {
                if ((direction == MoveDirection.Bottom && lastMove == MoveDirection.Bottom) || (direction == MoveDirection.Top && lastMove == MoveDirection.Top))
                {
                    return;
                }
            }

            ChangeOccured = false;
            var cols = Squares.GroupBy(r => r.X).ToList();
            foreach (var col in cols)
            {
                if (direction == MoveDirection.Top)
                    PushCol(col.OrderBy(x => x.Y).ToList(), direction);
                else
                    PushCol(col.OrderByDescending(x => x.Y).ToList(), direction);
            }
        }

        private void PushCol(List<SquareViewModel> col, MoveDirection direction)
        {
            int pos = 0;
            int desiredY = direction == MoveDirection.Bottom ? (Height - 1) : 0;
            if(desiredY != col[0].Y)
            {
                col[0].YRequest = desiredY;
                ChangeOccured = true;
            }

            for (int i = 1; i < col.Count; i++)
            {
                if (col[i].Value == col[pos].Value && !col[pos].CreatedInThisTurn)
                {
                    col[pos].Value *= 2;
                    boardContainer.Score += col[pos].Value;
                    col[pos].CreatedInThisTurn = true;

                    Squares.ElementAt(Squares.IndexOf(col[i])).ToBeRemoved = true;
                    col[i].YRequest = direction == MoveDirection.Bottom ? (Height - 1) - pos : pos;
                    col.RemoveAt(i);

                    i--;
                    ChangeOccured = true;
                }
                else
                {
                    pos++;
                    desiredY = direction == MoveDirection.Bottom ? (Height - 1) - pos : pos;
                    if(desiredY != col[i].YRequest)
                    {
                        col[i].YRequest = desiredY;
                        ChangeOccured = true;
                    }
                }
            }

            foreach (SquareViewModel item in col)
            {
                item.CreatedInThisTurn = false;
            }

            lastMove = direction;
        }
    }
}