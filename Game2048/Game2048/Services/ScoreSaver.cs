using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Game2048.Services
{
    public class ScoreSaver : IScoreSaver
    {
        public void Save(string key, int value)
        {
            Application.Current.Properties[key] = value;
        }
    }
}
