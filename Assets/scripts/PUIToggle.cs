using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUIToggle : MonoBehaviour
{
    public GameManager gameManager;
    public void puiopen()
    {
        gameManager.playopen();
    }
    public void puiclose()
    {
        gameManager.playclose();
    }
}
