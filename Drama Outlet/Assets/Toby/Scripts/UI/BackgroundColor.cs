using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundColor : MonoBehaviour
{
    public RawImage backgroundImage;
    public List<Color> colors;
    public Camera mainCamera;

    void Start()
    {
        colors = colors.Shuffle().ToList();
        backgroundImage.color = colors[0];
        mainCamera.backgroundColor = colors[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
