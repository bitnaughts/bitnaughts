using UnityEngine;
using System.Collections;

public struct ProjectileDataWrapper
{
    //hardpoint controls size (note that the size of shell requirement for each type of weapon (or just use more ammo for each shot, idk)

    public float Projectile_Speed;
    public float Projectile_Damage;

    public VisualDataWrapper Projectile_Visuals;

   // public int Projectile_FireRate;

    public ProjectileDataWrapper(float projectile_Speed, float projectile_Damage, VisualDataWrapper projectile_Visuals)//, //int projectile_FireRate)
    {
        Projectile_Speed = projectile_Speed;
        Projectile_Damage = projectile_Damage;
        Projectile_Visuals = projectile_Visuals;
       // Projectile_FireRate = projectile_FireRate;
    }


}
