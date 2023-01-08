using GameJam.System;
using UnityEngine;

namespace GameJam.UI
{
    public class CursorManager : SingletonMonoBehaviour<CursorManager>
    {
        Camera _camera;
        
        void Start()
        {
            _camera = Camera.main;
            Cursor.visible = false;
        }
        
        void Update()
        {
            var cursor = _camera.ScreenToWorldPoint(Input.mousePosition);
            cursor.z = 0;
            transform.position = cursor;
        }
    }
}
