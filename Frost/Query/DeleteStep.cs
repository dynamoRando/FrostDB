using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    public class DeleteStep : IPlanStep
    {
        #region Private Fields
        #endregion

        #region Public Properties
        public Guid Id { get; set; }
        public int Level { get; set; }
        public string TableName { get; set; }
        public string DatabaseName { get; set; }
        public IPlanStep InputStep { get; set; }
        public bool HasInputStep => CheckHasInputStep();
        #endregion

        #region Protected Methods
        #endregion

        #region Events
        #endregion

        #region Constructors
        public DeleteStep()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Public Methods
        public StepResult GetResult(Process process, string databaseName)
        {
            throw new NotImplementedException();
        }

        public string GetResultText()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        private bool CheckHasInputStep()
        {
            if (InputStep != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion


    }
}
