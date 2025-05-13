namespace Work02
{
    public interface IWorkController
    {
        void OnOkClick();
        void OnNextOneClick();
        void RegisterNoteGroup(INoteGroup noteGroup);
        void RegisterInputGroup(IInputGroup inputGroup);
    }
}