using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FixWork
{
    public class View : MonoBehaviour, IView
    {
        

        [SerializeField] private Button inputButton;
        [SerializeField] private TMP_InputField input;
        [SerializeField] private Button nextButton; 
        [SerializeField] private TMP_InputField nextInput;
        [SerializeField] private GameObject inputGroup;
        [SerializeField] private GameObject nextGroup;
        [SerializeField] private ControllerType  controllerType;
        private IController _controller; 

        public void Start()
        {
            if (controllerType == ControllerType.Divination)
            {
                _controller = new DivinationController(this);
            }
            else
            {
                _controller = new Controller(this);
            }

            inputButton.onClick.AddListener(() => _controller.OnOkClick(input.text));
            nextButton.onClick.AddListener(() => _controller.OnNextClick(input.text,nextInput.text));

        }

        public void ShowInput(bool show)
        {
            if (show)
            {
                inputGroup.SetActive(true);
                nextGroup.SetActive(false);
                
            }
            else
            {
                inputGroup.SetActive(false);
                nextGroup.SetActive(true);
            }
           
        }

        public void Display(string message)
        {
            nextInput.text = message;
            
        }

      
    }
}