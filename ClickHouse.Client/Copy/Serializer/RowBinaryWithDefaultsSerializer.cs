using System;
using ClickHouse.Client.Constraints;
using ClickHouse.Client.Formats;
using ClickHouse.Client.Types;

namespace ClickHouse.Client.Copy.Serializer;

// https://clickhouse.com/docs/en/interfaces/formats#rowbinarywithdefaults
internal class RowBinaryWithDefaultsSerializer : IRowSerializer
{
    public void Serialize(Span<object> row, ClickHouseType[] types, ExtendedBinaryWriter writer)
    {
        for (int col = 0; col < row.Length; col++)
        {
            if (row[col] is DBDefault)
            {
                writer.Write((byte)1);
            }
            else
            {
                writer.Write((byte)0);
                types[col].Write(writer, row[col]);
            }
        }
    }
}
