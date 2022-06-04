using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gadget : MonoBehaviour
{
    List<Player> pickupCandidates = new List<Player>();
    protected Player currentOwner = null;
    bool pickedUp = false;
    SphereCollider myCollider;
    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider collision)
    {
        if(!pickedUp)
        {
            Player playerComponent = collision.GetComponent<Player>();

            if(playerComponent && playerComponent.CanPickup())
            {
                playerComponent.AddToPickupList(this);
                pickupCandidates.Add(playerComponent);
            }
        }
    }

    private void OnTriggerExit(Collider collision) 
    {
        if(!pickedUp)
        {
            Player playerComponent = collision.GetComponent<Player>();

            if(playerComponent && pickupCandidates.Contains(playerComponent))
            {
                playerComponent.RemoveFromPickupList(this);
                pickupCandidates.Remove(playerComponent);
            }
        }
    }

    public void Pickup(Player owner)
    {
        // after item is picked up it should not be picked up by others
        foreach (Player candidate in pickupCandidates)
        {
            candidate.RemoveFromPickupList(this);
        }
        pickupCandidates.Clear();
        pickedUp = true;
        myCollider.enabled = false;
        currentOwner = owner;
    }

    public void Drop()
    {
        pickedUp = false;
        myCollider.enabled = true;
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        currentOwner = null;
    }

    public void RemoveCandidate(Player player)
    {
        pickupCandidates.Remove(player);
    }

    public virtual void Use()
    {
        Debug.Log("Gadget item was used");
    }

    public virtual void StopUsing()
    {

    }
}
