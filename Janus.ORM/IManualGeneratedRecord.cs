using Janus.ORM.SubSonic.DataProviders;
using Janus.ORM.SubSonic.Schema;

namespace Janus.ORM
{
    public interface IManualGeneratedRecord
    {
        ITable GetTableInformation(IDataProvider provider); 
    }
}