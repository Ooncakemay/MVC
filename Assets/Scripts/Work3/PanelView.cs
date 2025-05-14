using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Work3
{
    public class PanelView: MonoBehaviour,IPanelView
    {
        // background
        [SerializeField] private Sprite[] backgroundPictures;
        private const int backgroundTypeCount = 2;
        [SerializeField] private Image background;
        
        // mask
        [SerializeField] private GameObject mask;
        [SerializeField] private Text systemMsg;
        
        private Color transparent = new Color(1, 1, 1, 0);
        
        
        public void Construct( ICardGameController cardGameController)
        {
            cardGameController.RegisterPanelView(this);
        }

        void Start () 
        {
            Initialization();
        }
        
        private void Initialization()
        {
            
            AssignRandomBackground();
            systemMsg.color = transparent;
            mask.SetActive(false);
        }
        
        private void AssignRandomBackground()
        {
            var index = Random.Range(0, backgroundTypeCount);
            background.sprite = backgroundPictures[index];
        }
        
        public void Completed()
        {
            StartCoroutine(ShowGameCompleted());
        }
        
        private IEnumerator ShowGameCompleted()
        {
            mask.SetActive(true);
            systemMsg.text = "Congratulations!";
            float alpha = 0.05f;
            while(alpha < 1)
            {
                alpha += 0.05f;
                systemMsg.color = new Color(1, 1, 1, alpha);
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(3f);
            Debug.Log("遊戲結束");
        }

    }
}