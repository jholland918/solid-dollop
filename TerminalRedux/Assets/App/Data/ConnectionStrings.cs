namespace Assets.App.Data
{
    public static class ConnectionStrings
    {
        public static readonly string Default = string.Format(@"URI=file:{0}/{1}", UnityEngine.Application.dataPath, "App/Data/database1.sqlite3");
    }
}
