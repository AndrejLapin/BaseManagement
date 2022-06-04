using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : Gadget
{
    [SerializeField] float tickTime = 0.2f;
    [SerializeField] float damage = 5.0f;
    [SerializeField] DamageHitbox damageHitbox;
    float elapsedTime = 0;
    bool usingItem = false;

    void Update()
    {
        if(usingItem)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime > tickTime)
            {
                //Debug.Log(currentOwner.GetBodyRotation());
                int amountToInst = (int)(elapsedTime / tickTime);
                for(int i = 0; i < amountToInst; i++)
                {
                    DamageHitbox hitbox = Instantiate(damageHitbox, currentOwner.transform.position + (currentOwner.GetBodyRotation() * new Vector3(0.0f, 0.0f, 1.0f)), Quaternion.identity) as DamageHitbox;
                    hitbox.Init(damage, tickTime, true, true, false);
                }
                elapsedTime -= amountToInst * tickTime;
            }
        }
    }

    public override void Use()
    {
        if(!usingItem)
        {
            Debug.Log("Drill item was used");
            usingItem = true;
        }
    }

    public override void StopUsing()
    {
        usingItem = false;
    }
}
