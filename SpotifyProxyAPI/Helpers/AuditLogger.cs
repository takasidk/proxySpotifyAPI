using SpotifyProxyAPI.Middlewares;

namespace SpotifyProxyAPI.Helpers
{
    public static class AuditLogger
    {
        /// <summary>
        /// Method which logs request information
        /// </summary>
        /// <param name="transactionID"></param>
        /// <param name="method"></param>
        /// <param name="path"></param>
        /// <param name="queryString"></param>
        /// <param name="payload"></param>
        public static void RequestInfo(string transactionID, string method, string path, string queryString, string payload)
        {
            AuditMiddleware.Logger.Information(
                string.Format("Request:TransactionID-{0},Method-{1},Path-{2},QueryString-{3},Payload-{4}",
                transactionID, method, path, queryString, payload));
        }

        /// <summary>
        /// Method which logs response information
        /// </summary>
        /// <param name="transactionID"></param>
        /// <param name="method"></param>
        /// <param name="path"></param>
        /// <param name="queryString"></param>
        /// <param name="databaseName"></param>
        /// <param name="collectionName"></param>
        /// <param name="payload"></param>
        public static void ResponseInfo(string transactionID, string method, string path, string queryString, string databaseName, string collectionName, string payload)
        {
            AuditMiddleware.Logger.Information(
                string.Format(
                    "Request:TransactionID-{0},Method-{1},Path-{2},QueryString-{3},DatabaseName-{4},CollectionName-{5},Payload-{6}",
                transactionID, method, path, queryString, databaseName, collectionName, payload
                    ));
        }
    }
}
