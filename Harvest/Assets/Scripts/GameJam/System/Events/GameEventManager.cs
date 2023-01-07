using System.Collections.Generic;

namespace GameJam.System.Events
{
    public class GameEventManager : SingletonMonoBehaviour<GameEventManager>
    {
        static readonly HashSet<GameEvent> _listenedEvents = new HashSet<GameEvent>();
        
        public void Register(GameEvent gameEvent)
        {
            _listenedEvents.Add(gameEvent);
        }
        
        public void Deregister(GameEvent gameEvent) 
        {
            _listenedEvents.Remove(gameEvent);
        }
        
        public void RaiseEvent(string eventName)
        {
            foreach(var gameEvent in _listenedEvents)
            {
                if(gameEvent.name != eventName)
                    continue;
                
                gameEvent.Invoke();
            }
        }
    }
}
