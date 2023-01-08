using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameJam.UI
{
    public class HoverOverChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public TMP_Text _text;
        
        public void Start()
        {
            _text = GetComponent<TMP_Text>();
        }
 
        public void OnPointerEnter(PointerEventData eventData)
        {
            _text.color = Color.white;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _text.color = Color.black;
        }
    }
}
