using System.Collections.Generic;

namespace FixWork
{
    public class Model:IModel
    {
        private Dictionary<string,string> data = new Dictionary<string, string>();


        public string GetData(string name)
        {
            return data.TryGetValue(name, out var value) ? value : string.Empty;
        }

        public void SaveData(string name, string text)
        {
            data[name] = text;
        }
    }
}