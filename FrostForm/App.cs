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
            ListenForFormEvents();
        }
        #endregion

        #region Public Methods
        public void SetupClient(string remoteAddress, int remotePort)
        {
            _client = new FrostClient(remoteAddress, "127.0.0.1", remotePort, 520);
            ListenForAppEvents();
        }
        #endregion

        #region Private Methods
        private void ListenForAppEvents()
        {
            _client.EventManager.StartListening(ClientEvents.GotDatabaseNames, AddDbNames);
            _client.EventManager.StartListening(ClientEvents.GotDatabaseInfo, ShowDbInfo);
        }

        private void AddDbNames(IEventArgs args)
        {
            List<string> dbs = _client.Info.DatabaseNames;

            if (_form.listDatabases.InvokeRequired)
            {
                _form.listDatabases.Invoke(new MethodInvoker(delegate { dbs.ForEach(d => _form.listDatabases.Items.Add(d)); }));
            }
        }

        private void ShowDbInfo(IEventArgs args)
        {
            string currentDb = string.Empty;

            if (_form.listDatabases.InvokeRequired)
            {
                _form.listDatabases.Invoke(new MethodInvoker(delegate { currentDb = _form.listDatabases.SelectedItem.ToString(); }));
            }

            if (currentDb != string.Empty)
            {
                var item = _client.Info.DatabaseInfos.Where(d => d.Name == currentDb).First();

                if (_form.labelDatabaseName.InvokeRequired)
                {
                    _form.listDatabases.Invoke(new MethodInvoker(delegate { _form.labelDatabaseName.Text = item.Name; }));
                }

                if (_form.labelDatabaseId.InvokeRequired)
                {
                    _form.listDatabases.Invoke(new MethodInvoker(delegate { _form.labelDatabaseId.Text = item.Id.ToString(); }));
                }
            }
        }

        private void ListenForFormEvents()
        {
            _form.listDatabases.SelectedIndexChanged += ListDatabases_SelectedIndexChanged;
        }

        private void ListDatabases_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string currentDb = _form.listDatabases.SelectedItem.ToString();
            
            if (currentDb != string.Empty)
            {
                _client.GetDatabaseInfo(currentDb);
            }
        }
        #endregion



    }
}
