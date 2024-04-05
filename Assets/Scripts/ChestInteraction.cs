using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteraction : MonoBehaviour,IInteractable
{
    GameObject chestLight;
    void Awake(){
        chestLight = transform.GetChild(2).gameObject;
    }
    public void Interact()
    {
        print("Opened chest");
        GetComponent<Animator>().SetBool("Opened",true);
        GameObject.FindWithTag("GameController").GetComponent<GameManager>().ScorePoint();
        chestLight.GetComponent<Light>().intensity = 0;
    }
}
