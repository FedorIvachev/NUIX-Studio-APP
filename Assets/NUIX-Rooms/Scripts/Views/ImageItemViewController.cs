using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageItemViewController : ItemViewController
{

    public List<Texture> images;
    public int imageIndex = 0;

    public Renderer image;


    // Start is called before the first frame update
    void Start()
    {
        if (images == null) images = new List<Texture>();
        receiverMethods.Add(nameof(NextImage));
        receiverMethods.Add(nameof(PreviousImage));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetImage(int imageIndex)
    {
        image.material.mainTexture = images[imageIndex];
    }


    public void NextImage()
    {

        imageIndex++;
        if (imageIndex >= images.Count) imageIndex = 0;
        SetImage(imageIndex);
    }

    public void PreviousImage()
    {
        imageIndex--;
        if (imageIndex < 0) imageIndex = images.Count - 1;
        SetImage(imageIndex);
    }
}
