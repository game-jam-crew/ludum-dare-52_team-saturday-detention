using System.Collections;
using GameJam.System.Data;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameJam
{
    public class Fruit : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] bool isOnTree;
        [SerializeField] bool isFalling;
        [SerializeField] bool isOnGround;
        
        [SerializeField] bool isBad;
        
        [SerializeField] float fallSpeed = 450.0f;
        [SerializeField] float fallDistance = 350.0f;
        
        [SerializeField] int tempTestingScore = 100;
        
        Vector2 startingPosition;

        void Start()
        {
            isOnTree = true;
            isFalling = false;
            isOnGround = false;
            
            isBad = false;
            
            startingPosition = transform.position;
        }
        
        void Update()
        {
            // TODO: if !isOnTree, start fall coroutine.
            if(!isOnTree && !isFalling && !isOnGround)
            {
                StartCoroutine(fall());
            }
        }
        
        IEnumerator fall()
        {
            isFalling = true;
            var distanceTraveled = 0.0f;

            while(distanceTraveled < fallDistance)
            {
                distanceTraveled += fallSpeed * Time.deltaTime;
                Debug.Log($"Fall distance this frame = { distanceTraveled }");
                
                transform.position = new Vector2(startingPosition.x, startingPosition.y - distanceTraveled);
                
                yield return null;
            }
            
            Debug.Log("Done, the fruit is in the dirt.");
            isFalling = false;
            isOnGround = true;
        }
            
        
        public void OnPointerClick(PointerEventData eventData)
        {
            Destroy(gameObject);
            // TODO: GetPoints
            
            // TEMP: exploring concept for UI rendering
            GameDataStore.Instance.GainPoints(tempTestingScore);
            
            // OnTree = full points
            // OnGround = half points
            // isBadFruit = 0 points
            
            // TODO: TreeHealthUpdate
        }
    }
}
