using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo_Count : MonoBehaviour
{
    public Text ammunitionText;
    public Text magText;
    public static Ammo_Count occurrence;
    private void Awake()
    {
        occurrence=this;
    }
    public void UpdateAmmoText(int presentAmmunition)
    {
        ammunitionText.text="Ammo. "+ presentAmmunition;
    }
public void UpdateMagText(int mag)
{
    magText.text= "Magazines. "+ mag;
}

}
