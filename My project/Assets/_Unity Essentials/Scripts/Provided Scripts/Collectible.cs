using UnityEngine;

public class Collectible : MonoBehaviour
{

    public float RotationSpeed;
    public GameObject onCollectEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    transform.Rotate(0, 0.5f, 0);
        
    }

    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Player")) {
            //destroy the collectible
            Destroy(gameObject);

            //Instantiate the particle effect
            Instantiate(onCollectEffect, transform.position, transform.rotation);
        }
       
    }
}
