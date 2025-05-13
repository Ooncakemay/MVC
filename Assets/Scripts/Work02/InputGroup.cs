using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Work02
{
    public class InputGroup:MonoBehaviour,IInputGroup
    {
        
        [SerializeField] Button button;
        [SerializeField] TMP_InputField  textMeshProUGUI;
        
        
        public void Construct(IWorkController workController)
        {
            workController.RegisterInputGroup(this);
            button.onClick.AddListener(workController.OnOkClick);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
      
   
        
    }
}