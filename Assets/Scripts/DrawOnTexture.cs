using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawOnTexture : MonoBehaviour {

    public Texture2D baseTexture;

    // Update is called once per frame
    void Update()
    {
        OnMouseDrawing();    
    }

    /// <summary>
    /// Allows drawing to the texture with a mouse
    /// </summary>
    private void OnMouseDrawing()
    {
        if (Camera.main == null)
        {
            throw new Exception("Cannot find main camera");
        }

        //is mouse being pressed
        if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1)) return;
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //Do nothing if we arent hitting anyting
        if (!Physics.Raycast(mouseRay, out hit)) return;
        //Do nothing if we didn't get hit
        if (hit.collider.transform != transform) return;
        
        Vector2 pixelUV = hit.textureCoord;
        pixelUV.x *= baseTexture.width;
        pixelUV.y *= baseTexture.height;

        Color colorToSet = Input.GetMouseButton(0) ? Color.white : Color.black;

        baseTexture.SetPixel((int)pixelUV.x, (int)pixelUV.y, colorToSet);
        baseTexture.Apply();

    }
}
