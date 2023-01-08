using UnityEngine;

namespace GameJam.UI
{
    public class UICursor : MonoBehaviour
    {
        void Start()
        {
            Cursor.visible = false;
        }
        
        void Update()
        {
            var cursor = Input.mousePosition;
            transform.position = cursor;
        }
    }
}
