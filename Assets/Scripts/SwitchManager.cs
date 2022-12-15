using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    [Header("Animation Part")]
    [SerializeField] Animator body;
    [SerializeField] Animator hair;
    [SerializeField] Animator shirt;
    [SerializeField] Animator pant;
    [SerializeField] Animator shoe;
    
    private int bodyID;
    private int hairID;
    private int shirtID;
    private int pantID;
    private int shoeID;
    
    [Header("Button Cooldown")]
    [SerializeField] private float cooldown = 1.5f;

    private bool canPress = true;
    private float currentTime;



    private void Start()
    {
        bodyID = 0;
        hairID = 0;
        shirtID = 0;
        pantID = 0;
        shoeID = 0;
        currentTime = cooldown;
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            canPress = true;
        }
        else
            canPress = false;
    }

    public void nextBody()
    {
        if(canPress)
        {
            bodyID = (bodyID + 1) % body.GetInteger("Body_Count");
            body.SetInteger("ID", bodyID);
            currentTime = cooldown;
        }
    }
    public void prevBody()
    {
        if (canPress)
        {
            bodyID = bodyID - 1;
            if (bodyID < 0)
                bodyID = body.GetInteger("Body_Count") - 1;
            body.SetInteger("ID", bodyID);
            currentTime = cooldown;
        }
    }
    public void nextHair()
    {
        if(canPress)
        {
            hairID = (hairID + 1) % hair.GetInteger("Hair_Count");
            hair.SetInteger("ID", hairID);
            currentTime = cooldown;
        }
    }
    public void prevHair()
    {
        if (canPress)
        {
            hairID = hairID - 1;
            if (hairID < 0)
                hairID = hair.GetInteger("Hair_Count") - 1;
            hair.SetInteger("ID", hairID);
            currentTime = cooldown;
        }
    }
    public void nextPant()
    {
        if (canPress)
        {
            pantID = (pantID + 1) % pant.GetInteger("Pant_Count");
            pant.SetInteger("ID", pantID);
            currentTime = cooldown;
        }
    }
    public void prevPant()
    {
        if (canPress)
        {
            pantID = pantID - 1;
            if (pantID < 0)
                pantID = pant.GetInteger("Pant_Count") - 1;
            pant.SetInteger("ID", pantID);
            currentTime = cooldown;
        }
    }
    public void nextShirt()
    {
        if (canPress)
        {
            shirtID = (shirtID + 1) % shirt.GetInteger("Shirt_Count");
            shirt.SetInteger("ID", shirtID);
            currentTime = cooldown;
        }
    }
    public void prevShirt()
    {
        if (canPress)
        {
            shirtID = shirtID - 1;
            if (shirtID < 0)
                shirtID = shirt.GetInteger("Shirt_Count") - 1;
            shirt.SetInteger("ID", shirtID);
            currentTime = cooldown;
        }
    }
    public void nextShoe()
    {
        if (canPress)
        {
            shoeID = (shoeID + 1) % shoe.GetInteger("Shoe_Count");
            shoe.SetInteger("ID", shoeID);
            currentTime = cooldown;
        }
    }
    public void prevShoe()
    {
        if (canPress)
        {
            shoeID = shoeID - 1;
            if (shoeID < 0)
                shoeID = shoe.GetInteger("Shoe_Count") - 1;
            shoe.SetInteger("ID", shoeID);
            currentTime = cooldown;
        }
    }

}
