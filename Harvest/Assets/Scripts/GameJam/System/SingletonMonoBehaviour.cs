using UnityEngine;

namespace GameJam.System
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null) 
                    return _instance;
                
                _instance = FindObjectOfType<T>();
                if (_instance != null) 
                    return _instance;
                    
                var obj = new GameObject
                {
                    name = typeof(T).Name
                };
                _instance = obj.AddComponent<T>();
                return _instance;
            }
        }

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}