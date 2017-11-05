using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;


namespace SmartFighter {
    public static class Api {
        public class Player {
            public string card_id;
            public string name;
            public int elo_rating;
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

        private static string getApiUrl() {
            string url = Config.Instance.apiUrl;
            if (!url.EndsWith("/")) {
                url += "/";
            }
            return url;
        }

        public static bool createGame(string id, string player1Id, string player2Id, int result, DateTime date, string player1Character, string player2Character) {
            try {
                using (var response = makeRequest(
                        getApiUrl() + "game", "POST", new {
                            id = id,
                            player1 = player1Id,
                            player2 = player2Id,
                            result = result,
                            date = date.ToString("o"),
                            player1_character = player1Character,
                            player2_character = player2Character,
                        })) {
                    if (response.StatusCode == HttpStatusCode.Created) {
                        Logger.Instance.log("API [createGame]: Match {0} created", id);
                        return true;
                    }
                    reportError("createGame", response);
                }
            } catch (WebException error) {
                reportError("createGame", (HttpWebResponse)error.Response);
            }
            return false;
        }


        public static bool updateRounds(string id, Round[] rounds) {
            try {
                using (var response = makeRequest(getApiUrl() + "game/" + id + "/rounds", "POST", rounds)) {
                    if (response.StatusCode == HttpStatusCode.OK) {
                        Logger.Instance.log("API [updateRounds]: Rounds of match {0} updated", id);
                        return true;
                    }
                    reportError("updateRounds", response);
                }
            } catch (WebException error) {
                reportError("updateRounds", (HttpWebResponse)error.Response);
            }
            return false;
        }

        public static bool createPlayer(string cardId, string name) {
            try {
                using (var response = makeRequest(
                        getApiUrl() + "player", "POST", new {
                            card_id = cardId,
                            name = name,
                        })) {
                    if (response.StatusCode == HttpStatusCode.Created) {
                        Logger.Instance.log("API [createPlayer]: Player {0} created", cardId);
                        return true;
                    }
                    reportError("createPlayer", response);
                }
            } catch (WebException error) {
                reportError("createPlayer", (HttpWebResponse)error.Response);
            }
            return false;
        }

        public static Player getPlayer(string cardId) {
            try {
                using (var response = makeRequest(getApiUrl() + "player/" + cardId, "GET")) {
                    if (response.StatusCode == HttpStatusCode.OK) {
                        var json = readResponse(response);
                        try {
                            return JsonConvert.DeserializeObject<Player>(json);
                        } catch (JsonReaderException) {
                            Logger.Instance.log("API Error [getPlayer]: Player cannot be decoded. {0}", json);
                            return null;
                        }
                    }
                    reportError("getPlayer", response);
                }
            } catch (WebException error) {
                reportError("getPlayer", (HttpWebResponse)error.Response);
            }
            return null;
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

        public static string readResponse(HttpWebResponse response) {
            using (var streamReader = new StreamReader(response.GetResponseStream())) {
                return streamReader.ReadToEnd();
            }
        }

        private static void reportError(string apiName, HttpWebResponse response) {
            if (response != null) {
                Logger.Instance.log("API Error [{0}]: {1} {2}", apiName, response.StatusCode, readResponse(response));
            } else {
                Logger.Instance.log("API Error [{0}]: No response received.", apiName);
            }
        }
    }
}
