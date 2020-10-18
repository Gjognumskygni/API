using System;
using System.Collections.Generic;
using System.Text;

namespace Gjognumskygni.ViewModel
{
    public class TingCase
    {
        public IList<(int, Uri, TingCaseType)> FirstReading;
        public IList<CaseView> SecondReading;
        public IList<CaseView> ThirdReading;
    }

    public enum TingCaseType
    {
        Audio,

    }
}
