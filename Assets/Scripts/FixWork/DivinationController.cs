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
            view.Display(model.GetData(""));
            view.ShowInput(false);
            view.ShowNext(true);
            
        }

        public void OnNextClick()
        {
            view.Display(model.GetData(""));
            view.ShowInput(false);
            view.ShowNext(true);
            
        }

      
        
     
    }

}