using UnityEngine;

public class XPcollect : MonoBehaviour
{
    int value;
    
    void Start(){
        //Implement that the amount of xp gave equals the lvl of the mob killed.
        value = 1;     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col){
        PlayerScript.xp += value;
        Destroy(gameObject);
    }
}
