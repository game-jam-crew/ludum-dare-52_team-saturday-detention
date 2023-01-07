using System.Collections.Generic;
using UnityEngine;

namespace GameJam.System.Events
{
    [CreateAssetMenu(menuName = "GameJam/Game Event")]
    public class GameEvent : ScriptableObject
    {
        readonly HashSet<GameEventListener> _gameEventListeners = new HashSet<GameEventListener>();
        
        public void Register(GameEventListener gameEventListener)
        {
            _gameEventListeners.Add(gameEventListener);
            GameEventManager.Instance.Register(this);
        }
        
        public void Deregister(GameEventListener gameEventListener) 
        {
            _gameEventListeners.Remove(gameEventListener);
            
            if(_gameEventListeners.Count == 0)
                GameEventManager.Instance.Deregister(this);
        }
        
        [ContextMenu("Invoke")]
        public void Invoke()
        {
            foreach(var listener in _gameEventListeners)
                listener.RaiseEvent();
        }
    }
}
