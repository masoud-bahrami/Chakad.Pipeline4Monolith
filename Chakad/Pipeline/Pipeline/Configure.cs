namespace Chakad.Pipeline
{
    public class Configure
    {
        #region ~ Private Memebers ! ~
        private static Configure instance;
        /// <summary>Provides static access to the configuration object.</summary>
        public static Configure Instance
        {
            get
            {
                return Configure.instance;
            }
        }
        #endregion

    }
}
