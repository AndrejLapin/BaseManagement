using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float maxMoveSpeed = 2.5f;
    [SerializeField] float acceleration = 0.8f;
    [SerializeField] GameObject characterBody;
    [SerializeField] GameObject hands;
    CharacterState myState;
    float moveSpeed = 0f;
    Camera myCamera;
    Rigidbody myRigidBody;
    Gadget currentGadget = null;
    List<Gadget> pickupList = new List<Gadget>();

    // Start is called before the first frame update
    void Start()
    {
        myCamera = FindObjectOfType<Camera>();
        myRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Pickup();
    }

    void Move()
    {
        float horizontalThrow = Input.GetAxisRaw(InputName.HORIZONTAL);
        float verticalThrow = Input.GetAxisRaw(InputName.VERTICAL);
        float hypothenuse = Mathf.Sqrt(horizontalThrow * horizontalThrow + verticalThrow * verticalThrow);

        float horizontalVelocity = 0;
        float verticalVelocity = 0;

        if(hypothenuse != 0)
        {
            myState = CharacterState.Moving;
            //characterAnimator.SetBool(ANIMATOR_RUNNING, true);
            moveSpeed += acceleration * Time.deltaTime;
            if(moveSpeed > maxMoveSpeed) moveSpeed = maxMoveSpeed;
        }
        else
        {
            myState = CharacterState.Idle;
            //characterAnimator.SetBool(ANIMATOR_RUNNING, false);
            moveSpeed -= acceleration * Time.deltaTime;
            if(moveSpeed < 0) moveSpeed = 0;
        }
        
        if(horizontalThrow != 0)
        {
            horizontalVelocity = horizontalThrow / hypothenuse * moveSpeed ;
        }
        // else // to perform a little drift
        // {
        //     horizontalVelocity = myRigidBody.velocity.x * acceleration * acceleration;
        // }

        if(verticalThrow != 0)
        {
            verticalVelocity = verticalThrow / hypothenuse * moveSpeed;
        }
        // else // to perform a little drift
        // {
        //     verticalVelocity = myRigidBody.velocity.z * acceleration * acceleration;
        // }

        // calculating player velocity vector
        Vector3 playerVelocity = new Vector3(horizontalVelocity, 0, verticalVelocity);
        // rotating velocity vector by camera rotation
        // needed so if the camera faces other way controls behave naturally
        playerVelocity = Quaternion.Euler(0, myCamera.transform.rotation.eulerAngles.y, 0 ) * playerVelocity;

        if (hypothenuse != 0)
        {
            RotateBody(playerVelocity);
        }
        myRigidBody.velocity = playerVelocity;
    }

    void RotateBody(Vector3 playerVelocity)
    {
        float rotationTargetAngle = Vector3.Angle(Vector3.forward, playerVelocity) * Mathf.Sign(playerVelocity.x);
        characterBody.transform.rotation = Quaternion.Euler(0, rotationTargetAngle, 0);
    }

    void Pickup()
    {
        if(Input.GetButtonDown(InputName.PICKUP))
        {
            if(CanPickup())
            {
                if(pickupList.Count > 0 && pickupList[0])
                {
                    currentGadget = pickupList[0];
                    currentGadget.Pickup();
                    pickupList.Remove(currentGadget);
                    currentGadget.transform.parent = hands.transform;
                    currentGadget.transform.position = hands.transform.position;
                    currentGadget.transform.rotation = hands.transform.rotation;

                    foreach (Gadget gadget in pickupList)
                    {
                        // remove this candidate
                        gadget.RemoveCandidate(this);
                    }
                    pickupList.Clear();
                }
            }
            else // drop
            {
                currentGadget.transform.parent = null;
                currentGadget.Drop();
                currentGadget = null;
            }
        }
    }

    public void AddToPickupList(Gadget gadget)
    {
        pickupList.Add(gadget);
    }

    public void RemoveFromPickupList(Gadget gadget)
    {
        pickupList.Remove(gadget);
    }

    public bool CanPickup()
    {
        return !currentGadget;
    }
}
