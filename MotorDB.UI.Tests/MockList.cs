using System.Collections.Generic;
using Moq;

namespace MotorDB.UI
{

    public class MockList : IEnumerable<Mock>
    {
        List<Mock> mocks = new List<Mock>();

        public Mock<T> Add<T>(Mock<T> mock) where T : class
        {
            mocks.Add(mock);
            return mock;
        }

        public void VerifyAll()
        {
            foreach (var mock in mocks)
                mock.VerifyAll();
        }

        #region IEnumerable<Mock> Members

        public IEnumerator<Mock> GetEnumerator()
        {
            return mocks.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }


}
