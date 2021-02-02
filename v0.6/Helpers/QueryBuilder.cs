/// <summary>
/// Simple querybuilder helper class
/// for influxcontroller & graph widgets. Use this helper
/// to build cleaner widget classes when querieing
/// InfluxDB via controller.
/// </summary>
class QueryBuilder
{
    /// <summary>
    /// Most common query type. ie. SELECT* FROM "temperature" WHERE time > now() - 24h
    /// </summary>
    /// <param name="item"></param>
    /// <param name="timespan"></param>
    /// <returns></returns>
    public static string ItemTimeSpan(string database, string retentionpolicy, string item, string timespan)
    {
        return "SELECT * FROM \""+ database +"\".\"" + retentionpolicy + "\".\"" + item + "\" WHERE time > " + timespan;
    }
}

