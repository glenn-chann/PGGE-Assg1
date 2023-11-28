using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Material : MonoBehaviour
{
    public enum FloorMaterial
    {
        Concrete,
        Metal,
        Wood,
        Dirt,
        Sand,
    }


    public FloorMaterial Fmaterial;

}
