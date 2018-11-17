using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum OrbType
{
    None = 0,
    HealthOrb = 1,
    FireResistanceOrb = 2,
    SpeedOrb = 3,
    InvisibleOrb = 4,
    JumpOrb = 5,
    CoinOrb = 6,
    InstantDeathOrb = 7


}
[ExecuteInEditMode]
public class Orb : MonoBehaviour {

    private bool isInteractClicked = false;
    private bool isStarted = false;

    [SerializeField]
    public Color[] colorsOfOrbTypes = new Color[Enum.GetNames(typeof(OrbType)).Length];
    [SerializeField]
    private OrbType TypeOfOrb;

    void Start()
    {
        if (!isStarted)
        {
            isStarted = true;
            int colorIndex = (int)TypeOfOrb;
            GetComponent<SpriteRenderer>().color = colorsOfOrbTypes[colorIndex];
        }
    }

    void Update()
    {
        if (!isStarted)
        {
            int colorIndex = (int)TypeOfOrb;
            GetComponent<SpriteRenderer>().color = colorsOfOrbTypes[colorIndex];
        }



    }

    void FixedUpdate()
    {
  
        
    }

    public void SetOrb(OrbType newTypeOrb) {
        TypeOfOrb = newTypeOrb;
        int colorIndex = (int)TypeOfOrb;
        GetComponent<SpriteRenderer>().color = colorsOfOrbTypes[colorIndex];

    }
    public OrbType GetOrb()
    {
        return TypeOfOrb;

    }





    void OnValidate()
    {
        isStarted = false;
        if (colorsOfOrbTypes.Length != Enum.GetNames(typeof(OrbType)).Length)
        {
            Debug.LogWarning("Don't change the 'colorsOfOrbTypes' field's array size!");
            Array.Resize(ref colorsOfOrbTypes, Enum.GetNames(typeof(OrbType)).Length);
        }
    }


}
