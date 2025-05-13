namespace Work01
{
    public interface IWorkController
    {
        void OnOkClick(string name);
        void OnSaveClick(string name, string text);
        void RegisterNoteGroup(INoteGroup noteGroup);
        void RegisterInputGroup(IInputGroup inputGroup);
    }
}