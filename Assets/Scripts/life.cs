using UnityEngine;
using System.Collections;

public class life : MonoBehaviour {

	public float health = 20;
    float originalHealth = -1;
    bool dying;
    float deathCounter = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//this.GetComponent<SpriteRenderer> ().color = new Color (255, 255, 255);
	}
    void Update()
    {
        if (dying)
        {
            if (Random.value > .8f)
            {
                ProjectileManager.getExplosion(this.transform.position, this.GetComponent<SpriteRenderer>().sprite.bounds.size, Random.value * 2f * deathCounter);
                if (Random.value > .75f)
                {
                    deathCounter+=.15f;
                }
            }
        }
    }
    /// <summary>0
    /// Testy test, dont forget about this... so cool
    /// Anyways******** if you pass up parameters of what shot hit person, you can change the explosion size via Values class
    /// </summary>
    /// <param name="amount"></param>
	public void hurt(float amount)
	{
        if (originalHealth == -1) originalHealth = health;
        health -= amount;
		//this.GetComponent<SpriteRenderer> ().color = new Color (255, 0, 0);
		if (health <= 0) delete ();

       // if (health / originalHealth < Random.value)
       // {
           // if (Random.value > .5)
       //         ProjectileManager.getExplosion(this.transform.position, this.GetComponent<SpriteRenderer>().sprite.bounds.size, amount);


       // }

    }
	
	public void delete()
	{
          
        TeamManager.team_lists[this.GetComponent<Ship>().team].Remove(this.gameObject);
        dying = true;
		Destroy (this.gameObject,2f);
		Destroy (this,2f);
	}
}
