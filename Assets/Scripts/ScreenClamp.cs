using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/*********************************************/
/*  Mark Crawford                            */
/* CSC 350 H, Prof Tang                      */
/* Lab 7.B                                     */
/* 03/14/2024                                */
/*********************************************/


// class for ScreenUtils
public static class ScreenClamp
{
    static int screenwidth, screenheight;
    static float screenleft, screenright, screentop, screenbottom;

    public static float ScreenLeft { get { return screenleft; } }
    public static float ScreenRight { get { return screenright; } }
    public static float ScreenTop { get { return screentop; } }
    public static float ScreenBottom { get { return screenbottom; } }




    // Get screen dimensions
    public static void Initialize()
    {
        screenwidth = Screen.width;
        screenheight = Screen.height;

        float ScreenZ = -Camera.main.transform.position.z;
        Vector3 screenleftbottom = new Vector3(0, 0, ScreenZ);
        Vector3 lowerleftcornerworld = Camera.main.ScreenToWorldPoint(screenleftbottom);
        Vector3 screenrighttop = new Vector3(screenwidth, screenheight, ScreenZ);
        Vector3 upperrightcornerworld = Camera.main.ScreenToWorldPoint(screenrighttop);

        screenleft = lowerleftcornerworld.x;
        screenright = upperrightcornerworld.x;
        screentop = upperrightcornerworld.y;
        screenbottom = lowerleftcornerworld.y;


    }

}
