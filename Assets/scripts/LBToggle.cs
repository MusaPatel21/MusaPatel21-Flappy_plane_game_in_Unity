using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LBToggle : MonoBehaviour
{
    public GameManager gameManager;
    public void lbopen()
    {
        gameManager.lbopen();
    }
    public void lbclose()
    {
        gameManager.lbclose();
    }
}
