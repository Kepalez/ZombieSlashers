using UnityEngine;

public class firstPersonView : MonoBehaviour{
    public Transform camT;
    public Transform headT;

    void Update(){
        camT.position = headT.position+headT.up*0.2f-headT.right*0.2f;
    }
}