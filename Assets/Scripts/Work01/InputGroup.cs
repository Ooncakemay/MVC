

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Work01
{
    public class InputGroup:MonoBehaviour,IInputGroup
    {
        
        [SerializeField] Button button;
        [SerializeField] TMP_InputField  textMeshProUGUI;
        
        
        public void Construct(IWorkController workController)
        {
            workController.RegisterInputGroup(this);
            button.onClick.AddListener(() => workController.OnOkClick(textMeshProUGUI.text));
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        public  void Show()
        {
            gameObject.SetActive(true);
        }
   
        
    }
}