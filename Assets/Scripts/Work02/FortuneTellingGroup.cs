using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Work02
{
    public class FortuneTellingGroup:MonoBehaviour,IFortuneTellingGroup
    {
        
        [SerializeField] Button button;
        [SerializeField] TMP_InputField textMeshProUGUI;

        
        
        public void Construct(IWorkController workController)
        {
            workController.RegisterNoteGroup(this);
            button.onClick.AddListener(workController.OnNextOneClick);
        }

        public void ShowNextMessage(string data)
        {
            textMeshProUGUI.text = data;
        }

        public void Show(string data)
        {
            textMeshProUGUI.text = data;
            gameObject.SetActive(true);
        }
    }
}