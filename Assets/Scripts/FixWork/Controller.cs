
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
        public void Init()
        {
            view.ShowNext(false);
        }

        public void OnOkClick()
        {
            var name = view.GetInput();
            view.Display(model.GetData(name));
            view.ShowInput(false);
            view.ShowNext(true);
        }

        public void OnNextClick()
        {
            var name = view.GetInput();
            var note = view.GetNextInput();
            model.SaveData(name,note);
            view.ShowInput(true);
            view.ShowNext(false);
          
        }
    }

}