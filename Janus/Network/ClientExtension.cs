using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Server.Network
{
    public static class ClientExtension
    {
        public static void ForEach<T>(this IEnumerable<T> clients, Action<T> action) where T : SimpleClient
        {
            foreach (var client in clients)
            {
                action(client);
            }
        }
    }
}
