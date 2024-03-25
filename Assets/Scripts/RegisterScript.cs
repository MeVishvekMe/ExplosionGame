using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegisterScript : MonoBehaviour {

    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    public Button registerButton;

    public void callRegister() {
        StartCoroutine(register());
    }

    IEnumerator register() {
        WWWForm form = new WWWForm();
        form.AddField("name", usernameField.text);
        form.AddField("password", passwordField.text);
        WWW www = new WWW("http://localhost/DBconnect/register.php", form);
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
