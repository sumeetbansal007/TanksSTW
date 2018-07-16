using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    private void OnEnable()
    {
        Invoke("DestroyBomb", 10.0f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Destroy(gameObject);
        ContactPoint2D contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        //TODO: Need to pass the contact point point to PlatformDestroyer
        Debug.Log("Contact point = "+ pos);
        
        GameObject go = Instantiate(GameManager.instance.bombEffects[GameManager.instance.currentIndexOfBomb].gameObject, pos, Quaternion.identity);
        go.GetComponent<ParticleSystem>().Play();
        go.GetComponent<AudioSource>().Play();
        //Debug.Log("Ready To Switch");
        //GameManager.instance.SwitchTurn();
       
    }
   
    private void DestroyBomb()
    {
        if (this.gameObject != null)
            Destroy(gameObject);
    }
    private void OnDestroy()
    {
        CancelInvoke("DestroyBomb");
    }
}
