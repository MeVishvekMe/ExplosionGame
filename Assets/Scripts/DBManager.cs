using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DBManager : MonoBehaviour {
    public static string username;
    public TextMeshProUGUI lbText;
    public static int loginScore;
    public static int newHighScore;
    public static bool shouldChange;

    private void Update() {
        if (shouldChange) {
            shouldChange = false;
            changeScore();
        }
    }

    public void changeScore() {
        StartCoroutine(updateScore());
    }

    IEnumerator updateScore() {
        WWWForm form = new WWWForm();
        form.AddField("name", username);
        string newHighScoreString = "" + newHighScore;
        form.AddField("newHighScoreString", newHighScoreString);
        WWW www = new WWW("http://localhost/DBconnect/updateHighScore.php", form);
        yield return www;
    }

    private void Start() {
        StartCoroutine(leaderboard());
        StartCoroutine(currentUser());
    }

    public void goBack() {
        SceneManager.LoadScene(1);
    }

    IEnumerator currentUser() {
        WWWForm form = new WWWForm();
        form.AddField("name", username);
        WWW www = new WWW("http://localhost/DBconnect/getCurrentUser.php", form);
        yield return www;
        loginScore = int.Parse(www.text);
        Debug.Log(loginScore);
    }

    IEnumerator leaderboard() {
        WWWForm form = new WWWForm();
        WWW www = new WWW("http://localhost/DBconnect/leaderboard.php", form);
        yield return www;
        lbText.text = www.text;
    }
}
