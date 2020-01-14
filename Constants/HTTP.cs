public static class HTTP {
    public const string GET = "get",
        POST = "post",
        DELETE = "delete",
        PUT = "put";

    public const string API_ENDPOINT = "https://bitnaughts.azurewebsites.net/api/";

    public static class Endpoints {
        public const string GET = "get",
            SET = "set",
            UPDATE = "update",
            RESET = "reset",
            MINE = "mine",
            VISIT = "visit",
            FIGHT = "fight";

        public static class Parameters {
            public const string FLAG = "flag",
                TABLE = "table",
                TYPE = "type",
                ID = "id",
                PLANET = "planet",
                ASTEROID = "asteroid",
                SHIP = "ship",
                SHIP_1 = "ship_1",
                SHIP_2 = "ship_2",
                AMOUNT = "amount",
                DATE = "date";

            public static class Values {
                public const string RESET = "reset",
                    ADD = "add",
                    TYPE = "type",
                    ID = "id";
            }
        }

    }
}