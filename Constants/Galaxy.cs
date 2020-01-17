using System;

public static class GalaxyConstants {

    public const int MIN_SYSTEMS = 50,
        MAX_SYSTEMS = 100;

    public static class Systems {

        public const int MIN_STARS = 1,
            MAX_STARS = 3,
            MIN_PLANETS = 2,
            MAX_PLANETS = 8,
            MIN_ASTEROIDS = 6,
            MAX_ASTEROIDS = 24;

        public const double MIN_RADIUS = 1,
            MAX_RADIUS = 20,
            MIN_THETA = 0,
            MAX_THETA = 2 * Math.PI;

        public static class Stars {
            public const double MIN_SIZE = 100,
                MAX_SIZE = 1000,
                MIN_DENSITY = 1,
                MAX_DENSITY = 2;
        }
        public static class Planets {

            public const double MIN_SIZE = 10,
                MAX_SIZE = 30,
                MIN_DENSITY = 1,
                MAX_DENSITY = 6;

            public const double MIN_KARDASHEV_LEVEL = 0.1,
                MAX_KARDASHEV_LEVEL = 1;

            public static readonly string[] COMPOSITIONS = {
                "silicaceous",
                "carbonaceous",
                "metallic"
            };

            /* e.g. a "tranditional electronics manufacturing" planet */
            /* e.g. a "command mass media service" planet */

            /* Elements with lower indices are exponentially more likely to occur */
            
            public static readonly string[] ECONOMIC_FLAVORS = {
                "traditional" /* High scarcity, primitive, low technology content population */ ,
                "market" /* High excess, innovative, high technology, stratified population */ ,
                "mixed" /* Consistent, medium technology, content population */ ,
                "command" /* Aggressive, authoritarian, restless population, */
            };
            public static readonly string[] ECONOMIC_FOCUSES = {
                /* Core, primitive focuses */
                "agricultural",
                "mining",
                "hospitality",
                "construction",
                "telecommunications",
                "electronics",
                "software",
                "energy",
                /* Research-based focuses */
                "metallurgic",
                "chemical",
                "financial",
                "arms",
                /* Niche focuses */
                "education",
                "mass media",
                "philosphical"
            };
            public static readonly string[] ECONOMIC_LEVELS = {
                "extraction" /* Sells raw materials relating to economic focus */ ,
                "manufacturing" /* Turns materials into products relating to economic focus */ ,
                "service" /* Markets products to consumers relating to economic focus */ ,
                "data" /* Collects and sells data relating to economic focus */
            };

        }
        public static class Asteroids {
            public const double MIN_SIZE = 1,
                MAX_SIZE = 10,
                MIN_DENSITY = 1,
                MAX_DENSITY = 5;
            public static readonly string[] COMPOSITIONS = {
                "silicaceous",
                "carbonaceous",
                "metallic"
            };
        }
    }
}