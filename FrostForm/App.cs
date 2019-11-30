using FrostDbClient;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FrostForm
{
    public class App
    {
        #region Private Fields
        FrostClient _client;
        formFrost _form;
        #endregion

        #region Public Properties
        public FrostClient Client => _client;
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public App(formFrost form) 
        {
            _form = form;
        }
        #endregion

        #region Public Methods
        public void SetupClient(string remoteAddress, int remotePort)
        {
            _client = new FrostClient(remoteAddress, "127.0.0.1", remotePort, 520);
            ListenForEvents();
        }
        #endregion

        #region Private Methods
        private void ListenForEvents()
        {
            _client.EventManager.StartListening(ClientEvents.GotDatabaseNames, AddDbNames);
        }

        private void AddDbNames(IEventArgs args)
        {
            List<string> dbs = _client.Info.DatabaseNames;

            if (_form.listDatabases.InvokeRequired)
            {
                _form.listDatabases.Invoke(new MethodInvoker(delegate { dbs.ForEach(d => _form.listDatabases.Items.Add(d)); }));
            }
        }
        #endregion



    }
}
