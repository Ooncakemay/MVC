
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


        public void OnOkClick(string name)
        {
            view.Display(model.GetData(name));
            view.ShowInput(false);
        }

        public void OnNextClick(string name,string note)
        {
            model.SaveData(name,note);
            view.ShowInput(true);
          
        }
    }

}