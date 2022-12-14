using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{
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

    private void Start()
    {
        bodyID = 0;
        hairID = 0;
        shirtID = 0;
        pantID = 0;
        shoeID = 0;
    }

    public void nextBody()
    {
        bodyID = (bodyID + 1) % body.GetInteger("Body_Count");
        body.SetInteger("ID", bodyID);
    }
    public void prevBody()
    {
        bodyID = bodyID - 1;
        if (bodyID < 0)
            bodyID = body.GetInteger("Body_Count") - 1;
        body.SetInteger("ID", bodyID);
    }
    
}
