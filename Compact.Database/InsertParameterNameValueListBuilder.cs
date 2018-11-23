using System.Text;

namespace Compact.Database
{
    internal class InsertParameterNameValueListBuilder
    {
        public StringBuilder NameBuilder { get; set; }
        public StringBuilder ValueBuilder { get; set; }

        internal InsertParameterNameValueListBuilder()
        {
            NameBuilder = new StringBuilder();
            ValueBuilder = new StringBuilder();
        }
    }
}
