## proxySpotifyAPI

- Developed a proxy Spotify web API for Item search endpoint with OAuth Authentication, Serilog, Swagger UI, and Mini Profiler Enabled
- This API hits Item search end point of spotify and returns top 5 artists in the given market as a list

### Implementations
- POST endpoint which hits getItem search endpoint
- GET health check endpoint which checks health of the API
- Success response
  1. 200 -> Success response status code
- Custom Error response is implemented for (400, 404, 500)
  1. 400 -> Invalid Query
  2. 404 -> resource not found
  3. 500 -> Internal server Error
- Uses MongoDB as a cache. Stores previously queried data, so that API will not need to hit spotify API endpoint everytime for the same query.
- Swagger UI is implemented for better presentation/view.
- Serilog is used for logging. logs are classified into operational logs(which contains sensitive and detailed logs) and audit logs(which contains responses and requests logs)
- Miniprofiler is implemented to check time constraints of the methods used in the project
- OAUTH Authorization is implemented( Asynchronous method is called when ever Access token is expired(expires in 3600 secs)
- The code coverage is 92%(used MSTEST to code unit tests)
![image](https://user-images.githubusercontent.com/57638212/183074808-0292e3a2-6349-4d04-acb2-481c653b45a8.png)


### resources
- Spotify Item Search endpoint- https://developer.spotify.com/documentation/web-api/reference/#/operations/search
- Authorization flow- https://developer.spotify.com/documentation/general/guides/authorization/client-credentials/
