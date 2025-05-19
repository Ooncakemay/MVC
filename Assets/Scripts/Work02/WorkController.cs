using System.Collections.Generic;


namespace Work02
{
    public class WorkController:IWorkController
    {

        private List<string> _messageData = new()
        {
            "Cats may dislike you today, but dogs will understand.",
            "Step out with your left foot first — luck will visit for 5 minutes.",
            "You almost met the god of fortune, but you sneezed.",
            "Today is a good day to reflect on life — just don't take it too seriously.",
            "If you eat instant noodles without an egg, your fortune will be average.",
            "You'll feel an urge to buy bubble tea, but forget what you wanted."
        };

      
        
        private  IInputGroup _inputGroup;
        
        private IFortuneTellingGroup fortuneTellingGroup;



        private string GetData()
        {
            var random = new System.Random();
            var randomIndex = random.Next(0, _messageData.Count);
            var randomMessage = _messageData[randomIndex];
            return randomMessage;
        } 

        public void OnOkClick()
        {
            _inputGroup.Hide();
            fortuneTellingGroup.Show(GetData());
        }

        public void OnNextOneClick()
        {
            fortuneTellingGroup.ShowNextMessage(GetData());
 
        }

        public void RegisterNoteGroup(IFortuneTellingGroup fortuneTellingGroup)
        {
            this.fortuneTellingGroup = fortuneTellingGroup;
        }

        public void RegisterInputGroup(IInputGroup inputGroup)
        {
            _inputGroup = inputGroup;
        }
    }

}