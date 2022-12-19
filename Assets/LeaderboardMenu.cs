using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LeaderboardMenu : MonoBehaviour
{
    public TMP_InputField input;
    public void HandleRename() {
        SessionManager sessionManager = FindObjectOfType<SessionManager>();
        sessionManager.SetPlayerName(input.text);
    }
}
