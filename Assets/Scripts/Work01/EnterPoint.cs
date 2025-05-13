using System;
using UnityEngine;

namespace Work01
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