using UnityEngine;

public class RandomColorOnInstantiate : MonoBehaviour
{
    private Renderer rend;
    //simple stuff. assign random colours to player characters on instantiation. 
    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = Random.ColorHSV();
    }
}