using System.Windows.Forms;

namespace SamplePlugin2
{
    internal class SamplePlugin2
    {
        public string Name
        {
            get
            {
                return "SamplePlugin2";
            }
        }

        public void Initialize()
        {
            //    throw new NotImplementedException();
        }

        public void Do()
        {
            MessageBox.Show("This is a Sample Plugin.");
        }
    }
}
