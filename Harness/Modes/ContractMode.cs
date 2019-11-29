using FrostDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Harness.Modes
{
    public class ContractMode : BaseMode
    {
        #region Private Fields
        private Contract _contract;
        private bool _stayInMode = false;
        #endregion

        #region Public Properties
        public Database Database { get; }
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public ContractMode(App app, Database database) : base(app) 
        {
            Database = database;
        }
        #endregion

        #region Public Methods
        public override void Prompt()
        {
            _stayInMode = true;

            do
            {
                var result = this.Prompt("(ac) - add contract to db, " +
                    "(acd) - add default contract to db, " +
                    "(vc) - view contract for db, " +
                    "(em) to exit");

                switch (result)
                {
                    case "em":
                        _stayInMode = false;
                        break;
                    case "ac":
                        AddContractToDb();
                        break;
                    case "adc":
                        AddDefaultContractToDb();
                        break;
                    case "vc":
                        ViewContractForDb();
                        break;
                }
            } while (_stayInMode);
        }
        #endregion

        #region Private Methods
        private string Prompt(string message)
        {
            Console.WriteLine(message);
            Console.Write($"[Mode -> ContractMode : Database: ({ Database.Name })] [(em) to exit mode] ==>");

            var _consoleLine = Console.ReadLine();

            if (_consoleLine == "exit")
            {
                App.Write("Quitting...");
                App.Quit();
            }

            if (_consoleLine == "em")
            {
                App.Write("Quitting DbMode...");
                _stayInMode = false;
            }

            return _consoleLine;
        }

        private void ViewContractForDb()
        {
            throw new NotImplementedException();
        }

        private void AddContractToDb()
        {
            throw new NotImplementedException();
        }

        public void AddDefaultContractToDb()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
