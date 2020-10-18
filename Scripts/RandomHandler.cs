using System;

public static class RandomHandler {

    /* Unseeded Random Functions */
    public static Random unseeded_random = new Random ();

    public static bool NewBool () {
        return NewInt (1, 100) > 50;
    }
    public static bool NewPercentChance (int percent) {
        return NewInt (1, 100) <= percent;
    }
    public static int NewInt (int low, int high) {
        return unseeded_random.Next (low, high);
    }
    public static double NewDouble (double high) {
        return unseeded_random.NextDouble () * high;
    }
    public static double NewDouble (double low, double high) {
        return unseeded_random.NextDouble () * (high - low) + low;
    }
    public static float NewFloat (double high) {
        return (float) NewDouble (high);
    }
    public static float NewFloat (double low, double high) {
        return (float) NewDouble (low, high);
    }
    public static T NewItem<T> (T[] elements) {
        return elements[
            NewInt (0, elements.Length)
        ];
    }

    /* Seeded Random Functions */
    public static Random seeded_random;

    public static void Seed (int seed) {
        seeded_random = new Random (seed);
    }
    public static int NextSeed () {
        return seeded_random.Next ();
    }
    public static bool NextBool () {
        return NextInt (1, 100) > 50;
    }
    public static int NextInt (int low, int high) {
        return seeded_random.Next (low, high);
    }
    public static double NextDouble (double high) {
        return seeded_random.NextDouble () * high;
    }
    public static double NextDouble (double low, double high) {
        return seeded_random.NextDouble () * (high - low) + low;
    }
    public static float NextFloat (double high) {
        return (float) NextDouble (high);
    }
    public static float NextFloat (double low, double high) {
        return (float) NextDouble (low, high);
    }
    // public static PointF NextPosition (int low, int high) {
    //     return new PointF (
    //         (float) seeded_random.NextDouble () * (high - low) + low,
    //         (float) seeded_random.NextDouble () * (high - low) + low
    //     );
    // }
    // public static PointF NextPosition (double max_radius) {
    //     double radius = NextDouble (max_radius);
    //     double theta = NextDouble (2 * Math.PI);
    //     return new PointF (
    //         (float) (radius * Math.Cos (theta)),
    //         (float) (radius * Math.Sin (theta))
    //     );
    // }
    // public static OrbitF NextOrbit (double min_radius, double max_radius) {
    //     double radius = NextDouble (min_radius, max_radius);
    //     double theta = NextDouble (2 * Math.PI);
    //     return new OrbitF ((float)radius, (float)theta);
    //     // (float) (radius * Math.Cos (theta)),
    //     // (float) (radius * Math.Sin (theta))

    // }
    public static T NextItem<T> (T[] elements) {
        return elements[
            NextInt (0, elements.Length)
        ];
    }

    // Assuming most solar systems follow the same rule of the Goldilocks zone... 
    // Habitable Planets close to the sun, asteroid belt, gas giants to collect rest of the hydrogen

}