using UnityEngine;

namespace Work02
{
    public class EnterPoint:MonoBehaviour
    {
        [SerializeField] InputGroup InputGroup;
        [SerializeField] NoteGroup NoteGroup;
        
        private IWorkController _workController = new WorkController();
        private void Start()
        {
            InputGroup.Construct(_workController);
            NoteGroup.Construct(_workController);
        }

        
        
    }
}