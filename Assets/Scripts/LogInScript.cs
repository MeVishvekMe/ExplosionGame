using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LogInScript : MonoBehaviour {
    
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    public Button loginButton;
    public bool toLeaderboard = false;

    public void callLogin() {
        toLeaderboard = false;
        StartCoroutine(login());
    }

    IEnumerator login() {
        WWWForm form = new WWWForm();
        form.AddField("name", usernameField.text);
        form.AddField("password", passwordField.text);
        WWW www = new WWW("http://localhost/DBconnect/login.php", form);
        yield return www;
        Debug.Log("Here");
        Debug.Log(www.text);
        if (www.text[0] == '0') {
            Debug.Log("Logged in successfully");
            DBManager.username = usernameField.text;
            if (toLeaderboard) {
                SceneManager.LoadScene(3);
            }
            else {
                SceneManager.LoadScene(0);
            }
        }
    }


    public void goToLeaderboard() {
        toLeaderboard = true;
        StartCoroutine(login());
    }
    
    public void verifyInputs() {
        loginButton.interactable = (usernameField.text.Length >= 8 && passwordField.text.Length >= 8);
    }
    public void goToRegister() {
        SceneManager.LoadScene(2);
    }
}
