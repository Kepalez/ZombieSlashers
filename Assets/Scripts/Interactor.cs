using UnityEngine;

public class Interactor : MonoBehaviour{
    public Transform interactionSource;
    public float interactionRange;

    void Update(){
        if(Input.GetKeyDown(KeyCode.E)){
            Ray r = new Ray(interactionSource.position,interactionSource.forward);
            if(Physics.Raycast(r,out RaycastHit hitInfo,interactionRange)){
                if(hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj)){
                    interactObj.Interact();
                }
            }
        }
    }
}