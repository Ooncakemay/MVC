
namespace FixWork
{
    public class Controller:IController
    {
        
        private IView view;
        
        private IModel model;
        
        public Controller(IView view)
        {
            this.view = view;
            model = new Model();
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
            var name = view.GetName();
            var text = view.GetText();
            model.SaveData(name,text);
            view.NoteHide();
            view.InputShow();
        }
        
      

        
     
    }

}