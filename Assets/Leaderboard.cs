using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class Leaderboard : MonoBehaviour
{   
    public TextMeshProUGUI playerNames;
    public TextMeshProUGUI playerScores;
    public string leaderboardKey = "globalHighscore";
    public int leaderboardId = 9842;
    public static Leaderboard instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator SubmitScoreRoutine(int scoreToUpload) {
        bool done = false;
        Debug.Log("123");
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, leaderboardKey, (response) => {
            if (response.success) {
                Debug.Log("score submitted");
                done = true;
            } else {
                Debug.Log("error submitting score" + response);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    public static void RefreshLeaderboard() {
        instance.StartCoroutine(instance.FetchTopHighscoresRoutine());
        instance.StartCoroutine(instance.FetchUserHighscoreRoutine());
    }
    public IEnumerator FetchTopHighscoresRoutine() {
        bool done = false;
        LootLockerSDKManager.GetScoreList(leaderboardKey, 10, 0, (response) => {
            if (response.success) {
                Debug.Log("leaderboard received");
                string tempPlayerNames = "name\n";
                string tempPlayerScores = "score\n";

                LootLockerLeaderboardMember[] members = response.items;

                foreach (LootLockerLeaderboardMember member in members) {
                    tempPlayerNames += member.rank + ". ";
                    if (member.player.name != "") {
                        tempPlayerNames += member.player.name;
                    } else {
                        tempPlayerNames += member.player.id;
                    }
                    tempPlayerScores += member.score + "\n";
                    tempPlayerNames += "\n";
                }

                TextMeshProUGUI[] texts = FindObjectsOfType<TextMeshProUGUI>();
                for (int i = 0; i < texts.Length; i++) {
                    if (texts[i].name == "leaderboardNames") {
                        texts[i].text = tempPlayerNames;
                    } else if (texts[i].name == "leaderboardScores") {
                        texts[i].text = tempPlayerScores;
                    }
                }             
                done = true;
            } else {
                Debug.Log("error fetching leaderboard" + response);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    public IEnumerator FetchUserHighscoreRoutine() {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.GetMemberRank(leaderboardKey, playerID, (response) => {
            if (response.success) {
                Debug.Log("leaderboard received");
                    if (response.rank != 0) {


                string tempPlayerName = "";
                string tempPlayerScore = "";

                    tempPlayerName += response.rank + ". ";
                    if (response.player != null && response.player.name != "") {
                        tempPlayerName += response.player.name;
                    } else {
                        tempPlayerName += playerID;
                    }
                    tempPlayerScore += response.score;

                TextMeshProUGUI[] texts = FindObjectsOfType<TextMeshProUGUI>();
                for (int i = 0; i < texts.Length; i++) {
                    if (texts[i].name == "userLeaderboardName") {
                        texts[i].text = tempPlayerName;
                    } else if (texts[i].name == "userLeaderboardScore") {
                        texts[i].text = tempPlayerScore;
                    }
                }  
                                    }       
                done = true;
            } else {
                Debug.Log("error fetching leaderboard" + response);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}
