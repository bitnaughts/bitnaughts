using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime;
using System.Collections.Generic;
//using System;
using UnityEngine.UI;
public class FleetManager : MonoBehaviour
{
    public Shader shader;
    public Texture texture;

    void Update()
    {
      //  Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
     //   GameObject.Find("Text").GetComponent<Text>().text = "over" + (TeamManager.team_lists[0][0].GetComponent<BoxCollider2D>().bounds.Contains(pos));
    }
    //
    //public Transform friendFleet;
    //public Transform otherFleet;

    //public static GameObject[,] shipTeams = new GameObject[10, 100];

    public GameObject holder;
    public FleetData ships;
    //GameObject holder = new GameObject();
    string[] items;
    /*
	 * 	LOAD player fleet from file
	 * 	GENERATE others via method
	 * 
	 */

 //   public static GameObject[] getShipsInTeam(int team)
 //   {
  //      GameObject[] temp = new GameObject[100];
 //       for (int i = 0; i < 100; i++) temp[i] = shipTeams[team, i];
  //      //print (team + " " + i + " " +temp[i] + " " + temp[i].GetComponent<SpriteRenderer>().sprite );
//
 //       return temp;
//
 //   }

    void Start()
    {
        //Ship Modules
        //          Crew
        //          Life Support
        //          Engines
        //          Cabins
        //          Cargo Bay
        //          Communications System
        //          Reactor

        //Damage = velocity * mass * energy coefficient
        //          Conventional: low velocity, high mass, no energy
        //          Plasma: medium velocity, low mass, medium energy
        //          Laser: high velocity, basically no mass, high energy

        //Gun: 
        // Ammunition: damage type
        // Barrel: caliber
        // Receiver: firing modes, modifies velocity and accuracy
        // Muzzles: modifies velocity and accuracy
        // Aiming System: type of movement by gimbal
        // Gimbal: Rotation speed, velocity, accuracy
        // Magazine: Ammo amounts

        //{Barrel,Action,Muzzle,AimingType,Gimbal,Magazine,Ammunition}

        //Modifier types:
        //Muzzles: 
        //          Standard: N/A
        //          Compensator: increases accuracy, decreases velocity 
        //          Booster: increase velocity, decreases accuracy
        //          Compressor: increase velocity, decrease fire rate
        //          Vented: decrease velocity, increase fire rate
        //Action:
        //          Standard: N/A
        //          Fast-action: increased fire-rate, decreased accuracy
        //          Cyclic: increases fire rate, decreases accuracy, velocity
        //          Precise: increased accuracy, velocity, decreased fire-rate  
        //          Manual: adds button to click to fire, increased accuracy, velocity 
        //Barrels:
        //          Normal-standard: N/A
        //          Small: increased velocity, decreased damage, chambering speed fast
        //          Micro: increased velocity, decreased damage, chambering speed very fast
        //          Large: increased damage, decreased velocity, chambering speed slow
        //          Giant: increased damage, decreased velocity, chambering speed very slow
        //          Gatling Barrels for all sizes (large,giant are duel barreled)
        //Aiming System:
        //          None: shoots straight forward
        //          Manual: use mouse, target point to aim
        //          Dumblock: aims directly at a target, no compensation for leading
        //          Sprayer: sprays across target's left and right extremes to ensure at least some hits
        //          Predictive: adjusts for leading, autoaim
        //Targetting System:
        //          (comes with aiming system, for dumblock and predictive aiming systems)
        //          Large>Small, Small>Large, Closest>Furthest settings
        //Gimbal:
        //          Standard: N/A
        //          (Gimbal size required to match barrel size)
        //          Slow: increased accuracy, slow turn rate
        //          Fast: decreased accuracy, fast turn rate
        //          Dampening: increased accuracy, decreased velocity
        //          Rigid: decreased accuracy, increased velocity          
        //Ammunition:
        //          Conventional
        //          Conventional Rocket
        //          Plasma (Capsule, raw?)
        //          Plasma Rocket
        //          Laser
        //Magazine:
        //          Standard: 20 shot
        //          Clip: 5 shot
        //          Single shot: 1 shot
        //          Drum: 50 shot
        //          Belt-fed: (either add overheating or 200)
        //Rocket Aiming System-- same as aiming system of gun
        //Rocket Propulsion:
        //          Monopropellant:
        //          Bipropellant:
        //          Tripropellant:
        //          Stages (1,2)         
        //Warhead:
        //          Flak (all conventional weapons
        //          Clusterbomb (conventional missiles
        //          Discharger (all plasma?
        //          Arcing (all laser?

        //Barrel type, clip size, capacitor/receiver, muzzles,gimbals, gases/types of ammo, 
        // ABC - Type of weapon, 

        /*
		
		List of string ships

		
		
		(type):(teamNumber):(hardpoints) -- needs cargo, other attached items, internals

		Make all ships in (items), assign tags based on team numbers, group into parents by team numbers
S
		fighting -- which teams you are against/for/neutral


		//other attached items: drone bays, shield emitters, point defense, mining equipment, scanner drone, harvester drones, tractor drones



			Conventional
		-Kinetic
		-

			Rifle General Types
		-Gun
		-Rifle

	
		-Minigun (fast, weaker)?
		-Railgun

		-Point-defense
		-C-RAM
		

			Cannon
		-Cannon
		-Howizter (slow,larger)? 
		-Autocannon (minigun equivalent) ?
		-Artillery
		-Battery
		
			Plasma
		-Ion    
		-Proton 
		-Plasma 		

			Laser
		-Photon 
		-Cathod (electron) - shoots out a stream of electrons, finding the closest thing to ground to, and imparting large amounts of electrical energy to short out the ship's shields and systems
		-Gamma
			
			Laser Rifles
		-Emitter
		-Blaster
		-Disruptor
		
			Missile
		-Missile
		-Torpedo
		-Chaf

			Rocket
		-Charge
		-Bomb
		-Rocket
		-Hailfire Rocket (minigun equivalent) ?



		*/
        string data = "fighter:0:{R1:C1-1000}&{}{}{}";
        string items2 = data.Substring(data.IndexOf(":") + 3); //== {R1:C1:100}:{}{}{}*{C1:100}"

        string hardpoints = items2.Substring(0, items2.IndexOf("&")); //== {R1}

        //string modules = items.Substring(items.IndexOf(":"), items.IndexOf("*"));

        // string ammo = items.Substring(items.IndexOf("*"));


        //"fighter:0:{R1:C1-1000}:{}{}{}"
        string[] items = hardpoints.Split('}');
        for (int i = 0; i < items.Length - 1; i++)
        {
            print(items[i]);
            print(items[i].Substring(1, 2)); //R1
            print(items[i].Substring(items[i].IndexOf(":") + 1, 2)); //R1
            print(int.Parse(items[i].Substring(items[i].IndexOf("-") + 1)));
        }

        //FLAGSHIP VERSUS FIGHTERS
        //{ "flagship:0:{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}&{}{}{}", "fighter:1:{M1:L1-1000}&{}{}{}", "fighter:1:{R1:L1-1000}&{}{}{}", "fighter:1:{M1:L1-1000}&{}{}{}", "fighter:1:{R1:L1-1000}&{}{}{}", "fighter:1:{M1:L1-1000}&{}{}{}", "fighter:1:{R1:L1-1000}&{}{}{}", "fighter:1:{M1:L1-1000}&{}{}{}", "fighter:1:{R1:L1-1000}&{}{}{}", "fighter:1:{R1:L1-1000}&{}{}{}", "fighter:1:{R1:L1-1000}&{}{}{}" }; //"flagship:0:{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}&{}{}{}", "flagship:1:{R4:C1-1000}{R4:C1-1000}{R4:C1-1000}{R4:C1-1000}{R4:C1-1000}{R4:C1-1000}{R4:C1-1000}{R4:C1-1000}{R4:C1-1000}{R4:C1-1000}{R4:C1-1000}{R4:C1-1000}{R4:C1-1000}&{}{}{}" }; 

        //ship:team:{weapon1, boosts},{...},{...}:{module1},{...},{...}:{ammo1},{...},{...}
        //fighter:0:{R1,B}"flagship:0:{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}&{}{}{}",
        items = new string[] { "assaulter:0:{G5,C5,K5,C5,N5,B3,C}{M5,C5,V5,C5,N5,B3,C}&{}", "freighter:1:{M5,C5,V5,P5,N5,D3,C}&{}" };//, "freighter:1:{M5,C5,V5,P5,N5,D3,C}&{}", "freighter:1:{M5,C5,V5,P5,N5,D3,C}&{}", "freighter:1:{M5,C5,V5,P5,N5,D3,C}&{}", "freighter:1:{M5,C5,V5,P5,N5,D3,C}&{}", "freighter:1:{M5,C5,V5,P5,N5,D3,C}&{}", "freighter:1:{M5,C5,V5,P5,N5,D3,C}&{}" }; //"flagship:0:{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}&{}{}{}", "flagship:1:{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}&{}{}{}", "flagship:1:{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}{R5:C1-1000}&{}{}{}", "fighter:1:{M1:L1-1000}&{}{}{}", "fighter:1:{R1:L1-1000}&{}{}{}", "fighter:1:{M1:L1-1000}&{}{}{}", "fighter:1:{R1:L1-1000}&{}{}{}", "fighter:1:{M1:L1-1000}&{}{}{}", "fighter:1:{R1:L1-1000}&{}{}{}", "fighter:1:{M1:L1-1000}&{}{}{}", "fighter:1:{R1:L1-1000}&{}{}{}", "fighter:1:{R1:L1-1000}&{}{}{}", "bomber:1:{M3:C1-1000}&{}{}{}", "bomber:1:{M3:C1-1000}&{}{}{}", "bomber:1:{M3:C1-1000}&{}{}{}" };//, "flagship:0:{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}{R2:C1-1000}&{}{}{}" };        //M5,C5,K5,C5,N5,B3,C
        //From small/weak -> big/strong         
        //Barrel
        //M,S,N,L,G with quality 1-9 123456789
        //Action
        //C,F,S,P,M
        //Muzzle
        //C,K(ompressor),S,B,V
        //Aiming
        //N,M,D,S,P
        //Gimbal
        //F,R,N,S,D
        //Magazine
        //S,C,N,D,B
        //Ammo
        //C,P,L,R(ocket)




        //"fighter:1:{M3:C1-1000}&{}{}{}", "fighter:1:{M3:C1-1000}&{}{}{}", "fighter:1:{M3:C1-1000}&{}{}{}", "fighter:1:{M3:C1-1000}&{}{}{}", "fighter:1:{M3:C1-1000}&{}{}{}" };

        ////{Barrel,Action,Muzzle,AimingType,Gimbal,Magazine,Ammunition}


        //GetComponent<RigidBody2D>().AddForce(new Vector2(0,100f));
        ships = new FleetData(items);

        for (int i = 0; i < ships.numberShips; i++)
        {
            GameObject newShip = Instantiate(Resources.Load("template"), new Vector2(0, 0), this.transform.rotation) as GameObject;

            newShip.transform.SetParent(holder.transform);

            newShip.GetComponent<MeshRenderer>().material = new Material(shader); //.sprite = (Resources.Load("sprites/" + ships.getShip(i).getType()) as GameObject).GetComponent<SpriteRenderer>().sprite;
            //texture.
            newShip.GetComponent<MeshRenderer>().material.mainTexture = texture;

            newShip.AddComponent<Ship>();
            //newShip.GetComponent<Ship>().size = (Resources.Load("sprites/"+ships.getShip(i).getType()) as GameObject).GetComponent<ShipSize>().size;
            newShip.GetComponent<Ship>().team = ships.getShip(i).getTeam();
            if (newShip.GetComponent<Ship>().team == 0) newShip.GetComponent<Ship>().speed = .075f;

            //  for (int j = 0; j < 100; j++)
            //  {
            //      if (shipTeams[ships.getShip(i).getTeam(), j] == null)
            //      {
            //        shipTeams[ships.getShip(i).getTeam(), j] = newShip;
            if (TeamManager.team_lists[ships.getShip(i).getTeam()] == null) TeamManager.team_lists[ships.getShip(i).getTeam()] = new List<GameObject>();

            TeamManager.team_lists[ships.getShip(i).getTeam()].Add(newShip);

              //      break;
             //   }
          //  }
        //

            newShip.AddComponent<life>();
            newShip.GetComponent<life>().health = ships.getShip(i).getLifePoints();
       //     newShip.AddComponent<BoxCollider2D>();

            for (int j = 0; j < ships.getShip(i).externalPoints.Length; j++)
            {
                GameObject gunPosition = Instantiate(Resources.Load("templateOLD"), new Vector2(0, 0), this.transform.rotation) as GameObject;
                gunPosition.AddComponent<Hardpoint>();
                gunPosition.GetComponent<Hardpoint>().setValues(ships.getShip(i).getHardpoint(j).getValues());
                gunPosition.transform.localScale = new Vector3(2, 2, 2);
                gunPosition.transform.position = ships.getShip(i).getHardpoint(j).getPosition();
                gunPosition.transform.SetParent(newShip.transform);
                gunPosition.tag = "Untagged";

                GameObject barrel = Instantiate(Resources.Load("templateOLD"), new Vector2(0, 0), this.transform.rotation) as GameObject;
                barrel.GetComponent<SpriteRenderer>().enabled = false;
                barrel.transform.SetParent(gunPosition.transform);
                barrel.transform.localPosition = new Vector3(0, 0, -2);
                barrel.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                barrel.transform.localScale = new Vector2(1, 1);
                barrel.AddComponent<partFunction>();
                //get damage modifications of WEAPON and HARDPOINT
                //HARDPOINT controls:   firerate, damage, speed, firecharactistics
                //AMMO controls:        damage, speed, visuals, firecharacteristics
                //
                barrel.GetComponent<partFunction>().item = ships.getShip(i).getHardpoint(j).item;
                //barrel.GetComponent<partFunction>().ProjectileFireCharacteristics = Values.getEquipmentElements(ships.getShip(i).getHardpoint(j).getEquippedItem()).Projectile_FireCharacteristics;
                //barrel.GetComponent<partFunction>().firingType = ships.getShip(i).getHardpoint(j).getEquippedAmmo();//getEquippedItem();
                //barrel.GetComponent<partFunction>().weaponType = ships.getShip(i).getHardpoint(j).getEquippedItem();
                //barrel.GetComponent<partFunction>().rateOfFire = Values.getEquipmentElements(ships.getShip(i).getHardpoint(j).getEquippedItem()).Projectile_FireCharacteristics.Projectile_FireRate;//Values.getProjectileElements(ships.getShip (i).getHardpoint(j).getEquippedItem()).Projectile_FireRate;
                //barrel.GetComponent<partFunction>().turnSpeed = Values.getRotationSpeed(ships.getShip(i).getHardpoint(j).getSize());
                //barrel.GetComponent<partFunction>().ammoAmount = ships.getShip(i).getHardpoint(j).getEquippedAmmoAmount();
                //if (ships.getShip(i).getHardpoint(j).getEquippedAmmoAmount() == -1) barrel.GetComponent<partFunction>().ammo = false;
                barrel.tag = "Untagged";

                GameObject minAngle = Instantiate(Resources.Load("sprites/pixelHalf"), new Vector2(0, 0), this.transform.rotation) as GameObject;
                minAngle.transform.SetParent(gunPosition.transform);
                minAngle.transform.localPosition = new Vector3(0, 0, -1);
                minAngle.transform.localScale = new Vector2(1, 50);
                minAngle.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -90));
                minAngle.GetComponent<SpriteRenderer>().enabled = false;
                minAngle.tag = "Untagged";

                GameObject maxAngle = Instantiate(Resources.Load("sprites/pixelHalf"), new Vector2(0, 0), this.transform.rotation) as GameObject;
                maxAngle.transform.SetParent(gunPosition.transform);
                maxAngle.transform.localPosition = new Vector3(0, 0, -1);
                maxAngle.transform.localScale = new Vector2(1, 50);
                maxAngle.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -90));
                maxAngle.GetComponent<SpriteRenderer>().enabled = false;
                maxAngle.tag = "Untagged";

                GameObject pointAngle = Instantiate(Resources.Load("sprites/pixelHalf"), new Vector2(0, 0), this.transform.rotation) as GameObject;
                pointAngle.transform.SetParent(barrel.transform);
                pointAngle.transform.localPosition = new Vector3(0, 0, -1);
                pointAngle.transform.localScale = new Vector2(1, 60);
                pointAngle.GetComponent<SpriteRenderer>().enabled = false;
                pointAngle.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                pointAngle.tag = "Untagged";
            }
            ///int team = ships.getShip (i).getTeam();
            //for (int j = 0; j < 10; j++)
            //{
            ///	if (shipTeams[team,j] == null)	shipTeams[team,j] = newShip;
            //}
        }


    }

}
