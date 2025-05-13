using System.Collections.Generic;

namespace Work01
{
    public class WorkController:IWorkController
    {
        private Dictionary<string,string> _noteData = new Dictionary<string, string>();
        
        private  IInputGroup _inputGroup;
        
        private INoteGroup _noteGroup;


        private void SaveData(string name, string text)
        {
            if (_noteData.ContainsKey(name))
            {
                _noteData[name] = text;
            }
            else
            {
                _noteData.Add(name, text);
            }
        }

        private string GetData(string name)
        {
            return _noteData.ContainsKey(name) ? _noteData[name] : string.Empty;
        } 

        public void OnOkClick(string name)
        {
            _inputGroup.Hide();
            _noteGroup.Show(name,GetData(name));
        }

        public void OnSaveClick(string name,string text)
        {
            SaveData(name,text);
            _noteGroup.Hide();
            _inputGroup.Show();
        }

        public void RegisterNoteGroup(INoteGroup noteGroup)
        {
            _noteGroup = noteGroup;
        }

        public void RegisterInputGroup(IInputGroup inputGroup)
        {
            _inputGroup = inputGroup;
        }
    }

}