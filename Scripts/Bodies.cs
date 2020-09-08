using System;
using System.Collections;
using System.Collections.Generic;

public abstract class Body {

    /* Base Class: The highest level abstraction of every physical object in BitNaughts */

    public int id;
    public int seed;

    /* How the object is situation in the solar system */
    /* Note: all objects orbit at equal speeds, so only theta and radius are relevant */
    public OrbitF orbit;

    /* How dense the object is */
    public float density;

    /* For varying object sizes */
    public string composition;
    public float size;

    public Body (string serial) { }
    public Body (int seed) {

        /* Pre-seeds random number generator */
        this.seed = seed;
        RandomHandler.Seed (seed);
    }
}

public class Asteroid : Body {

    //Notes::
    //Size of celestial bodies is related to hitpoints. Damaging bodies makes them smaller until destroyed

    public bool is_mineable, is_regenerating;

    public Asteroid (string serial) : base (serial) {

        string[] fields = serial.Split (',');

        this.id = int.Parse (fields[0]);
        this.seed = int.Parse (fields[2]);
        this.orbit = new OrbitF (float.Parse (fields[3]), float.Parse (fields[4]));
        this.size = float.Parse (fields[4]);
        this.density = float.Parse (fields[5]);
        this.composition = fields[6];
        this.is_mineable = false;
        this.is_regenerating = false;
    }

    public Asteroid (int seed) : base (seed) {
        this.id = Galaxy.AsteroidID++;

        this.orbit = RandomHandler.NextOrbit (11, 14);

        this.size = RandomHandler.NextFloat (
            GalaxyConstants.Systems.Asteroids.MIN_SIZE,
            GalaxyConstants.Systems.Asteroids.MAX_SIZE
        );
        this.density = RandomHandler.NextFloat (
            GalaxyConstants.Systems.Asteroids.MIN_DENSITY,
            GalaxyConstants.Systems.Asteroids.MAX_DENSITY
        );
        this.composition = RandomHandler.NextItem (
            GalaxyConstants.Systems.Asteroids.COMPOSITIONS
        );
        is_mineable = true;
        is_regenerating = false;
    }

    public override string ToString () {
        return JSONHandler.ToJSON (new Dictionary<string, string> { { "id", id.ToString () },
            { "seed", seed.ToString () },
            { "radius", orbit.radius.ToString ("F") },
            { "theta", orbit.theta.ToString ("F") },
            { "size", size.ToString ("F") },
            { "density", density.ToString ("F") },
            { "composition", composition },
            { "is_mineable", (Convert.ToInt32 (is_mineable)).ToString () },
            { "is_regenerating", (Convert.ToInt32 (is_regenerating)).ToString () }
        });
    }
}

public class Planet : Body {

    public bool is_habitable, is_inhabited;
    public float kardashev_level;
    public string economy_type;

    public Planet (string serial) : base (serial) {

        string[] fields = serial.Split (',');

        this.id = int.Parse (fields[0]);
        this.seed = int.Parse (fields[2]);
        this.orbit = new OrbitF (float.Parse (fields[3]), float.Parse (fields[4]));
        this.size = float.Parse (fields[4]);
        this.density = float.Parse (fields[5]);
        this.composition = fields[6];
        this.is_habitable = false;
        this.is_inhabited = false;
        this.kardashev_level = float.Parse (fields[10]);
        this.economy_type = fields[11];
    }

    public Planet (int seed) : base (seed) {
        this.id = Galaxy.PlanetID++;

        if (RandomHandler.NextBool ()) {
            /* Habitable */
            this.orbit = RandomHandler.NextOrbit (5, 10);
            this.size = RandomHandler.NextFloat (
                GalaxyConstants.Systems.Planets.MIN_SIZE,
                GalaxyConstants.Systems.Planets.MAX_SIZE
            );
            this.density = RandomHandler.NextFloat (
                GalaxyConstants.Systems.Planets.MIN_DENSITY,
                GalaxyConstants.Systems.Planets.MAX_DENSITY
            );
            this.composition = RandomHandler.NextItem (
                GalaxyConstants.Systems.Planets.COMPOSITIONS
            );
            this.economy_type = String.Join (
                " ",
                RandomHandler.NextItem (GalaxyConstants.Systems.Planets.ECONOMIC_FLAVORS),
                RandomHandler.NextItem (GalaxyConstants.Systems.Planets.ECONOMIC_FOCUSES),
                RandomHandler.NextItem (GalaxyConstants.Systems.Planets.ECONOMIC_LEVELS)
            );
            this.kardashev_level = RandomHandler.NextFloat (
                GalaxyConstants.Systems.Planets.MIN_KARDASHEV_LEVEL,
                GalaxyConstants.Systems.Planets.MAX_KARDASHEV_LEVEL
            );
            this.is_habitable = true;
            this.is_inhabited = true;
        } else {
            /* Gas Giant */
            this.orbit = RandomHandler.NextOrbit (15, 30);
            this.size = RandomHandler.NextFloat (
                GalaxyConstants.Systems.Planets.MIN_SIZE,
                GalaxyConstants.Systems.Planets.MAX_SIZE
            );
            this.density = RandomHandler.NextFloat (
                GalaxyConstants.Systems.Planets.MIN_DENSITY,
                GalaxyConstants.Systems.Planets.MAX_DENSITY
            );
            this.composition = "gas";
            this.economy_type = "NULL";
            this.kardashev_level = 0;
            this.is_habitable = false;
            this.is_inhabited = false;
        }
    }

    public override string ToString () {
        return JSONHandler.ToJSON (new Dictionary<string, string> { { "id", id.ToString () },
            { "seed", seed.ToString () },
            { "radius", orbit.radius.ToString ("F") },
            { "theta", orbit.theta.ToString ("F") },
            { "size", size.ToString ("F") },
            { "density", density.ToString ("F") },
            { "composition", composition },
            { "is_habitable", (Convert.ToInt32 (is_habitable)).ToString () },
            { "is_inhabited", (Convert.ToInt32 (is_inhabited)).ToString () },
            { "kardashev_level", kardashev_level.ToString ("F") },
            { "economy_type", economy_type }
        });
    }
}

public class Star : Body {

    public Star (int seed) : base (seed) {
        this.id = Galaxy.StarID++;

        this.orbit = RandomHandler.NextOrbit (0, 1);

        this.size = RandomHandler.NextFloat (
            GalaxyConstants.Systems.Stars.MIN_SIZE,
            GalaxyConstants.Systems.Stars.MAX_SIZE / 2
        );
        this.density = RandomHandler.NextFloat (
            GalaxyConstants.Systems.Stars.MIN_DENSITY * 3,
            GalaxyConstants.Systems.Stars.MAX_DENSITY
        );
        this.composition = RandomHandler.NextItem (
            GalaxyConstants.Systems.Planets.COMPOSITIONS
        );
    }

    public override string ToString () {
        return JSONHandler.ToJSON (
            new Dictionary<string, string> { { "id", id.ToString () },
                { "seed", seed.ToString () },
                { "radius", orbit.radius.ToString ("F") },
                { "theta", orbit.theta.ToString ("F") },
                { "size", size.ToString ("F") },
                { "density", density.ToString ("F") },
                { "composition", composition }
            }
        );
    }
}