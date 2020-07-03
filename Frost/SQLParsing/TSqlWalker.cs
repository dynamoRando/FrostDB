using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrostDB
{
    internal class TSqlWalker
    {
        #region Private Fields
        private ParseTreeWalker _walker;
        private TSqlParserListenerExtended _loader;
        private IParseTree _tree;
        #endregion

        #region Constructors
        public TSqlWalker(ParseTreeWalker walker, TSqlParserListenerExtended loader, IParseTree tree)
        {
            _walker = new ParseTreeWalker();
            _loader = loader;
            _tree = tree;
        }
        #endregion Constructors

        #region Public Methods
        public void Walk()
        {
            _walker.Walk(_loader, _tree);
        }
        #endregion
    }
}
