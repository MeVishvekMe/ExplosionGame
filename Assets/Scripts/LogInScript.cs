using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogInScript : MonoBehaviour {
    public void goToRegister() {
        SceneManager.LoadScene(2);
    }
}
