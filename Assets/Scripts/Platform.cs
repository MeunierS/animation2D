using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other){
        other.transform.SetParent(transform);
    }
    private void OnTriggerExit2D(Collider2D other){
        other.transform.SetParent(null);
    }
}
