public static class HTTP {
    public const string GET = "get", POST = "post", DELETE = "delete", PUT = "put";

    public const string API_ENDPOINT = "https://bitnaughts.azurewebsites.net/api/";

    public static class Endpoints {

        public const string GET = "get",
            SET = "set",
            UPDATE = "update",
            RESET = "reset";

        public static class Parameters {
            public const string FLAG = "flag",
                TYPE = "type",
                ID = "id";
            public static class Values {
                public const string RESET = "reset",
                    TYPE = "type",
                    ID = "id";
            }
        }

    }
}