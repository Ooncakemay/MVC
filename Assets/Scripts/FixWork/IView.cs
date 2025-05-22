namespace FixWork
{
    public interface IView
    {
        public void ShowInput(bool show);
        public void ShowNext(bool show);
       
        public string GetInput();
        public string GetNextInput();
        public void Display(string message);


    }
}