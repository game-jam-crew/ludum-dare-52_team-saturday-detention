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
        [SerializeField] Sprite rottenSprite;
        [SerializeField] float timeOnTree = 0.0f;
        [SerializeField] float fallSpeed = 450.0f;
        [SerializeField] float fallDistance = 350.0f;
        [SerializeField] int baseScore = 100;
        
        SpriteRenderer _spriteRenderer;
        FruitLifeState _fruitLifeState;
        Vector2 _startingPosition;

        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _startingPosition = transform.position;
            _fruitLifeState = FruitLifeState.OnTree;

            var randomFallDelay = Random.Range(timeOnTree, timeOnTree + 5.0f);
            StartCoroutine(waitBeforeFall(randomFallDelay));
        }

        IEnumerator waitBeforeFall(float delaySeconds)
        {
            yield return new WaitForSeconds(delaySeconds);

            _fruitLifeState = FruitLifeState.Falling;
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
            
            StartCoroutine(delayedRot());
        }

        IEnumerator delayedRot()
        {
            yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
            
            _fruitLifeState = FruitLifeState.Rotten;
            _spriteRenderer.sprite = rottenSprite;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            var score = deriveScore();
            GameDataStore.Instance.GainPoints(score);

            // TODO: TreeHealthUpdate -> fire tree health update event for tree to listen to it.
            
            Destroy(gameObject);
        }
        
        int deriveScore()
        {
            var score = baseScore;

            if(_fruitLifeState == FruitLifeState.OnGround)
                score = (int) Mathf.Floor(baseScore / 2.0f);
            if(_fruitLifeState == FruitLifeState.Rotten)
                score = 0;
            
            return score;
        }
    }
}
