using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void OnStartGame(){
        SceneManager.LoadScene("DungeonMap");
    }
    void OnQuitGame(){
        Application.Quit();
    }
}
