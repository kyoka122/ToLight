using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using KanKikuchi.AudioManager;

public static class SEPlay
{
    public static void SEplay(string Pathname)
    {
        Debug.Log("Click");
        //Path.GetFileNameWithoutExtension(Pathname);
        SEManager.Instance.Play(Pathname);
    }
    
}
