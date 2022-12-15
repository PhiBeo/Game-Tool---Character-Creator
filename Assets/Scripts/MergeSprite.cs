using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Unity.IO;
using UnityEditor;

public class MergeSprite : MonoBehaviour
{
    [SerializeField] Animator bodyAnimator;
    [SerializeField] Animator hairAnimator;
    [SerializeField] Animator shirtAnimator;
    [SerializeField] Animator pantAnimator;
    [SerializeField] Animator shoeAnimator;

    private Texture2D[] bodies;
    private Texture2D[] hairs;
    private Texture2D[] shirts;
    private Texture2D[] pants;
    private Texture2D[] shoes;
    private void Start()
    {
        //initialize the texture of each body component locate in Assets/Body part
        bodies = Resources.LoadAll<Texture2D>("Body");
        hairs = Resources.LoadAll<Texture2D>("Hair");
        shirts = Resources.LoadAll<Texture2D>("Shirt");
        pants = Resources.LoadAll<Texture2D>("Pant");
        shoes = Resources.LoadAll<Texture2D>("Shoe");
    }

    //combine the pixel color of every sprite
    private Texture2D CombineBodyPart(int bodyID, int hairID, int shirtID, int pantID, int shoeID)
    {
        Texture2D tmpBody = getReadableTexture(bodies[bodyID]);
        Texture2D tmpHair = getReadableTexture(hairs[hairID]);
        Texture2D tmpShirt = getReadableTexture(shirts[shirtID]);
        Texture2D tmpPant = getReadableTexture(pants[pantID]);
        Texture2D tmpShoe = getReadableTexture(shoes[shoeID]);

        Texture2D mergeTexture = new Texture2D(tmpBody.width, tmpBody.height);
        
        //set the base texture to body
        mergeTexture.SetPixels(tmpBody.GetPixels());

        for (int x = 0; x < tmpBody.width; x++)
        {
            for(int y = 0; y < tmpBody.height;y++)
            {
                //Get the each component pixel color in x,y coordinate
                //h: hair
                //sT: shirt
                //p: pant
                //sE: shoe
                Color h = tmpHair.GetPixel(x, y);
                Color sT = tmpShirt.GetPixel(x, y);
                Color p = tmpPant.GetPixel(x, y);
                Color sE = tmpShoe.GetPixel(x, y);
                
                //check if there a pixel and emplace on the body component texture
                if (h.a > 0.0f)
                    mergeTexture.SetPixel(x, y, h);
                if (sT.a > 0.0f)
                    mergeTexture.SetPixel(x, y, sT);
                if (p.a > 0.0f)
                    mergeTexture.SetPixel(x, y, p);
                if (sE.a > 0.0f)
                    mergeTexture.SetPixel(x, y, sE);
            }
        }

        //apply new colors
        mergeTexture.Apply();
        return mergeTexture;
    }
    //Texture is not readable by defauld
    //function to get a readable texture
    Texture2D getReadableTexture(Texture2D texture)
    {
        RenderTexture tmp = RenderTexture.GetTemporary(
                    texture.width,
                    texture.height,
                    0,
                    RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);


        // Blit the pixels on texture to the RenderTexture
        Graphics.Blit(texture, tmp);

        // Backup the currently set RenderTexture
        RenderTexture previous = RenderTexture.active;

        // Set the current RenderTexture to the temporary one we created
        RenderTexture.active = tmp;

        // Create a new readable Texture2D to copy the pixels to it
        Texture2D myTexture2D = new Texture2D(texture.width, texture.height);

        // Copy the pixels from the RenderTexture to the new Texture
        myTexture2D.ReadPixels(new Rect(0, 0, tmp.width, tmp.height), 0, 0);
        myTexture2D.Apply();

        // Reset the active RenderTexture
        RenderTexture.active = previous;

        // Release the temporary RenderTexture
        RenderTexture.ReleaseTemporary(tmp);

        return myTexture2D;
    }

    public void SaveSprite()
    {
        //get choosen ID from animator
        int bodyID = bodyAnimator.GetInteger("ID");
        int hairID = hairAnimator.GetInteger("ID");
        int shirtID = shirtAnimator.GetInteger("ID");
        int pantID = pantAnimator.GetInteger("ID");
        int shoeID = shoeAnimator.GetInteger("ID");

        //merging the sprite
        Texture2D saveSprite = CombineBodyPart(bodyID, hairID, shirtID, pantID, shoeID);

        var bytes = saveSprite.EncodeToPNG();

        var dirPath = Application.dataPath + "/Output";

        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        File.WriteAllBytes(dirPath + "/R" + Random.Range(0, 100000) + ".png", bytes);

        Debug.Log(bytes.Length / 1024 + "Kb was saved as: " + dirPath);

        UnityEditor.AssetDatabase.Refresh();
    }
}
