using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameSparks.Api.Responses;
using GameSparks.Core;
using GameSparks.Api.Messages;

using GameSparks.RT;
using UnityEngine.SceneManagement;

using GameSparks.Api.Requests;

namespace TBD {
    public class GameSparksManager : MonoBehaviour {
        private static GameSparksManager instance = null;
        public static GameSparksManager Instance() {
            if (instance != null)
                return instance;
            else Debug.LogError("GSM| GameSparksManager Not Initialized...");
            return null;
        }

        void Awake() {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        private GameSparksRTUnity gameSparksRTUnity;
        public GameSparksRTUnity GetRTSession() {
            return gameSparksRTUnity;
        }

        private RTSessionInfo sessionInfo;
        public RTSessionInfo GetSessionInfo() {
            return sessionInfo;
        }

        #region Login & Registration
        public delegate void AuthCallback(AuthenticationResponse _authresp2);

        public void RegisterUser(string _userName, string _password) {
            new RegistrationRequest()
                .SetDisplayName(_userName)
                .SetUserName(_userName)
                .SetPassword(_password)
                .Send((regResp) => {
                    if (!regResp.HasErrors) { // if we get the response back with no errors then the registration was successful
                        Debug.Log("GSM| Registration Successful...");
                    } else Debug.LogWarning("GSM| Registration Failed \n" + regResp.Errors.JSON);
                });
        }

        public void AuthenticateUser(string _userName, string _password, AuthCallback _authcallback) {
            new AuthenticationRequest()
                .SetUserName(_userName)
                .SetPassword(_password)
                .Send((authResp) => {
                    if (!authResp.HasErrors) {
                        Debug.Log("Authentication Successful...");
                        _authcallback(authResp);
                    } else Debug.LogWarning("GSM| Error Authenticating User \n" + authResp.Errors.JSON);
                });
        }
        #endregion

        #region Matchmaking Request
        public void FindPlayers() {
            Debug.Log("GSM| Attempting Matchmaking...");
            new MatchmakingRequest()
                .SetMatchShortCode("MATCH")
                .SetSkill(0)
                .Send((response) => {
                    if (response.HasErrors) {
                        Debug.LogError("GSM| MatchMaking Error \n" + response.Errors.JSON);
                    }
                });
        }
        #endregion

        public void StartNewRTSession(RTSessionInfo _info) {
            if (gameSparksRTUnity == null) {
                Debug.Log("GSM| Creating New RT Session Instance...");
                sessionInfo = _info;
                gameSparksRTUnity = this.gameObject.AddComponent<GameSparksRTUnity>();
                GSRequestData mockedResponse = new GSRequestData();
                mockedResponse.AddNumber("port", (double)_info.GetPortID());
                mockedResponse.AddString("host", _info.GetHostURL());
                mockedResponse.AddString("accessToken", _info.GetAccessToken()); // construct a dataset from the game-details
                FindMatchResponse response = new FindMatchResponse(mockedResponse);

                gameSparksRTUnity.Configure(response,
                    (peerId) => { OnPlayerConnectedToGame(peerId); },
                    (peerId) => { OnPlayerDisconnected(peerId); },
                    (ready) => { OnRTReady(ready); },
                    (packet) => { OnPacketReceived(packet); });
                gameSparksRTUnity.Connect(); // when the config is set, connect the game
            } else {
                Debug.LogError("Session Already Started");
            }

        }

        private void OnPlayerConnectedToGame(int _peerId) {
            Debug.Log("GSM| Player " + _peerId + " Connected");
        }

        private void OnPlayerDisconnected(int _peerId) {
            Debug.Log("GSM| Player " + _peerId + " Disconnected");
            //GameController.Instance().OnOpponentDisconnected(_peerId);
        }

        private void OnRTReady(bool _isReady) {
            if (_isReady) {
                Debug.Log("GSM| RT Session Connected...");
                StartCoroutine(LoadGameScene());
                StartCoroutine(SendPackets());
            }
        }

        IEnumerator LoadGameScene() {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(1);
        }

        IEnumerator SendPackets() {
            for (int i = 1; i <= 150; i++) {
                GameSparksRTUnity.Instance.SendData(1, GameSparksRT.DeliveryIntent.RELIABLE, new RTData().SetInt(1, i));
                yield return new WaitForSeconds(2f);
            }
        }


        private void OnPacketReceived(RTPacket _packet) {

            switch (_packet.OpCode) {
                //case 1:
                //    if (chatManager == null) { // if the chat manager is not yet set up, then assign the reference in the scene
                //        chatManager = GameObject.Find("Chat Manager").GetComponent<ChatManager>();
                //    }
                //    chatManager.OnMessageReceived(_packet); // send the whole packet to the chat-manager
                //    break;

                case 2:
                    GameController.Instance().UpdateOpponents(_packet);
                    break;

                case 3:
                    GameController.Instance().ReceiveHit(_packet);
                    break;

                //case 4:
                //    GameController.Instance().UpdateOpponentShells(_packet);
                //    break;

                case 4:
                    GameController.Instance().RegisterOpponentCollision(_packet);
                    break;

            }
        }
    }


    public class RTSessionInfo {
        private string hostURL;
        public string GetHostURL() { return this.hostURL; }
        private string acccessToken;
        public string GetAccessToken() { return this.acccessToken; }
        private int portID;
        public int GetPortID() { return this.portID; }
        private string matchID;
        public string GetMatchID() { return this.matchID; }

        private List<RTPlayer> playerList = new List<RTPlayer>();
        public List<RTPlayer> GetPlayerList() {
            return playerList;
        }

        /// <summary>
        /// Creates a new RTSession object which is held untill a new RT session is created
        /// </summary>
        /// <param name="_message">Message.</param>
        public RTSessionInfo(MatchFoundMessage _message) {
            portID = (int)_message.Port;
            hostURL = _message.Host;
            acccessToken = _message.AccessToken;
            matchID = _message.MatchId;
            // we loop through each participant and get thier peerId and display name //
            foreach (MatchFoundMessage._Participant p in _message.Participants) {
                playerList.Add(new RTPlayer(p.DisplayName, p.Id, (int)p.PeerId));
            }
        }

        public class RTPlayer {
            public RTPlayer(string _displayName, string _id, int _peerId) {
                this.displayName = _displayName;
                this.id = _id;
                this.peerID = _peerId;
            }

            public string displayName;
            public string id;
            public int peerID;
            public bool isOnline;
        }
    }
}