using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Game2048.Services
{
    public class ScoreReader : IScoreReader
    {
        public int Read(string key)
        {
            return (int)Application.Current.Properties[key];
        }
    }
}
