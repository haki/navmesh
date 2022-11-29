using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGameOver = false;

    private InGameRanking _inGameRanking;
    private GameObject[] runners;
    private List<RankingSystem> _sortArray = new List<RankingSystem>();

    private void Awake()
    {
        instance = this;
        runners = GameObject.FindGameObjectsWithTag("Runner");
        _inGameRanking = FindObjectOfType<InGameRanking>();
    }

    private void Start()
    {
        for (var i = 0; i < runners.Length; i++)
        {
            _sortArray.Add(runners[i].GetComponent<RankingSystem>());
        }
    }

    private void Update()
    {
        CalculateRank();
    }

    private void CalculateRank()
    {
        _sortArray = _sortArray.OrderBy(x => x.distance).ToList();
        for (var i = 0; i < _sortArray.Count; i++)
        {
            _sortArray[i].rank = i + 1;
        }

        _inGameRanking.a = _sortArray[_sortArray.Count - 1].name;
        _inGameRanking.b = _sortArray[_sortArray.Count - 2].name;
        _inGameRanking.c = _sortArray[_sortArray.Count - 3].name;
        _inGameRanking.d = _sortArray[_sortArray.Count - 4].name;
        _inGameRanking.e = _sortArray[_sortArray.Count - 5].name;
    }
}
