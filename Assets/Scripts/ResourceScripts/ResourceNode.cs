using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    //need a reference to tile it is on
    //resource type
    //resource amount
    //hp?
    [SerializeField] ResourceType type = 0;
    [SerializeField] int resourceAmount = 5;
    [SerializeField] float nodeHealth = 50;
    [SerializeField] ResourcePickup drop;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(float damageAmount)
    {
        nodeHealth -= damageAmount;
        if(nodeHealth <= 0)
        {
            // destroy and drop resources
            ResourcePickup newDrop = Instantiate(drop, transform.position, Quaternion.identity) as ResourcePickup;
            newDrop.SetAmount(resourceAmount);
            Destroy(gameObject);
        }
    }
}
