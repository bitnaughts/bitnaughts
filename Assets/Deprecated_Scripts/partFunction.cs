using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class partFunction : MonoBehaviour
{
    public ItemObject item;

    public GameObject TESTER;

    private float leftAngle;
    bool clockwiseTurning = true;
    private float rightAngle;
    bool counterClockwiseTurning = true;
    //private int numberAngle;
    //Is the thing limited by degrees of rotation (Or is it free to move around in any direction
    private bool angleLimit = true;
    //Is the thing able to turn, move around (Or is it static, straight forward
    public bool turnable = true;
    //Is the thing able to shoot, fire objects
    public bool shooting = false;

    public int ammoAmount = 1000000;
    public bool ammo = false;
    //How fast does the object fire
    public int rateOfFire = 5;
    public float turnSpeed;
    //What object does the thing shoot
    public string firingType = "laser";
    public string weaponType = "";

    public GameObject target;
    Vector3 aimPoint;
    int targetIndex = -1;
    float cooldown;

    bool onTarget = false;

    Ship ship;

    //public ProjectieFireCharacteristicsDataWrapper ProjectileFireCharacteristics;
    bool firingClip;
    int current_shot;

    void Start()
    {
        ship = GetComponentInParent<Ship>();
        TESTER = GameObject.FindWithTag("EditorOnly");
        aimPoint = new Vector3(0, 0, 0);
        rateOfFire = rateOfFire * 1;
        angleLimit = this.GetComponentInParent<Hardpoint>().angleLimit;
        leftAngle = this.GetComponentInParent<Hardpoint>().leftAngle;
        rightAngle = this.GetComponentInParent<Hardpoint>().rightAngle;

        if (leftAngle == -1)
        {
            angleLimit = false;
            shooting = true;
            transform.Rotate(0, 0, this.GetComponentInParent<Hardpoint>().mainAngle - 90);
        }
    }

    void Update()
    {

        if (turnable) rotate();
        if (shooting && ammoAmount > 0)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                firingClip = true;
            }

            if (cooldown <= 0f)
            {
                cooldown = item.coefficients[Values.FIRERATE];
                shoot();
                ammoAmount--;
            }
            cooldown -= Time.deltaTime * 20;
        }
    }

    private void chanceReaim(float chance)
    {
        if (Random.value < chance)
        {
            chanceReaim();
        }
    }
    private void chanceReaim()
    {
        if (angleLimit && turnable)
        {
            float leftAngleTemp = leftAngle + (this.transform.rotation.eulerAngles.z - this.transform.localRotation.eulerAngles.z);
            if (leftAngleTemp < 0) leftAngleTemp += 360f;
            leftAngleTemp = leftAngleTemp % 360f;
            float rightAngleTemp = rightAngle + (this.transform.rotation.eulerAngles.z - this.transform.localRotation.eulerAngles.z);
            if (rightAngleTemp < 0) rightAngleTemp += 360f;
            rightAngleTemp = rightAngleTemp % 360f;
            target = testAiming.findTarget(this.transform.position, leftAngleTemp, rightAngleTemp, ship.team);
            if (leftAngle == -1f) target = testAiming.findTarget(this.transform.position, ship.team);
        }
        else target = testAiming.findTarget(this.transform.position, ship.team);
    }

    private void rotate()
    {
        Vector3 turretPoint = this.GetComponentInChildren<Transform>().position;
        Vector2 clickPoint;

        if (target != null)
        {
            /*Correction for leading targets*/
            float projectileSpeed = item.coefficients[Values.VELOCITY];//Values.getProjectileElements(firingType).Projectile_Speed * Values.getEquipmentElements(weaponType).Projectile_SpeedModifier ;// GetComponent<projectileFuntion>().speed;
            float distance = Mathf.Sqrt(Mathf.Pow(transform.position.x - target.transform.position.x, 2) + Mathf.Pow(transform.position.y - target.transform.position.y, 2));

            float time = distance / (projectileSpeed);
            float enemyShipSpeed = target.GetComponent<Ship>().currentSpeed;   //keep in mind this assumes the ships stay at a constant speed

            float enemyShipMovement = time * enemyShipSpeed;
            float enemyShipRotation = (target.GetComponentInParent<Transform>().rotation.eulerAngles.z + 90) % 360;

            float xModifier = Mathf.Cos(enemyShipRotation * Mathf.Deg2Rad) * enemyShipMovement;
            float yModifier = Mathf.Sin(enemyShipRotation * Mathf.Deg2Rad) * enemyShipMovement;

            clickPoint.x = target.transform.position.x + xModifier;
            clickPoint.y = target.transform.position.y + yModifier;

            // if (this.GetComponentInParent<Ship>().team == 0) GameObject.Find("display").GetComponent<Text>().text = ((int)angle + " " + (int)leftAngleTemp + " " + (int)rightAngleTemp);
            if (angleLimit)
            {
                /*Finding the angle from 0 to 360 between targets*/
                float angle = testAiming.findAngle(turretPoint, clickPoint);

                /*Correcting limits to relevant degrees*/
                float leftAngleTemp = leftAngle + (this.transform.rotation.eulerAngles.z - this.transform.localRotation.eulerAngles.z);
                if (leftAngleTemp < 0) leftAngleTemp += 360f;
                leftAngleTemp = leftAngleTemp % 360f;

                float rightAngleTemp = rightAngle + (this.transform.rotation.eulerAngles.z - this.transform.localRotation.eulerAngles.z);
                if (rightAngleTemp < 0) rightAngleTemp += 360f;
                rightAngleTemp = rightAngleTemp % 360f;

                /*In any case, if the angle is between limits, then move to aim there*/
                if ((rightAngleTemp > leftAngleTemp && ((angle > 0 && angle < leftAngleTemp) || (angle > rightAngleTemp)))
                || (angle > rightAngleTemp && angle < leftAngleTemp))
                {
                    transform.up = new Vector2(clickPoint.x - turretPoint.x, clickPoint.y - turretPoint.y);
                    shooting = true;
                    onTarget = true;

                }
                else { onTarget = false; shooting = false; chanceReaim(.1f); }
            }
            else
            {
                transform.up = new Vector2(clickPoint.x - turretPoint.x, clickPoint.y - turretPoint.y);
                shooting = true;
                onTarget = true;
            }

        }
        else
        {
            if (angleLimit) shooting = false;
            chanceReaim(.1f);
        }
    }


    private bool shootMechanics()
    {

        if (firingClip)
        {
            current_shot++;
          //  cooldown = item.coefficients[Values.FIRERATE];
            if (current_shot >= item.coefficients[Values.CLIPSIZE])
            {
                current_shot = 0;
                firingClip = false;
                cooldown = item.coefficients[Values.FIRERATE] * 10;
            }
            return true;
            /*
            ProjectileFireCharacteristics.clipShot++;
            if (ProjectileFireCharacteristics.Projectile_ClipSize > 0)
            {
                if (ProjectileFireCharacteristics.clipShot >= ProjectileFireCharacteristics.Projectile_ClipSize)
                { ProjectileFireCharacteristics.clipShot = 0;
                    ProjectileFireCharacteristics.clipCount++;
                    cooldown = ProjectileFireCharacteristics.Projectile_ClipReloadTime;
                    firingClip = false;
                    if (ProjectileFireCharacteristics.clipCount >= ProjectileFireCharacteristics.Projectile_ClipAmount) {
                        ProjectileFireCharacteristics.clipCount = 0;
                        cooldown = ProjectileFireCharacteristics.Projectile_ReloadTime;
                    }
                }
            }
            return true;*/
        }
        return false;
    }

    private void shoot()
    {
        if (target != null)
        {
            if (shootMechanics())
            {
                //  for (int i = 0; i < item.coefficients.Length; i++)
                // {
                //    print(i + " " + item.coefficients[i]);
                // }
                Vector3 rotation_value = this.transform.rotation.eulerAngles;
                rotation_value.z += item.coefficients[Values.ACCURACY] * (Random.value - .5f);
                ProjectileManager.getProjectile(item, target, this.transform.position, Quaternion.Euler(rotation_value), ship.team);
                //ProjectileManager.getPlayer(firingType, weaponType, this.transform.position);

                if (ship == null)
                {
                    Destroy(this.gameObject);
                    Destroy(this);
                }
            }
        }
        else
        {
            //firingClip = false;
        }


    }

}



//current = this.transform.rotation.eulerAngles.z + 90f;

//current += 360;
//current = current % 360f;
//  print(current + " " + leftAngleTemp + " " + rightAngleTemp);
// current += 360;
// leftAngleTemp += 360;
// rightAngleTemp += 360;


/*   else if (current > leftAngleTemp)
   {
       transform.rotation = Quaternion.Euler(0, 0, leftAngleTemp-90);
   }
   else if(current < rightAngleTemp)
   {
       transform.rotation = Quaternion.Euler(0, 0, rightAngleTemp - 90);
   }
  */

//Checking to see if it is at the edge of its range, disabling it if it is
/*  if (angleLimit)
  {
      counterClockwiseTurning = true;
      clockwiseTurning = true;
      shooting = true;
      //"5" is a buffer to insure it isn't referring to the other side
      if (current > leftAngleTemp && current < leftAngleTemp + 2.1f * Time.deltaTime * 100 * turnSpeed)
      {
          counterClockwiseTurning = false;
          shooting = false;
          chanceReaim();
      }
      if (current > rightAngleTemp && current < rightAngleTemp + 2.1f * Time.deltaTime * 100 * turnSpeed)
      {
          clockwiseTurning = false;
          shooting = false;
          chanceReaim();
      }
  }*/

/*	float reverseDirectionMultiplier = 1f;
    //Rotating if not close enough
    if (Mathf.Abs (current - angle) > 1f) 
    { 
        onTarget = false;
        //3f
        //Finding fastest way to desired angle
        if (Mathf.Abs (current - angle) > 180f && Mathf.Abs (current - angle) < 360f)
            reverseDirectionMultiplier = -1f;
        //Rotating in desired direction
        if (current > angle)
            reverseDirectionMultiplier *= -1f;
        //3
    //	if (!angleLimit)
    //		turnSpeed = .5f;//8f*Random.value;
        if ((reverseDirectionMultiplier > 0 && counterClockwiseTurning) || (reverseDirectionMultiplier < 0 && clockwiseTurning))
            transform.Rotate (0, 0, Time.deltaTime * 100 * turnSpeed * reverseDirectionMultiplier);	
    }*/
//else onTarget = true;