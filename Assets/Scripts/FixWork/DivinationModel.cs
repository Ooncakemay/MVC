using System;
using System.Collections.Generic;

namespace FixWork
{
    public class DivinationModel:IModel
    {
        private List<string> data = new()
        {
            "Cats may dislike you today, but dogs will understand.",
            "Step out with your left foot first — luck will visit for 5 minutes.",
            "You almost met the god of fortune, but you sneezed.",
            "Today is a good day to reflect on life — just don't take it too seriously.",
            "If you eat instant noodles without an egg, your fortune will be average.",
            "You'll feel an urge to buy bubble tea, but forget what you wanted."
        };

        private Random random = new();

       
        public string GetData(string name)
        {
            var randomIndex = random.Next(0, data.Count);
            var randomMessage = data[randomIndex];
            return randomMessage;
        }

        public void SaveData(string name, string text)
        {
            
            
            
        }
    }
}