using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaliburnMicroTest.Wpf.Models
{
    public interface IDataAccessProvider
    {
        string ConnectionName { get; }
    }

    public class DataAccessProvider : IDataAccessProvider
    {
        public DataAccessProvider()
        {
            
        }
        public string ConnectionName => "Sql Server Provider";
    }
}
