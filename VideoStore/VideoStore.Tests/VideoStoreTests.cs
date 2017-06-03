using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStore;
using NSubstitute;

namespace VideoStore.Tests
{
    [TestFixture]
    public class VideoStoreTests 
    {
        private IRentals rentals;       
        private VideoStore sut;

        [SetUp]
        public void SetUp()
        {
            rentals = Substitute.For<IRentals>();
            sut = new VideoStore(rentals);
        }
        
    }
}
