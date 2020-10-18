using System;
using System.Collections.Generic;

namespace Gjognumskygni.ViewModel
{
    public class VoteResultViewModel
    {
        public string Term { get; set; }

        public string Topic { get; set; }

        public string Reading { get; set; }

        public DateTime VoteDate { get; set; }

        public int Present { get; set; }

        public int YesCount { get; set; }

        public int NoCount { get; set; }

        public int BlankCount { get; set; }

        public int AbsentCount { get; set; }

        public IList<string> YesVoters { get; set; }

        public IList<string> NoVoters { get; set; }

        public IList<string> BlankVoters { get; set; }

        public IList<string> AbsentVoters { get; set; }
    }
}
