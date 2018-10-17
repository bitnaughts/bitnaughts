

public struct EquipmentDataWrapper
{
    public float Projectile_DamageModifier;
    public float Projectile_SizeModifier;
    public float Projectile_SpeedModifier;

    public ProjectileFireCharacteristicsDataWrapper Projectile_FireCharacteristics;
    //public string Projectile_FireCharacteristics;   //BURST:CLIPSIZE-CLIPFIRESPEED,CLIPAMOUNT-CLIPRELOADSPEED,RELOADTIME
    


    // public int Projectile_FireRate;

    public EquipmentDataWrapper(float projectile_DamageModifier, float projectile_SizeModifier, float projectile_SpeedModifier, ProjectileFireCharacteristicsDataWrapper projectile_FireCharacteristics)//, VisualDataWrapper projectile_Visuals)//, //int projectile_FireRate)
    {
        Projectile_DamageModifier = projectile_DamageModifier;
        Projectile_SizeModifier = projectile_SizeModifier;
        Projectile_SpeedModifier = projectile_SpeedModifier;
        Projectile_FireCharacteristics = projectile_FireCharacteristics;
        // Projectile_FireRate = projectile_FireRate;
    }


}
