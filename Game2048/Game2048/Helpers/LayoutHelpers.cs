﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Game2048.Helpers
{
    static class LayoutHelpers
    {
        public static readonly BindableProperty IsItemsHostProperty =
            BindableProperty.CreateAttached("IsItemsHost", typeof(bool), typeof(Layout<View>), false);

        public static bool GetIsItemsHost(Layout<View> layout)
        {
            return (bool)layout.GetValue(IsItemsHostProperty);
        }

        public static void SetIsItemsHost(Layout<View> layout, bool value)
        {
            layout.SetValue(IsItemsHostProperty, value);
        }
    }
}
