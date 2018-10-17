using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FleetObject
{
    public List<ShipObject> ships = new List<ShipObject>();
    


    public FleetObject(List<ShipObject> ships)
    {
        this.ships = ships;
    }
    
}
