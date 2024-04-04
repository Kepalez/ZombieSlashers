using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCarpetTrap : MonoBehaviour
{
    Transform spears;
    [SerializeField]Vector3 initialSpearsPosition;
    public float attackDuration = 0.5f;
    public float returnDuration = 2f;

    // Start is called before the first frame update
    void Start()
    {
        spears = transform.GetChild(1);
        initialSpearsPosition = spears.position;
        StartCoroutine("attackSpear");
    }

    IEnumerator attackSpear(){
        float timeElapsed = 0f;
        while (timeElapsed < attackDuration)
        {
            timeElapsed += Time.deltaTime;
            spears.position = initialSpearsPosition + (transform.position - initialSpearsPosition)*(timeElapsed / attackDuration);
                
            yield return null;
        }

        spears.position = transform.position;
        yield return new WaitForSeconds(1f);
        StartCoroutine("returnSpear");
    }
    IEnumerator returnSpear(){
        float timeElapsed = 0f;
        while (timeElapsed < returnDuration)
        {
            timeElapsed += Time.deltaTime;
            spears.position = transform.position + (initialSpearsPosition - transform.position)*(timeElapsed / returnDuration);
                
            yield return null;
        }

        spears.position = initialSpearsPosition;
        yield return new WaitForSeconds(4f);
        StartCoroutine("attackSpear");
    }
}
