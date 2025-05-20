using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FixWork
{
    public class View : MonoBehaviour, IView
    {
    
        [SerializeField] private Button inputButton;
        [SerializeField] private TMP_InputField inputText;
        [SerializeField] private Button noteButton;
        [SerializeField] private TMP_InputField noteText;
        [SerializeField] private GameObject inputPanel;
        [SerializeField] private GameObject notePanel;
        [SerializeField] private ControllerType  controllerType; 
        
        private IController controller;
        
        public void Start()
        {
            if (controllerType == ControllerType.Note)
            {
                controller = new Controller(this);
            }
            else
            {
                controller = new DivinationController(this);
            }
           
            noteButton.onClick.AddListener(controller.OnNoteClick);
            inputButton.onClick.AddListener(controller.OnOkClick);
        }

        public string GetName()
        {
            return inputText.text;
        }

        public void Display(string message)
        {
            noteText.text = message;
        }

        public string GetText()
        {
            return noteText.text;
        }

        public void NoteShow()
        {
            notePanel.SetActive(true);
        }

        public void NoteHide()
        {
            
            notePanel.SetActive(false);
        }

        public void InputHide()
        {
            inputPanel.SetActive(false);
        }

        public void InputShow()
        {
            inputPanel.SetActive(true);
        }
    }
}