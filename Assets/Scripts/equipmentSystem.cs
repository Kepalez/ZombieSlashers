using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipmentSystem : MonoBehaviour
{

    [SerializeField] GameObject weaponHolder;
    [SerializeField] GameObject weaponSheath;
    [SerializeField] GameObject weapon;

    GameObject currentWeaponInHand;
    GameObject currentWeaponInSheath;

    // Start is called before the first frame update
    void Start()
    {
        currentWeaponInSheath = Instantiate(weapon,weaponSheath.transform);
    }

    public void DrawWeapon(){
        if(currentWeaponInHand == null) currentWeaponInHand= Instantiate(weapon,weaponHolder.transform);
        Destroy(currentWeaponInSheath);
    }

    public void SheathWeapon(){
        if(currentWeaponInSheath == null) currentWeaponInSheath = Instantiate(weapon,weaponSheath.transform);
        Destroy(currentWeaponInHand);
    }

    public void StartDealDamage(){
        currentWeaponInHand.GetComponentInChildren<DamageDealer>().StartDealDamage();
    }
    public void EndDealDamage(){
        currentWeaponInHand.GetComponentInChildren<DamageDealer>().EndDealDamage();
    }
}
