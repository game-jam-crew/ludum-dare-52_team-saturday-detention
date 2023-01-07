using System.Collections;
using GameJam.System.Data;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameJam
{
    public enum FruitLifeState
    {
        OnTree,
        Falling,
        OnGround,
        Rotten,
    }
    
    public class Fruit : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] float timeOnTree = 0.0f;
        [SerializeField] float fallSpeed = 450.0f;
        [SerializeField] float fallDistance = 350.0f;
        [SerializeField] int tempTestingScore = 100;
        [SerializeField] Sprite rottenSprite;
        
        SpriteRenderer _spriteRenderer;
        FruitLifeState _fruitLifeState;
        Vector2 _startingPosition;

        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _startingPosition = transform.position;
            _fruitLifeState = FruitLifeState.OnTree;
            Debug.Log(_fruitLifeState);

            var randomFallDelay = Random.Range(timeOnTree, timeOnTree + 5.0f);
            StartCoroutine(waitBeforeFall(randomFallDelay));
        }

        IEnumerator waitBeforeFall(float delaySeconds)
        {
            yield return new WaitForSeconds(delaySeconds);

            _fruitLifeState = FruitLifeState.Falling;
            Debug.Log(_fruitLifeState);
            StartCoroutine(fall());
        }
        
        IEnumerator fall()
        {
            var distanceTraveled = 0.0f;

            while(distanceTraveled < fallDistance)
            {
                distanceTraveled += fallSpeed * Time.deltaTime;
                transform.position = new Vector2(_startingPosition.x, _startingPosition.y - distanceTraveled);
                
                yield return null;
            }
            
            _fruitLifeState = FruitLifeState.OnGround;
            Debug.Log(_fruitLifeState);
            
            StartCoroutine(delayedRot());
        }

        IEnumerator delayedRot()
        {
            yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
            
            _fruitLifeState = FruitLifeState.Rotten;
            _spriteRenderer.sprite = rottenSprite;
            Debug.Log(_fruitLifeState);
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
