using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GamePanelController : MonoBehaviour
{
    [SerializeField] private GameObject quizCardPrefab;
    [SerializeField] private Transform quizCardParent;
    
    private readonly List<GameObject> _quizCards = new List<GameObject>();
    private List<QuizData> _quizDataList;
    
    private GameObject _firstCardObject;
    private GameObject _secondCardObject;
    private GameObject _thirdCardObject;
    private void Start()
    {
        _quizDataList = QuizDataController.LoadQuizData(0);
        
        InitQuizCards();
    }

    void Update()
    {
        SetQuizCardPosition(_firstCardObject, 0);
        SetQuizCardPosition(_secondCardObject, 1);
    }
    
    private void InitQuizCards()
    {
        _firstCardObject = ObjectPool.Instance.GetObject();
        _firstCardObject.GetComponent<QuizCardController>().SetQuiz(_quizDataList[0], OnCompletedQuiz);
        _secondCardObject = ObjectPool.Instance.GetObject();
        _firstCardObject.GetComponent<QuizCardController>().SetQuiz(_quizDataList[1], OnCompletedQuiz);
        _thirdCardObject = ObjectPool.Instance.GetObject();
        _firstCardObject.GetComponent<QuizCardController>().SetQuiz(_quizDataList[2], OnCompletedQuiz);
        
        _quizCards.Add(_firstCardObject);
        _quizCards.Add(_secondCardObject);
        _quizCards.Add(_thirdCardObject);
    }

    private void OnCompletedQuiz(int cardIndex)
    {
        
    }


    private void SetQuizCardPosition(GameObject quizCardObject, int index)
    {
        var quizCardTransform = quizCardObject.GetComponent<RectTransform>();
        if (index == 0)
        {
            quizCardTransform.anchoredPosition = new Vector2(0, 0);
            quizCardTransform.localScale = Vector3.one;
            quizCardTransform.SetAsLastSibling();
        }
        else if (index == 1)
        {
            quizCardTransform.anchoredPosition = new Vector2(0, 160);
            quizCardTransform.localScale = Vector3.one * 0.9f;
        }
    }

    public void ChangeQuizCard()
    {
        var temp = _firstCardObject;
        _firstCardObject = _secondCardObject;
        _secondCardObject = _thirdCardObject;
        _thirdCardObject = temp;
        SetQuizCardPosition(_firstCardObject, 0);
        SetQuizCardPosition(_secondCardObject, 1);
    }
    
    private void MoveQuizCard()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            GameObject firstCard = _quizCards[0];
            _quizCards.RemoveAt(0);
            _quizCards.Add(firstCard);
                
            firstCard.SetActive(false);
            _quizCards[0].SetActive(true);

            StartCoroutine(AnimateCardSwap(_quizCards[0]));
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
