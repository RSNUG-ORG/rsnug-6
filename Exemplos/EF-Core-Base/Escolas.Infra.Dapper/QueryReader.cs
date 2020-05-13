using System;
using System.IO;

namespace Escolas.Infra.Dapper
{
    public class QueryReader
    {
        public static string GetQuery(string path, string queryName)
        {
            StreamReader queryReader = new StreamReader(Path.Combine(path, $"{queryName}.sql"));
            return queryReader.ReadToEnd();
        }
    }
}
