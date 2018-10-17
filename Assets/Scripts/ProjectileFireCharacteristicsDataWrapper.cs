

public class ProjectileFireCharacteristicsDataWrapper
{
    public int Projectile_FireRate;
    public int Projectile_ClipSize;
    public int Projectile_ClipReloadTime;
    public int Projectile_ClipAmount;
    public int Projectile_ReloadTime;

    public int clipShot;
    public int clipCount;

    public ProjectileFireCharacteristicsDataWrapper (int projectile_FireRate, int projectile_ClipSize, int projectile_ClipReloadTime, int projectile_ClipAmount, int projectile_ReloadTime)
    {
        Projectile_FireRate = projectile_FireRate;
        Projectile_ClipSize = projectile_ClipSize;
        Projectile_ClipReloadTime = projectile_ClipReloadTime;
        Projectile_ClipAmount = projectile_ClipAmount;
        Projectile_ReloadTime = projectile_ReloadTime;
    }
    public ProjectileFireCharacteristicsDataWrapper(int projectile_FireRate)
    {
        Projectile_FireRate = projectile_FireRate;
        Projectile_ClipSize = 0;
        Projectile_ClipReloadTime = 0;
        Projectile_ClipAmount = 0;
        Projectile_ReloadTime = 0;
    }

    public ProjectileFireCharacteristicsDataWrapper(int projectile_FireRate, int projectile_ClipSize, int projectile_ReloadTime)
    {
        Projectile_FireRate = projectile_FireRate;
        Projectile_ClipSize = projectile_ClipSize;
        Projectile_ClipReloadTime = 1;
        Projectile_ClipAmount = 1;
        Projectile_ReloadTime = projectile_ReloadTime;
    }

}
