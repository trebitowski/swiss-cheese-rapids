using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LeaderboardMenu : MonoBehaviour
{
    public TMP_InputField input;

    public GameObject[] scoreBoards;
    public int selected = 0;
    public void HandleRename() {
        SessionManager sessionManager = FindObjectOfType<SessionManager>();
        sessionManager.SetPlayerName(input.text);
    }

    public void HandleLeft() {
        scoreBoards[selected].SetActive(false);
        selected = (selected - 1) % scoreBoards.Length;
        if (selected < 0) {selected = scoreBoards.Length - 1;}
        scoreBoards[selected].SetActive(true);
    }

    public void HandleRight() {
        scoreBoards[selected].SetActive(false);
        selected = (selected + 1) % scoreBoards.Length;
        scoreBoards[selected].SetActive(true);
    }
}
