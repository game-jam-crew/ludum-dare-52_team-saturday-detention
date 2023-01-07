using UnityEngine;
using UnityEngine.EventSystems;

namespace GameJam
{
    public class Fruit : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] bool isOnTree;
        [SerializeField] bool isBad;

        void Start()
        {
            isOnTree = true;
            isBad = false;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            Destroy(gameObject);
            // TODO: GetPoints
            // OnTree = full points
            // OnGround = half points
            // isBadFruit = 0 points
            
            // TODO: TreeHealthUpdate
        }
    }
}
