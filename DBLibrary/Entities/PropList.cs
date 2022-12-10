using System.Collections;
using System.Linq.Expressions;
using System.Text;

namespace DBLibrary.Entities
{
    public class PropList 
    {
        public IList<Property> Props { get; }

        public PropList(IList<Property> props)
        {
            Props = props;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            foreach (var prop in Props)
            {
                result.Append(prop.Value).Append(' ');
            }
            return result.ToString();
        }
    }
}
