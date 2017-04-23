using System.Collections;
using System.Text;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using GameSparks.Api.Responses;
using GameSparks.Api.Messages;
using GameSparks.Api.Requests;
using UnityEngine.SceneManagement;

namespace TBD {
    public class LobbyManager : MonoBehaviour {

        public InputField userNameInput, passwordInput;
        public GameObject loginPanel, SearchPanel;
        public Button loginButton, registerButton, matchmakingButton, startGameButton, exitButton, logoutButton;
        public Text matchDetails, loginDetails;

        private RTSessionInfo tempRTSessionInfo;

        void Start() {
            #region Listeners
            loginButton.onClick.AddListener(() => {
                GameSparksManager.Instance().AuthenticateUser(userNameInput.text, passwordInput.text, OnAuthentication);
            });

            registerButton.onClick.AddListener(() => {
                GameSparksManager.Instance().RegisterUser(userNameInput.text, passwordInput.text);
            });

            matchmakingButton.onClick.AddListener(() => {
                GameSparksManager.Instance().FindPlayers();
                matchDetails.text = "Searching For Players...";
            });

            MatchNotFoundMessage.Listener = (message) => {
                matchDetails.text = "No Match Found...";
            };

            MatchFoundMessage.Listener = (message) => {
                matchmakingButton.gameObject.SetActive(false);
                startGameButton.gameObject.SetActive(true);
            };

            MatchFoundMessage.Listener += OnMatchFound;

            startGameButton.onClick.AddListener(() => {
                GameSparksManager.Instance().StartNewRTSession(tempRTSessionInfo);
            });

            exitButton.onClick.AddListener(() => {
                //Destroys objects that need to be killed before closing
                GameObject[] gos = GameObject.FindGameObjectsWithTag("GameController");
                foreach (GameObject go in gos) {
                    Destroy(go);
                }
                Application.Quit();
            });

            logoutButton.onClick.AddListener(() => {
                SceneManager.LoadScene(0);
            });
            #endregion
        }

        //Once a match is found, a message will be received. this will hold player details and such
        private void OnMatchFound(MatchFoundMessage _message) {
            matchDetails.text = "Match Found...";
            tempRTSessionInfo = new RTSessionInfo(_message); // we'll store the match data until we need to create an RT session instance
        }

        private void OnAuthentication(AuthenticationResponse _resp) {
            loginPanel.SetActive(false);
            SearchPanel.SetActive(true);
            startGameButton.gameObject.SetActive(false);
        }
    }
}
