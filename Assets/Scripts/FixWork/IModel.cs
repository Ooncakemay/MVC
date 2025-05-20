namespace FixWork
{
    public interface IModel
    {
        public string GetData(string name);
        public void SaveData(string name, string text);
    }
}