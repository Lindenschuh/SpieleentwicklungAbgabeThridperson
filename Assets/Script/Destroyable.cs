using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public int life = 100;

    public bool GetDMG(int dmg)
    {
        life -= dmg;
        if (life <= 0)
        {
            Destroy(gameObject);
            return true;
        }
        return false;
    }
}