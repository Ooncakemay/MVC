namespace Work02
{
    public interface IWorkController
    {
        void OnOkClick();
        void OnNextOneClick();
        void RegisterNoteGroup(IFortuneTellingGroup fortuneTellingGroup);
        void RegisterInputGroup(IInputGroup inputGroup);
    }
}