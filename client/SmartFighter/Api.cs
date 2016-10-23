using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;


namespace SmartFighter {
    public static class Api {
        private static string getApiUrl() {
            string url = Config.Instance.apiUrl;
            if (!url.EndsWith("/")) {
                url += "/";
            }
            return url;
        }

        public static bool createGame(string id, string player1Id, string player2Id, int result, DateTime date) {
            try {
                using (var response = makeRequest(
                        getApiUrl() + "game/", "POST", new {
                            id = id,
                            player1 = player1Id,
                            player2 = player2Id,
                            result = result,
                            date = date.ToString("o")
                        })) {
                    if (response.StatusCode == HttpStatusCode.Created) {
                        Logger.Instance.log("API [createGame]: Match {0} created", id);
                        return true;
                    }
                    using (var streamReader = new StreamReader(response.GetResponseStream())) {
                        var message = streamReader.ReadToEnd();
                        Logger.Instance.log("API Error [createGame]: {0}", message);
                    }
                }
            } catch (WebException error) {
                Logger.Instance.log("API request exception: {0}", error);
            }
            return false;
        }

        public class Round {
            public int result;
            public int player1;
            public int player2;

            public Round(int result, int player1, int player2) {
                this.result = result;
                this.player1 = player1;
                this.player2 = player2;
            }
        }

        public static bool updateRounds(string id, Round[] rounds) {
            try {
                using (var response = makeRequest(getApiUrl() + "game/" + id + "/rounds/", "POST", rounds)) {
                    if (response.StatusCode == HttpStatusCode.OK) {
                        Logger.Instance.log("API [updateRounds]: Rounds of match {0} updated", id);
                        return true;
                    }
                    using (var streamReader = new StreamReader(response.GetResponseStream())) {
                        var message = streamReader.ReadToEnd();
                        Logger.Instance.log("API Error [updateRounds]: {0}", message);
                    }
                }
            } catch (WebException error) {
                Logger.Instance.log("API request exception: {0}", error);
            }
            return false;
        }

        public static HttpWebResponse makeRequest(string url, string method, object data = null) {
            Logger.Instance.log("Request: {0} {1}", method, url);
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = method;
            request.Timeout = 5000;

            if (data != null) {
                using (var stream = new StreamWriter(request.GetRequestStream())) {
                    stream.Write(JsonConvert.SerializeObject(data));
                }
            }
            return (HttpWebResponse)request.GetResponse();
        }
    }
}
