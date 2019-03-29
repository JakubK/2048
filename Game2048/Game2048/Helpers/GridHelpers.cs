using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Game2048.Helpers
{
    public class GridHelpers
    {
        //Rows 
        public static readonly BindableProperty RowCountProperty = 
            BindableProperty.CreateAttached(
                "RowCount"
                , typeof(int)
                , typeof(GridHelpers)
                ,-1
                , propertyChanged: (bindable,oldValue, newValue) => RowCountChanged(bindable, (int)oldValue, (int)newValue));


        public static int GetRowCount(BindableObject obj)
        {
            return (int)obj.GetValue(RowCountProperty);
        }

        public static void SetRowCount(BindableObject obj,int value)
        {
            obj.SetValue(RowCountProperty, value);
        }

        public static void RowCountChanged(BindableObject obj, int oldValue, int newValue)
        {
            if (!(obj is Grid) || newValue < 0)
                return;

            Grid grid = (Grid)obj;
            grid.RowDefinitions.Clear();

            for (int i = 0; i < newValue; i++)
                grid.RowDefinitions.Add(
                    new RowDefinition() { Height = GridLength.Star });
        }

        //Columns

        public static readonly BindableProperty ColumnCountProperty =
            BindableProperty.CreateAttached(
                "ColumnCount"
                , typeof(int)
                , typeof(GridHelpers)
                , -1
                , propertyChanged: (bindable, oldValue, newValue) => ColumnCountChanged(bindable, (int)oldValue, (int)newValue));


        public static int GetColumnCount(BindableObject obj)
        {
            return (int)obj.GetValue(RowCountProperty);
        }

        public static void SetColumnCount(BindableObject obj, int value)
        {
            obj.SetValue(RowCountProperty, value);
        }

        public static void ColumnCountChanged(BindableObject obj, int oldValue, int newValue)
        {
            if (!(obj is Grid) || newValue < 0)
                return;

            Grid grid = (Grid)obj;
            grid.ColumnDefinitions.Clear();

            for (int i = 0; i < newValue; i++)
                grid.ColumnDefinitions.Add(
                    new ColumnDefinition() { Width = GridLength.Star });
        }
    }
}