using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject Prefab;
    [SerializeField] int poolSize;
    [SerializeField] private RectTransform parent;

    private Queue<GameObject> _pool;
    private static ObjectPool _instance;

    public static ObjectPool Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        _instance = this;
        _pool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            CreateNewObject();
        }
    }

    
    /// <summary>
    /// Object Pool에 새로운 오브젝트 생성 메서드
    /// </summary>
    private void CreateNewObject()
    {
        GameObject newObject = Instantiate(Prefab, parent);
        newObject.SetActive(false);
        _pool.Enqueue(newObject);
    }
    
    // 오브젝트 생성 -> 오브젝트 비활성화 -> 풀에 생성한 오브젝트 넣기
    
    /// <summary>
    /// 오브젝트 풀에 있는 오브젝트 반환 메서드
    /// </summary>
    /// <returns></returns>
    public GameObject GetObject()
    {
        if(_pool.Count == 0) CreateNewObject();
        
        GameObject dequeueObject = _pool.Dequeue();
        dequeueObject.SetActive(true);
        return dequeueObject;
    }
    
    // 풀에 오브젝트가 없으면 오브젝트를 생성 -> 풀에 있는 오브젝트를 dequeueObject에 넣기 -> 빼낸 오브젝트 활성화 -> 빼낸 오브젝트 반환

    /// <summary>
    /// 사용한 오브젝트를 오브젝트 풀로 되돌려 주는 메서드
    /// </summary>
    /// <param name="returnObj"></param>
    public void ReturnObject(GameObject returnObj)
    {
        returnObj.SetActive(false);
        _pool.Enqueue(returnObj);
    }
    
    // 
}