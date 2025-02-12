using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GamePanelController : MonoBehaviour
{
    [SerializeField] private GameObject quizCardPrefab;
    [SerializeField] private Transform quizCardParent;
    
    private List<GameObject> quizCards = new List<GameObject>();
    
    private void Start()
    {
        InitQuizCards();
    }

    void Update()
    {
        MoveQuizCard();
    }
    
    private void InitQuizCards()
    {
        var firstCardObject = ObjectPool.Instance.GetObject();
        var secondCardObject = ObjectPool.Instance.GetObject();
        var thirdCardObject = ObjectPool.Instance.GetObject();
        
        quizCards.Add(firstCardObject);
        quizCards.Add(secondCardObject);
        quizCards.Add(thirdCardObject);
    }

    private void MoveQuizCard()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            GameObject firstCard = quizCards[0];
            quizCards.RemoveAt(0);
            quizCards.Add(firstCard);
                
            firstCard.SetActive(false);
            quizCards[0].SetActive(true);

            StartCoroutine(AnimateCardSwap(quizCards[0]));
        }
    }
    
    private IEnumerator AnimateCardSwap(GameObject card)
    {
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;
        float duration = 0.3f;
        float elapsedTime = 0f;

        card.transform.localScale = startScale;
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            card.transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / duration);
            yield return null;
        }
        
        card.transform.localScale = endScale;
    }
}
