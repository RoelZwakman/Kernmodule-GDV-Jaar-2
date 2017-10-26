using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesiredDirectionVisualiser : MonoBehaviour {


    public static DesiredDirectionVisualiser instance;
    
    public Image[] arrowImages;

    void Awake()
    {
        instance = this;
    }

    public static void SwitchDirectionToShow(int dirToShow) /////Switches which arrow should be shown to indicate which paddle needs to be hit for a point
    {
        if (dirToShow == 0)
        {
            instance.arrowImages[0].color = new Color(instance.arrowImages[0].color.r, instance.arrowImages[0].color.g, instance.arrowImages[0].color.b, 1);
            instance.arrowImages[1].color = new Color(instance.arrowImages[1].color.r, instance.arrowImages[1].color.g, instance.arrowImages[1].color.b, 0);
        }

        else if (dirToShow == 1)
        {
            instance.arrowImages[0].color = new Color(instance.arrowImages[0].color.r, instance.arrowImages[0].color.g, instance.arrowImages[0].color.b, 0);
            instance.arrowImages[1].color = new Color(instance.arrowImages[1].color.r, instance.arrowImages[1].color.g, instance.arrowImages[1].color.b, 1);
        }
    }
}
