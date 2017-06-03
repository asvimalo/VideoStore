using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStore;

namespace VideoStore.Tests
{
    public class VideoStoreTests 
    {
        private IRentals Rental;
        public VideoStoreTests(IRentals r)
        {
            Rental = r;
        }
    }
}
