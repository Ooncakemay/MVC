using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace FixWork
{
    public class View : MonoBehaviour, IView
    {
        

        [SerializeField] private Button inputButton;
        [SerializeField] private TMP_InputField input;
        [SerializeField] private Button nextButton; 
        [SerializeField] private TMP_InputField nextInput;
        [FormerlySerializedAs("input")] [SerializeField] private GameObject inputGroup;
        [FormerlySerializedAs("next")] [SerializeField] private GameObject nextGroup;
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

            inputButton.onClick.AddListener(() => _controller.OnOkClick());
            nextButton.onClick.AddListener(() => _controller.OnNextClick());


        }


        
        public void ShowNext(bool show)
        {
            nextGroup.SetActive(show);
        }

        public string GetInput()
        {
            return input.text;
        }

        public string GetNextInput()
        {
            return nextInput.text;
        }

        public void ShowInput(bool show)
        {
            inputGroup.SetActive(show);
        }

        public void Display(string message)
        {
            nextInput.text = message;
            
        }

      
    }
}