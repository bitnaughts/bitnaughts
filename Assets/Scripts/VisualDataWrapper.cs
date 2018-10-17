using UnityEngine;
using System.Collections;

public struct VisualDataWrapper
{
	public Vector2 Transform_Scale;
    public Color Sprite_Color;




    public float ParticleSystem_Lifetime;
    public float ParticleSystem_Size;

    public VisualDataWrapper(Vector2 transform_Scale, Color sprite_Color, float particleSystem_Lifetime, float particleSystem_Size)
    {
        Transform_Scale = transform_Scale;
        Sprite_Color = sprite_Color;

        ParticleSystem_Lifetime = particleSystem_Lifetime;
        ParticleSystem_Size = particleSystem_Size;

    }
 //   float 


}


