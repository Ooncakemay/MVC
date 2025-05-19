using UnityEngine;
using UnityEngine.Serialization;

namespace Work02
{
    public class EnterPoint:MonoBehaviour
    {
        [SerializeField] private InputGroup inputGroup;
        [SerializeField] private FortuneTellingGroup fortuneTellingGroup;
        
        private IWorkController _workController = new WorkController();
        private void Start()
        {
            inputGroup.Construct(_workController);
            fortuneTellingGroup.Construct(_workController);
        }

        
        
    }
}