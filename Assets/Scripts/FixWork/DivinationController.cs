namespace FixWork
{
    public class DivinationController:IController
    {
        
        private IView view;
        
        private IModel model;
        
        public DivinationController(IView view)
        {
            this.view = view;
            model = new DivinationModel();
        }

     

        public void OnOkClick()
        {
            var name = view.GetName();
            var text = model.GetData(name);
            view.InputHide();
            view.NoteShow();
            view.Display(text);

        }
        
        public void OnNoteClick()
        {
            var text = model.GetData("");
            view.Display(text);
     
        }
        
     
    }

}