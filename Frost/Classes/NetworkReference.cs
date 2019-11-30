using FrostCommon;

namespace FrostDB
{
    public static class NetworkReference
    {
        #region Private Fields
        #endregion

        #region Public Properties
        public static Network Network { get; set; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        public static void SendMessage(Message message)
        {
            Network.SendMessage(message);
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
