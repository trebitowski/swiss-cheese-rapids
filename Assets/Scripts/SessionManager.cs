using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class SessionManager : MonoBehaviour
{
    public Leaderboard leaderboard;
    public static SessionManager instance;
    
    void Start()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        StartCoroutine(SetupRoutine());
    }

    public void SetPlayerName(string name) {
        LootLockerSDKManager.SetPlayerName(name, (response) => {
            if (response.success)
            {   
                StartCoroutine(SubmitScoresAndRefresh());
            } else {
                Debug.Log("error setting player name" + response);
            } 
        });
    }

    public IEnumerator SubmitScoresAndRefresh() {
        Leaderboard leaderBoard = FindObjectOfType<Leaderboard>();
        int score = PlayerPrefs.GetInt("totalScore");
        int cheese = PlayerPrefs.GetInt("cheeseScore");
        int distance = PlayerPrefs.GetInt("distanceScore");
        yield return leaderBoard.SubmitScoreRoutine(score, cheese, distance);
        Leaderboard.RefreshLeaderboard();
    }

    public IEnumerator SetupRoutine() {
        yield return LoginRoutine();
        yield return leaderboard.FetchTopHighscoresRoutine();
        yield return leaderboard.FetchUserHighscoreRoutine();
    }

    IEnumerator LoginRoutine() {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                
                Debug.Log("successfully started LootLocker session");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            } else {
                Debug.Log("error starting LootLocker session" + response);
                done = true;
            }          
        });
        yield return new WaitWhile(() => done == false);
    }
}