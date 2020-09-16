using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
  //  private float points=100.0f;
    void Start()
    {

    }

    // Update is called once per frame

    void Update()
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Score>().score+=2;
            Destroy(gameObject);
        }
    }
}
