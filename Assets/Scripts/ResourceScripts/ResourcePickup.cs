using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePickup : MonoBehaviour
{
    [SerializeField] ResourceType type = 0;
    int amount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAmount(int newAmount)
    {
        amount = newAmount;
    }

    public int GetAmount()
    {
        return amount;
    }

    void OnTriggerEnter(Collider collision)
    {
        Player playerComponent = collision.GetComponent<Player>();

        if(playerComponent)
        {
            amount -= playerComponent.AbsorbResource(type, amount);
            if(amount <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
