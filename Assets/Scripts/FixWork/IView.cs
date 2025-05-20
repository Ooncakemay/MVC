namespace FixWork
{
    public interface IView
    {
        public  void NoteShow();
        public void NoteHide();
        public  void InputHide();
        public void InputShow();
        public string GetName();
        public void Display(string message);

        public string GetText();
    }
}