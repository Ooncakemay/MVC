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
        

        public void OnOkClick(string name)
        {
            view.Display(model.GetData(""));
            view.ShowInput(false);
        }
        
        public void OnNextClick(string name,string text)
        {
            view.Display(model.GetData(""));
            
        }

      
        
     
    }

}