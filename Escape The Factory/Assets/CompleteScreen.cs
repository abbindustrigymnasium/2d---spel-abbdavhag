using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Windows.Input;

public class CompleteScreen : MonoBehaviour
{

    public void ReturnToMain()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(1);
        }
    }
}

