using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegisterScript : MonoBehaviour {

    public InputField usernameField;
    public InputField passwordField;
    public Button registerButton;

    public void callRegister() {
        StartCoroutine(register());
    }

    IEnumerator register() {
        WWW www = new WWW("http://localhost/DBconnect/register.php");
        yield return www;
        if (www.text == "0") {
            Debug.Log("User created successfully");
            goToLogin();
        }
        else {
            Debug.Log("Error code " + www.text);
        }
    }

    public void verifyInputs() {
        registerButton.interactable = (usernameField.text.Length >= 8 && passwordField.text.Length >= 8);
    }

    public void goToLogin() {
        SceneManager.LoadScene(1);
    }
}
