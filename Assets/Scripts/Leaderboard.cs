using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class Leaderboard : MonoBehaviour
{       
    public string leaderboardKey = "globalHighscore";
    public string cheeseLeaderboardKey = "cheeseHighscore";
    public string distanceLeaderboardKey = "distanceHighscore";
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

    public IEnumerator SubmitScoreRoutine(int scoreToUpload, int cheeseToUpload, int distanceToUpload) {
        bool done1 = false;
        bool done2 = false;
        bool done3 = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.GetPlayerName((response) =>
{
    if (response.success)
    {
        Debug.Log("Successfully retrieved player name: " + response.name);
        if(response.name != ""){
        LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, leaderboardKey, (response) => {
            if (response.success) {
                Debug.Log("score submitted");
                done1 = true;
            } else {
                Debug.Log("error submitting score" + response);
                done1 = true;
            }
        });
        LootLockerSDKManager.SubmitScore(playerID, cheeseToUpload, cheeseLeaderboardKey, (response) => {
            if (response.success) {
                Debug.Log("score submitted");
                done2 = true;
            } else {
                Debug.Log("error submitting score" + response);
                done2 = true;
            }
        });
        LootLockerSDKManager.SubmitScore(playerID, distanceToUpload, distanceLeaderboardKey, (response) => {
            if (response.success) {
                Debug.Log("score submitted");
                done3 = true;
            } else {
                Debug.Log("error submitting score" + response);
                done3 = true;
            }
        });
        }
    } else
    {
        Debug.Log("Error getting player name");
        done1 = true;
        done2 = true;
        done3 = true;
    }
});
        
        yield return new WaitWhile(() => done1 == false || done2 == false || done3 == false);
    }

    public static void RefreshLeaderboard() {
        instance.StartCoroutine(instance.FetchTopHighscoresRoutine());
        instance.StartCoroutine(instance.FetchUserHighscoreRoutine());
    }
    public IEnumerator FetchTopHighscoresRoutine() {
        bool done1 = false;
        bool done2 = false;
        bool done3 = false;
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

                TextMeshProUGUI[] texts = Resources.FindObjectsOfTypeAll(typeof(TextMeshProUGUI)) as TextMeshProUGUI[];
                for (int i = 0; i < texts.Length; i++) {
                    if (texts[i].name == "leaderboardNames") {
                        texts[i].text = tempPlayerNames;
                    } else if (texts[i].name == "leaderboardScores") {
                        texts[i].text = tempPlayerScores;
                    }
                }             
                done1 = true;
            } else {
                Debug.Log("error fetching leaderboard" + response);
                done1 = true;
            }
        });
        LootLockerSDKManager.GetScoreList(cheeseLeaderboardKey, 10, 0, (response) => {
            if (response.success) {
                Debug.Log("leaderboard received");
                string tempPlayerNames = "name\n";
                string tempPlayerScores = "cheese\n";

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

                TextMeshProUGUI[] texts = Resources.FindObjectsOfTypeAll(typeof(TextMeshProUGUI)) as TextMeshProUGUI[];
                for (int i = 0; i < texts.Length; i++) {
                    if (texts[i].name == "cheeseNames") {
                        texts[i].text = tempPlayerNames;
                    } else if (texts[i].name == "cheeseScores") {
                        texts[i].text = tempPlayerScores;
                    }
                }             
                done2 = true;
            } else {
                Debug.Log("error fetching leaderboard" + response);
                done2 = true;
            }
        });
        LootLockerSDKManager.GetScoreList(distanceLeaderboardKey, 10, 0, (response) => {
            if (response.success) {
                Debug.Log("leaderboard received");
                string tempPlayerNames = "name\n";
                string tempPlayerScores = "distance\n";

                LootLockerLeaderboardMember[] members = response.items;

                foreach (LootLockerLeaderboardMember member in members) {
                    tempPlayerNames += member.rank + ". ";
                    if (member.player.name != "") {
                        tempPlayerNames += member.player.name;
                    } else {
                        tempPlayerNames += member.player.id;
                    }
                    tempPlayerScores += member.score + "m\n";
                    tempPlayerNames += "\n";
                }

                TextMeshProUGUI[] texts = Resources.FindObjectsOfTypeAll(typeof(TextMeshProUGUI)) as TextMeshProUGUI[];
                for (int i = 0; i < texts.Length; i++) {
                    if (texts[i].name == "distanceNames") {
                        texts[i].text = tempPlayerNames;
                    } else if (texts[i].name == "distanceScores") {
                        texts[i].text = tempPlayerScores;
                    }
                }             
                done3 = true;
            } else {
                Debug.Log("error fetching leaderboard" + response);
                done3 = true;
            }
        });
        yield return new WaitWhile(() => done1 == false || done2 == false || done3 == false);
    }

    public IEnumerator FetchUserHighscoreRoutine() {
        bool done1 = false;
        bool done2 = false;
        bool done3 = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.GetMemberRank(leaderboardKey, playerID, (response) => {
            if (response.success) {
                Debug.Log("leaderboard received");
                    string tempPlayerName = "";
                    string tempPlayerScore = "";

                    if (response.player == null || response.player.name == "" || response.rank == 0) {
                        tempPlayerName += "Enter a name and play a game to join the leaderboard";
                        tempPlayerScore += "";
                    } else {
                        tempPlayerName += response.rank + ". ";
                        tempPlayerName += response.player.name;
                        tempPlayerScore += response.score;
                    }
                Debug.Log(tempPlayerName);
                Debug.Log(tempPlayerScore);
                TextMeshProUGUI[] texts = Resources.FindObjectsOfTypeAll(typeof(TextMeshProUGUI)) as TextMeshProUGUI[];
                for (int i = 0; i < texts.Length; i++) {
                    if (texts[i].name == "userLeaderboardName") {
                        texts[i].text = tempPlayerName;
                    } else if (texts[i].name == "userLeaderboardScore") {
                        texts[i].text = tempPlayerScore;
                    }
                }       
                done1 = true;
            } else {
                Debug.Log("error fetching leaderboard" + response);
                done1 = true;
            }
        });
        LootLockerSDKManager.GetMemberRank(cheeseLeaderboardKey, playerID, (response) => {
            if (response.success) {
                // Debug.Log("leaderboard received");
                   string tempPlayerName = "";
                    string tempPlayerScore = "";
                    
                    if (response.player == null || response.player.name == "" || response.rank == 0) {
                        tempPlayerName += "Enter a name and play a game to join the leaderboard";
                        tempPlayerScore += "";
                    } else {
                        tempPlayerName += response.rank + ". ";
                        tempPlayerName += response.player.name;
                        tempPlayerScore += response.score;
                    }

                TextMeshProUGUI[] texts = Resources.FindObjectsOfTypeAll(typeof(TextMeshProUGUI)) as TextMeshProUGUI[];
                for (int i = 0; i < texts.Length; i++) {
                    if (texts[i].name == "userCheeseName") {
                        texts[i].text = tempPlayerName;
                    } else if (texts[i].name == "userCheeseScore") {
                        texts[i].text = tempPlayerScore;
                    }
                }        
                done2 = true;
            } else {
                Debug.Log("error fetching leaderboard" + response);
                done2 = true;
            }
        });
        LootLockerSDKManager.GetMemberRank(distanceLeaderboardKey, playerID, (response) => {
            if (response.success) {
                // Debug.Log("leaderboard received");


                    string tempPlayerName = "";
                    string tempPlayerScore = "";

                    if (response.player == null || response.player.name == "" || response.rank == 0) {
                        tempPlayerName += "Enter a name and play a game to join the leaderboard";
                        tempPlayerScore += "";
                    } else {
                        tempPlayerName += response.rank + ". ";
                        tempPlayerName += response.player.name;
                        tempPlayerScore += response.score;
                    }

                    TextMeshProUGUI[] texts = Resources.FindObjectsOfTypeAll(typeof(TextMeshProUGUI)) as TextMeshProUGUI[];
                    for (int i = 0; i < texts.Length; i++) {
                        if (texts[i].name == "userDistanceName") {
                            texts[i].text = tempPlayerName;
                        } else if (texts[i].name == "userDistanceScore") {
                            texts[i].text = tempPlayerScore + "m";
                        }
                    }      
                done3 = true;
            } else {
                Debug.Log("error fetching leaderboard" + response);
                done3 = true;
            }
        });
        yield return new WaitWhile(() => done1 == false || done2 == false || done3 == false);
    }
}
