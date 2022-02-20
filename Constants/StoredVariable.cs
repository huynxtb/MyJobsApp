namespace MyJobsApp.Constants
{
    public static class StoredProcedureName
    {
        public static readonly string UpsInsUpdBackupFile = "upsInsUpdBackupFile";
        public static readonly string UpsInsUpdCustomerProdInfo = "upsInsUpdCustomerProdInfo";
    }

    public static class StoredVariable
    {
        public static readonly string Action = "@Action";
        public static readonly string JInput = "@JInput";
        public static readonly string ReturnValue = "@ReturnValue";
        public static readonly string OutputMessage = "@OutputMessage";
        public static readonly string UserId = "@UserId";
    }

    public static class StoredAction
    {
        public static readonly string Insert = "Insert";
        public static readonly string Update = "Update";
        public static readonly string Delete = "Delete";
        public static readonly string GetAll = "GetAll";
        public static readonly string CleanUp = "CleanUp";
    }
}