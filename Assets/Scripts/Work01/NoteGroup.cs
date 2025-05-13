

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Work01
{
    public class NoteGroup:MonoBehaviour,INoteGroup
    {
        
        [SerializeField] Button button;
        [SerializeField] TMP_InputField textMeshProUGUI;
        private string _name;
        
        
        public void Construct(IWorkController workController)
        {
            workController.RegisterNoteGroup(this);
            button.onClick.AddListener(() => workController.OnSaveClick(_name,textMeshProUGUI.text));
        }

        public void Show(string name, string note)
        {
            _name = name;
            textMeshProUGUI.text = note;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
    
   
        
    }
}