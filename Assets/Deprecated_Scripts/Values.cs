using UnityEngine;
using System.Collections;

public static class Values 
{
    public const short BARREL = 0;
    public const short ACTION = 1;
    public const short MUZZLE = 2;
    public const short GIMBAL = 3;
    public const short AIMING = 4;
    public const short MAGAZINE = 5;
    public const short AMMO = 6;

    public const short VELOCITY = 0;
    public const short FIRERATE = 1;
    public const short ACCURACY = 2;
    public const short ROTATION = 3;
    public const short CLIPSIZE = 4;
    public const short MASS = 5;

    public static float barrelSize(string size)
    {
        switch (size[0])
        {
            case 'M'://MICRO
                return 1f;
            case 'S'://SMALL
                return 1.5f;
            case 'N'://NORMAL
                return 2f;
            case 'L'://LARGE
                return 3f;
            case 'G'://GIANT
                return 5f;
        }
        return -1;
    }
    public static float[] modifyCoefficients(float[] coefficients, string type, int part)
    {
        int quality_modifier;
        if (part != AMMO) quality_modifier = int.Parse(type[1] + "");
        switch (part)
        {
            case BARREL:
                switch (type[0])
                {
                    case 'M'://MICRO
                        coefficients[VELOCITY] *= 1.625f;
                        coefficients[FIRERATE] *= .5f;
                        coefficients[ACCURACY] *= .5f;
                        coefficients[MASS] *= .025f;
                        break;
                    case 'S'://SMALL
                        coefficients[VELOCITY] *= 1.325f;
                        coefficients[FIRERATE] *= .625f;
                        coefficients[MASS] *= .06f;
                        break;
                    case 'N'://NORMAL
                        coefficients[VELOCITY] *= 1f;
                        coefficients[FIRERATE] *= 1f;
                        coefficients[MASS] *= 1;
                        break;
                    case 'L'://LARGE
                        coefficients[VELOCITY] *= .5f;
                        coefficients[FIRERATE] *= 3f;
                        coefficients[MASS] *= 15;
                        break;
                    case 'G'://GIANT
                        coefficients[VELOCITY] *= .325f;
                        coefficients[FIRERATE] *= 10f;
                        coefficients[MASS] *= 40;
                        break;
                }
                break;
            case ACTION:
                switch (type[0])
                {
                    case 'C':
                        coefficients[VELOCITY] *= .65f;
                        coefficients[FIRERATE] *= .5f;
                        coefficients[ACCURACY] *= .5f;
                        break;
                    case 'F':
                        coefficients[FIRERATE] *= .8f;
                        coefficients[ACCURACY] *= .75f;
                        break;
                    case 'S':
                        coefficients[VELOCITY] *= 1f;
                        coefficients[FIRERATE] *= 1f;
                        break;
                    case 'P':
                        coefficients[VELOCITY] *= 1.5f;
                        coefficients[FIRERATE] *= 2.375f;
                        coefficients[ACCURACY] *= 3f;
                        break;
                    case 'M':
                        coefficients[VELOCITY] *= 4f;
                        coefficients[FIRERATE] *= 5f;
                        coefficients[ACCURACY] *= 10f;
                        break;
                }
                break;
            case MUZZLE:
                switch (type[0])
                {
                    case 'C':
                        coefficients[VELOCITY] *= .825f;
                        coefficients[ACCURACY] *= 1.5f;
                        break;
                    case 'K'://compressor
                        coefficients[VELOCITY] *= 1.325f;
                        coefficients[FIRERATE] *= 1.25f;
                        break;
                    case 'S':
                        coefficients[VELOCITY] *= 1f;
                        coefficients[FIRERATE] *= 1f;
                        break;
                    case 'B':
                        coefficients[VELOCITY] *= 1.325f;
                        coefficients[ACCURACY] *= .825f;
                        break;
                    case 'V':
                        coefficients[VELOCITY] *= .8f;
                        coefficients[FIRERATE] *= .825f;
                        break;
                }
                break;
            case GIMBAL:
                switch (type[0])
                {
                    case 'F':                     
                        coefficients[ACCURACY] *= .8f;
                        coefficients[ROTATION] *= 3f;
                        break;
                    case 'R'://compressor
                        coefficients[VELOCITY] *= 1.325f;
                        coefficients[ACCURACY] *= .825f;
                        break;
                    case 'N':
                        coefficients[VELOCITY] *= 1f;
                        coefficients[FIRERATE] *= 1f;
                        break;
                    case 'S':
                        coefficients[ACCURACY] *= 1.625f;
                        coefficients[ROTATION] *= .5f;
                        break;
                    case 'D':
                        coefficients[VELOCITY] *= .8f;
                        coefficients[ACCURACY] *= 1.75f;
                        break;
                }
                break;
            case AIMING:
                switch (type[0])
                {
                    case 'N':
                        coefficients[ACCURACY] *= 1f;
                        coefficients[ROTATION] *= 1f;
                        break;
                    case 'M'://compressor
                        coefficients[VELOCITY] *= 1f;
                        coefficients[ACCURACY] *= 1f;
                        break;
                    case 'D':
                        coefficients[VELOCITY] *= 1f;
                        coefficients[FIRERATE] *= 1f;
                        break;
                    case 'S':
                        coefficients[ACCURACY] *= 1f;
                        coefficients[ROTATION] *= 1f;
                        break;
                    case 'P':
                        coefficients[VELOCITY] *= 1f;
                        coefficients[ACCURACY] *= 1f;
                        break;
                }
                break;
            case MAGAZINE:
                switch (type[0])
                {
                    case 'S':
                        coefficients[CLIPSIZE] = 1;
                        coefficients[VELOCITY] *= 3f;
                        coefficients[FIRERATE] *= 2f;
                        coefficients[ACCURACY] *= 2f;                        
                        break;
                    case 'C':
                        coefficients[CLIPSIZE] = 5;
                        coefficients[VELOCITY] *= 1.825f;
                        coefficients[FIRERATE] *= 1.25f;
                        coefficients[ACCURACY] *= 1.25f;
                        break;
                    case 'N':
                        coefficients[CLIPSIZE] = 20;
                        break;
                    case 'D':
                        coefficients[CLIPSIZE] = 50;
                        coefficients[VELOCITY] *= .8f;
                        coefficients[FIRERATE] *= .9f;
                        coefficients[ACCURACY] *= .875f;
                        break;
                    case 'B':
                        coefficients[CLIPSIZE] = 100;
                        coefficients[VELOCITY] *= .75f;
                        coefficients[FIRERATE] *= .8f;
                        coefficients[ACCURACY] *= 2f;
                        break;
                }
                break;
            case AMMO:
                switch (type[0])
                {
                    case 'C':
                        coefficients[VELOCITY] = 1f;
                        coefficients[FIRERATE] = 5f;
                        coefficients[ACCURACY] = 10f;
                        coefficients[MASS] = 5;
                        break;
                    case 'P'://compressor
                        coefficients[VELOCITY] = 1.5f;
                        coefficients[FIRERATE] = 4f;
                        coefficients[ACCURACY] = 1f;
                        coefficients[MASS] = 2;
                        break;
                    case 'L':
                        coefficients[VELOCITY] = 10f;
                        coefficients[FIRERATE] = 3f;
                        coefficients[ACCURACY] = 5f;
                        coefficients[MASS] = .01f;
                        break;
                }
                break;
        }
        return coefficients;
    }

    public static EquipmentDataWrapper getEquipmentElements(string type)
    {
        switch (type)
        {
            case "R1": return new EquipmentDataWrapper(1f, 2f, 1f, new ProjectileFireCharacteristicsDataWrapper(6, 5, 20, 5, 50));          //Gun, Small
            case "R2": return new EquipmentDataWrapper(1.25f, 2f, 1.5f, new ProjectileFireCharacteristicsDataWrapper(6, 5, 20, 5, 50));     //Rifle, Small
            case "R3": return new EquipmentDataWrapper(.625f, 2f, 1f, new ProjectileFireCharacteristicsDataWrapper(4, 25, 20, 5, 50));      //Gatling Gun, Small
            case "R4": return new EquipmentDataWrapper(.375f, 1f, .625f, new ProjectileFireCharacteristicsDataWrapper(1, 75, 20, 5, 50));   //Microgun, Small
            case "R5": return new EquipmentDataWrapper(.5f, 1.5f, .625f, new ProjectileFireCharacteristicsDataWrapper(1, 50, 20, 5, 50));   //Minigun, Small
            case "R6": return new EquipmentDataWrapper(2f, 2.25f, 2f, new ProjectileFireCharacteristicsDataWrapper(10, 1, 20, 5, 50));      //Railgun, Medium

            case "C1": return new EquipmentDataWrapper(3f, 2f, .5f, new ProjectileFireCharacteristicsDataWrapper(20));                      //Cannon, Small
            case "C2": return new EquipmentDataWrapper(3f, 2f, .75f, new ProjectileFireCharacteristicsDataWrapper(10, 5, 30, 3, 100));      //Autocannon, Small
            case "C3": return new EquipmentDataWrapper(4f, 4f, .375f, new ProjectileFireCharacteristicsDataWrapper(30));                    //Mortar, Medium
            case "C4": return new EquipmentDataWrapper(4f, 4f, .5f, new ProjectileFireCharacteristicsDataWrapper(40));                      //Howizter, Large
            case "C5": return new EquipmentDataWrapper(4f, 4f, .5f, new ProjectileFireCharacteristicsDataWrapper(20, 2, 20, 1, 50));
            case "C6": return new EquipmentDataWrapper(5f, 3f, .625f, new ProjectileFireCharacteristicsDataWrapper(50));                    //Artillery, Large
            case "C7": return new EquipmentDataWrapper(5f, 3f, .625f, new ProjectileFireCharacteristicsDataWrapper(25, 2, 20, 1, 50));

            case "O1": return new EquipmentDataWrapper(2f, 3f, .1f, new ProjectileFireCharacteristicsDataWrapper(5, 5, 20, 1, 20));         //Rocket, Medium
            case "O2": return new EquipmentDataWrapper(2f, 1.5f, .2f, new ProjectileFireCharacteristicsDataWrapper(1, 10, 30));      //Hailfire Rocket, Small
            case "O3": return new EquipmentDataWrapper(3f, 3f, .15f, new ProjectileFireCharacteristicsDataWrapper(10, 1, 20, 5, 50));       //Unguided Torpedo, Large

            case "M1": return new EquipmentDataWrapper(2f, 3f, .1f, new ProjectileFireCharacteristicsDataWrapper(5, 5, 20, 1, 20));         //Missile, Medium
            case "M2": return new EquipmentDataWrapper(2f, 3f, .1f, new ProjectileFireCharacteristicsDataWrapper(1, 10, 30));        //Hailfire Missile, Small
            case "M3": return new EquipmentDataWrapper(3f, 4f, .1f, new ProjectileFireCharacteristicsDataWrapper(1, 1, 20, 5, 50));         //Torpedo, Large

        }
        return new EquipmentDataWrapper(1f, 1f, 1f, new ProjectileFireCharacteristicsDataWrapper(1, 10, 4, 5, 10));

    }
    public static ProjectileDataWrapper getProjectileElements(string type)
	{
		switch (type)
		{
            case "C1": return new ProjectileDataWrapper(.75f, 1f, new VisualDataWrapper(new Vector2(2, 5), new Color(255, 255, 255), .05f, .2f));
            case "C2": return new ProjectileDataWrapper(.825f, 1.25f, new VisualDataWrapper(new Vector2(2, 5), new Color(255, 255, 255), .05f, .2f));
            case "C3": return new ProjectileDataWrapper(.825f, 1.5f, new VisualDataWrapper(new Vector2(2, 5), new Color(255, 255, 255), .05f, .2f));

            case "P1": return new ProjectileDataWrapper(1f, 1.25f, new VisualDataWrapper(new Vector2(2, 5), new Color(0, 255, 170), .05f, .35f));
            case "P2": return new ProjectileDataWrapper(1.125f, 1.5f, new VisualDataWrapper(new Vector2(2, 5), new Color(255, 170, 0), .05f, .375f));
            case "P3": return new ProjectileDataWrapper(1.25f, 1.75f, new VisualDataWrapper(new Vector2(2, 5), new Color(170, 0, 255), .05f, .4f));

            case "L1": return new ProjectileDataWrapper(3f, .5f, new VisualDataWrapper(new Vector2(1, 5), new Color(255, 0, 0), .05f, .2f));
            case "L2": return new ProjectileDataWrapper(3f, .75f, new VisualDataWrapper(new Vector2(1, 5), new Color(255, 0, 0), .05f, .2f));
            case "L3": return new ProjectileDataWrapper(3f, 2f, new VisualDataWrapper(new Vector2(1, 5), new Color(255, 0, 0), .05f, .2f));
        }
        return new ProjectileDataWrapper(.75f, 1, new VisualDataWrapper(new Vector2(5, 5), new Color(255, 255, 255), .05f, .2f));
    }
    public static string getProjectileName(string type)
    {
        switch (type)  //warheads, flak,
        {
            case "C1": return "Conventional";   //Flak warheads
            case "C2": return "Kinetic";
            case "C3": return "Nuclear";

            case "P1": return "Plasma";         //Disabler warhead (turns missiles into rockets)? or destroys? tiers?
            case "P2": return "Ion";
            case "P3": return "Muon";
            case "P4": return "Graviton";
            case "P5": return "Chronoton";

            case "L1": return "Photon";         //skattergram (more flak-like?) 
            case "L2": return "Phonon";
            case "LT": return "Infrared";
            case "LX": return "Thermic";
            case "L3": return "Gamma";
        }
        return "Error";
    }
	public static float getRotationSpeed (string type)
	{
		switch (type)
		{
			case "small": return 2f;
			case "medium": return 1f;
			case "large": return .5f;
		}
		return 0;
	}
	public static VisualDataWrapper getVisualElements(string type)
	{
		switch (type)
		{
            /*shortName = name;
        switch (name)
        {
            case "R1": this.name = "Gun"; description = "A cheap, quick modification made to standard land-based weaponary, making it suitable for space warfare"; break;                           
            case "R2": this.name = "Rifle"; description = "An improvement on contemporary smooth-bored designs, giving the shells in-flight gyroscopic stability"; break;                        
            case "R3": this.name = "GatlingGun"; description = "A gun mounted with three rotating barrels, putting out a more constant stream of shells"; break;                    //GattlingGun: 
            case "R4": this.name = "Microgun"; description = "A small-caliber gun mounted with ten rotating barrels, allowing for overclocked firing rates, putting out a storm of small shells"; break;
            case "R5": this.name = "Minigun"; description = "A gun mounted with five rotating barrels, putting out a reliable, deadly wall of shells"; break;                        //Minigun: 
            case "R6": this.name = "Railgun"; description = " An electromagnetic propulsion system accelerates its projectile to break-neck speeds, causing increased stopping power"; break;                        //Railgun:

            case "C1": this.name = "Cannon"; description = "A simple mounting of a land-based defensive piece, shooting out large shells at low velocities"; break;                         //Cannon: A simple mounting of a land-based defensive piece, shooting out large shells at low velocities
            case "C2": this.name = "Autocannon"; description = "A modified cannon with improved chambering mechanisms, allowing for vastly improved firing rates"; break;                     //Autocannon: A modified cannon with improved chambering mechanisms, allowing for vastly improved firing rates
            case "C3": this.name = "Mortar"; description = "A wide-diameter, inaccurate firing system, allowing for specialization in the rounds being shot"; break;                         //Mortar: A wide-diameter, inaccurate firing system, allowing for specialization in the rounds being shot           (Mortar allows for mines and flak, same with howizter)
            case "C4": this.name = "Howizter"; description = "An oversized cannon, shooting massive shells at low velocities, but with massive amounts of inertia"; break;                       //Howizter: An oversized cannon, shooting massive shells at low velocities, but with massive amounts of inertia
//            case "C5": this.name = "HowizterBattery"; description = "A set of two modified howizters, effectively doubling the fire rate"; break;                //Battery: A set of two modified howizters, effectively doubling the fire rate
            case "C5": this.name = "Artillery"; description = "A rifled cannon, shooting shells at high velocities with improved accuracy"; break;                      //Artillery: A rifled cannon, shooting shells at high velocities with improved accuracy
//           case "C7": this.name = "ArtilleryBattery"; description = "A set of two modified artillery pieces, effectively doubling the fire rate"; break;               //Battery: A set of two modified artillery pieces, effectively doubling the fire rate

            case "O1": this.name = "RocketLauncher"; description = "A dumbfire self-propelled explosive, dealing huge damage but with relative inaccuracy"; break;                 //Rocket: A dumbfire self-propelled explosive, dealing huge damage but with relative inaccuracy
//            case "O2": this.name = "RocketLauncherArray"; description = "An array of four rocket tubes, quadrupling the damage alpha in four-shot clips"; break;            //RocketArray: 
            case "O2": this.name = "HailfireRocketLauncher"; description = "A multitude of miniature rocket tubes, spraying many small rockets in the general direction of the enemy"; break;    //HailfireRocket: 
            case "O3": this.name = "ClusterRocketLauncher"; description = "A self-propelled unguided capsule, holding ten smaller rockets that deploy as they near their target"; break;          //ClusterRocket: 

            case "M1": this.name = "MissileLauncher"; description = ""; break;

                //	case "T1": name += "Torpedo"; break;

            case "M2": this.name = "HailfireRocketLauncher"; description = ""; break;
            case "M3": this.name = "MissileLauncher"; description = ""; break;
            case "M4": this.name = "TorpedoTube"; description = ""; break;                //	case "T2": name += "PulsedTorpedo"; break;
                //	case "T3": name += "BipropellantTorpedo"; break;
                //	case "T4": name += "TripropellantTorpedo"; break;


        }
        /*switch (name.Substring(0,2))
		{
			case "C1": name = "Conventional"; break;
			case "C2": name = "Kinetic"; break;
			case "C3": name = "Nuclear"; break;

			case "P1": name = "Neutron"; break;
			case "P2": name = "Ion"; break;
			case "P3": name = "Plasma"; break;
            
			case "L1": name = "Cathod"; break;
			case "L2": name = "Photon"; break;
			case "L3": name = "Gamma"; break;
		}
		switch (name.Substring(2,2))
		{
			case "R1": if (name.Substring(0,1) != "L") name += "Gun"; else name += "Emitter"; break;
			case "R2": if (name.Substring(0,1) != "L") name += "Rifle"; else name += "Blaster";break;
			case "R3": if (name.Substring(0,1) != "L") name += "Railgun"; else name += "Disruptor"; break;
			case "R4": name += "Minigun"; break;

			case "C1": name += "Cannon"; break;
			case "C2": name += "Autocannon"; break;
			case "C3": name += "Howizter"; break;
			case "C4": name += "Battery"; break;

			case "O1": name += "Rocket"; break;
			case "O2": name += "PulsedRocket"; break;
			case "O3": name += "BipropellantRocket"; break;
			case "O4": name += "TripropellantRocket"; break;

			case "M1": name += "Missile"; break;
			case "M2": name += "PulsedMissile"; break;
			case "M3": name += "BipropellantMissile"; break;
			case "M4": name += "TripropellantMissile"; break;

		//	case "T1": name += "Torpedo"; break;
		//	case "T2": name += "PulsedTorpedo"; break;
		//	case "T3": name += "BipropellantTorpedo"; break;
		//	case "T4": name += "TripropellantTorpedo"; break;
		}*/
            //CONVENTIONAL RIFLE TYPES
            case "C1R1": return new VisualDataWrapper(new Vector2(5, 5), new Color(255, 255, 255), .05f, .2f); //Conventional Gun: A cheap, quick modification made to standard land-based weaponary, making it suitable for space warfare
			case "C1R2": return new VisualDataWrapper(new Vector2(4, 4), new Color(255, 255, 255), .05f, .2f); //Conventional Rifle: An improvement on the smooth-bored designs before it, giving the shells in-flight gyroscopic stability
            case "C1R3": return new VisualDataWrapper(new Vector2(3, 3), new Color(255, 255, 255), .05f, .2f); //Convemtional Railgun: An electromagnetically-propelled shell accelerates to break-neck speeds, and causes increased stopping power
            case "C1R4": return new VisualDataWrapper(new Vector2(2, 2), new Color(255, 255, 255), .05f, .2f); //Conventional Minigun: A gun mounted with rotating barrels, allowing for overclocked firing rates, putting out a deadly storm of shells

            case "C2R1": return new VisualDataWrapper(new Vector2(5, 5), new Color(255, 255, 255), .05f, .2f); //Kinetic Gun: A smooth-bored gun, where standard lead rounds are replaced with heavy alloy slugs for increased speed and stopping power
            case "C2R2": return new VisualDataWrapper(new Vector2(4, 4), new Color(255, 255, 255), .05f, .2f); //Kinetic Rifle: An improved rifling technique for heavier slugs allows them to be shot with more power
            case "C2R3": return new VisualDataWrapper(new Vector2(3, 3), new Color(255, 255, 255), .05f, .2f); //Kinetic Railgun: A utilitization of electromagnets to more efficiently propel heavy slugs at even faster rates
            case "C2R4": return new VisualDataWrapper(new Vector2(2, 2), new Color(255, 255, 255), .05f, .2f); //Kinetic Minigun: A gun with rotating barrels whose main purpose is to put out large amounts of debris in the form of deadly slugs

            case "C3R1": return new VisualDataWrapper(new Vector2(6, 6), new Color(255, 255, 255), .05f, .2f); //Nuclear Gun: A smooth-bored gun, firing small nuclear-primed tips, causes violent nuclear fusion reactions upon detonation
            case "C3R2": return new VisualDataWrapper(new Vector2(5, 5), new Color(255, 255, 255), .05f, .2f); //Nuclear Rifle: A rifled barrel allows for nuclear-primed shells to be fired with more power and stability

            //CONVENTIONAL CANNON TYPES
            case "C1C1": return new VisualDataWrapper(new Vector2(10, 10), new Color(255, 255, 255), .05f, .2f); //Conventional Cannon: A simple mounting of a land-based defensive piece, shooting out large shells at low velocities
            case "C1C2": return new VisualDataWrapper(new Vector2(10, 10), new Color(255, 255, 255), .05f, .2f); //Conventional Autocannon: A modified cannon with improved chambering mechanism allows for vastly improved firing rates
            case "C1C3": return new VisualDataWrapper(new Vector2(14, 14), new Color(255, 255, 255), .05f, .2f); //Conventional Howizter: An oversized cannon, shooting massive shells at low velocities, but with massive amounts of inertia
            case "C1C4": return new VisualDataWrapper(new Vector2(14, 14), new Color(255, 255, 255), .05f, .2f); //Conventional Battery: A set of two modified howizters, effectively doubling the fire rate

            case "C2C1": return new VisualDataWrapper(new Vector2(10, 10), new Color(255, 255, 255), .05f, .2f); //Kinetic Cannon: A standard cannon, but able to utilize improved 
            case "C2C2": return new VisualDataWrapper(new Vector2(10, 10), new Color(255, 255, 255), .05f, .2f); //Kinetic Autocannon: A modified cannon with improved chambering mechanism allows for vastly improved firing rates
            case "C2C3": return new VisualDataWrapper(new Vector2(14, 14), new Color(255, 255, 255), .05f, .2f); //Kinetic Howizter: An oversized cannon, shooting massive shells at low velocities, but with massive amounts of inertia
            case "C2C4": return new VisualDataWrapper(new Vector2(14, 14), new Color(255, 255, 255), .05f, .2f); //Kinetic Battery: A set of two modified howizters, effectively doubling the fire rate

            case "C3C1": return new VisualDataWrapper();
			case "C3C2": return new VisualDataWrapper();
			case "C3C3": return new VisualDataWrapper();
			case "C3C4": return new VisualDataWrapper();

			//CONVENTIONAL ROCKET TYPES
			case "C1O1": return new VisualDataWrapper();
			case "C1O2": return new VisualDataWrapper();
			case "C1O3": return new VisualDataWrapper();
			case "C1O4": return new VisualDataWrapper();

			case "C2O1": return new VisualDataWrapper();
			case "C2O2": return new VisualDataWrapper();
			case "C2O3": return new VisualDataWrapper();
			case "C2O4": return new VisualDataWrapper();

			case "C3O1": return new VisualDataWrapper();
			case "C3O2": return new VisualDataWrapper();
			case "C3O3": return new VisualDataWrapper();
			case "C3O4": return new VisualDataWrapper();


			//CONVENTIONAL MISSILE TYPES
			case "C1M1": return new VisualDataWrapper();
			case "C1M2": return new VisualDataWrapper();
			case "C1M3": return new VisualDataWrapper();
			case "C1M4": return new VisualDataWrapper();

			case "C2M1": return new VisualDataWrapper();
			case "C2M2": return new VisualDataWrapper();
			case "C2M3": return new VisualDataWrapper();
			case "C2M4": return new VisualDataWrapper();

			case "C3M1": return new VisualDataWrapper();
			case "C3M2": return new VisualDataWrapper();
			case "C3M3": return new VisualDataWrapper();
			case "C3M4": return new VisualDataWrapper();

			////

			//PLASMA RIFLE TYPES
			case "P1R1": return new VisualDataWrapper();
			case "P1R2": return new VisualDataWrapper();
			case "P1R4": return new VisualDataWrapper();

			case "P2R1": return new VisualDataWrapper();
			case "P2R2": return new VisualDataWrapper();
			case "P2R4": return new VisualDataWrapper();

			case "P3R1": return new VisualDataWrapper();
			case "P3R2": return new VisualDataWrapper();
			case "P3R4": return new VisualDataWrapper();

			//PLASMA CANNON TYPES
			case "P1C1": return new VisualDataWrapper();
			case "P1C2": return new VisualDataWrapper();
			case "P1C3": return new VisualDataWrapper();
			case "P1C4": return new VisualDataWrapper();

			case "P2C1": return new VisualDataWrapper();
			case "P2C2": return new VisualDataWrapper();
			case "P2C3": return new VisualDataWrapper();
			case "P2C4": return new VisualDataWrapper();

			case "P3C1": return new VisualDataWrapper();
			case "P3C2": return new VisualDataWrapper();
			case "P3C3": return new VisualDataWrapper();
			case "P3C4": return new VisualDataWrapper();

			//PLASMA ROCKET TYPES
			case "P1O1": return new VisualDataWrapper();
			case "P1O2": return new VisualDataWrapper();
			case "P1O3": return new VisualDataWrapper();
			case "P1O4": return new VisualDataWrapper();

			case "P2O1": return new VisualDataWrapper();
			case "P2O2": return new VisualDataWrapper();
			case "P2O3": return new VisualDataWrapper();
			case "P2O4": return new VisualDataWrapper();

			case "P3O1": return new VisualDataWrapper();
			case "P3O2": return new VisualDataWrapper();
			case "P3O3": return new VisualDataWrapper();
			case "P3O4": return new VisualDataWrapper();

			//PLASMA MISSILE TYPES
			case "P1M1": return new VisualDataWrapper();
			case "P1M2": return new VisualDataWrapper();
			case "P1M3": return new VisualDataWrapper();
			case "P1M4": return new VisualDataWrapper();

			case "P2M1": return new VisualDataWrapper();
			case "P2M2": return new VisualDataWrapper();
			case "P2M3": return new VisualDataWrapper();
			case "P2M4": return new VisualDataWrapper();

			case "P3M1": return new VisualDataWrapper();
			case "P3M2": return new VisualDataWrapper();
			case "P3M3": return new VisualDataWrapper();
			case "P3M4": return new VisualDataWrapper();

			////

			//LASER RIFLE TYPES
			case "L1R1": return new VisualDataWrapper();
			case "L1R2": return new VisualDataWrapper();

			case "L2R1": return new VisualDataWrapper();
			case "L2R2": return new VisualDataWrapper();

			case "L3R1": return new VisualDataWrapper();
			case "L3R2": return new VisualDataWrapper();

			//LASER CANNON TYPES
			case "L1C1": return new VisualDataWrapper();
			case "L1C2": return new VisualDataWrapper();
			case "L1C3": return new VisualDataWrapper();
			case "L1C4": return new VisualDataWrapper();

			case "L2C1": return new VisualDataWrapper();
			case "L2C2": return new VisualDataWrapper();
			case "L2C3": return new VisualDataWrapper();
			case "L2C4": return new VisualDataWrapper();

			case "L3C1": return new VisualDataWrapper();
			case "L3C2": return new VisualDataWrapper();
			case "L3C3": return new VisualDataWrapper();
			case "L3C4": return new VisualDataWrapper();
		}
		return new VisualDataWrapper();
	}
    public static string[] camelCaseSplitter(string input)
    {
        int index = 0;
        for (int i = 2; i < input.Length; i++)
        {
            int charValue = (int)input.Substring(i, 1).ToCharArray()[0];
            if (charValue >= 65 && charValue <= 90)
            {
                index = i;
            }
        }
            return new string[2] { input.Substring(0, index), input.Substring(index) };
    }
    public static string camelCaseParser(string input)
    {
        string output = input;
        for (int i = 2; i < output.Length; i++)
        {
            int charValue = (int)output.Substring(i, 1).ToCharArray()[0];
            if (charValue >= 65 && charValue <= 90)
            {
                output = output.Substring(0, i) + " " + output.Substring(i);
                i++;
           }
        }
        return output + "   ";
    }
    public static bool isWithinBounds(Vector2 position, Vector2 other_position, float bounds)
    {
       // bounds = Mathf.Sqrt(bounds);
        float distance = Mathf.Sqrt(Mathf.Pow(position.x - other_position.x, 2) + Mathf.Pow(position.y - other_position.y, 2));
        if (distance < bounds)
            return true;
        return false;
        //   if (Mathf.Abs(position.x - other_position.x) < bounds.x)
        //   {
        //       if (Mathf.Abs(position.y - other_position.y) < bounds.y)
        //       {
        ////           return true;
        //       }
        //  }
        //  return false;
    }
    public static float distanceBetweenObjects(Vector2 pos1, Vector2 pos2)
    {
        return Mathf.Sqrt(Mathf.Pow(pos1.x - pos2.x, 2) + Mathf.Pow(pos1.y - pos2.y, 2));
    }
    public static bool IsPointInPolygon4(Vector2[] polygon, Vector2 testPoint)
    {
        bool result = false;
        int j = polygon.Length - 1;
        for (int i = 0; i < polygon.Length; i++)
        {
            if (polygon[i].y < testPoint.y && polygon[j].y >= testPoint.y || polygon[j].y < testPoint.y && polygon[i].y >= testPoint.y)
            {
                if (polygon[i].x + (testPoint.y - polygon[i].y) / (polygon[j].y - polygon[i].y) * (polygon[j].x - polygon[i].x) < testPoint.x)
                {
                    result = !result;
                }
            }
            j = i;
        }
        return result;
    }
}
