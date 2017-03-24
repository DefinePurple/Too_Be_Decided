using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameSparks.RT;

namespace TBD {
    public class GameController : MonoBehaviour {

        private static GameController instance; // this is the singleton for the game-controller class
        public static GameController Instance() {
            return instance;
        }
        void Awake() {
            instance = this;
        }
        public GameObject[] playerPrefabs;
        private Player_Master[] playerList;
        public Player_Master[] GetAllPlayers() {
            return playerList;
        }
        public Text[] playerScoreHUDList, playerNamesHUDList; // these are the text-fields for each player's kills and name in the HUD panel. This is set from the editor

        void Start() {
            SpawnPoint[] allSpawners = FindObjectsOfType(typeof(SpawnPoint)) as SpawnPoint[];
            int playerCount = GameSparksManager.Instance().GetSessionInfo().GetPlayerList().Count;
            #region Setup Player Tanks
            playerList = new Player_Master[playerCount];

            Debug.Log("GC| Found " + playerList.Length + " Players...");
            for (int i = 0; i < playerCount; i++) {
                for (int j = 0; j < allSpawners.Length; j++) {
                    if (allSpawners[j].playerPeerId == GameSparksManager.Instance().GetSessionInfo().GetPlayerList()[i].peerID) {
                        //Instantiate a new player
                        //GameObject newPlayer = Instantiate(playerPrefabs[i], allSpawners[j].gameObject.transform.position, allSpawners[j].gameObject.transform.rotation) as GameObject;
                        GameObject pref = playerPrefabs[i];
                        SpawnPoint spawner = allSpawners[j];
                        
                        //Set the players name
                        //newPlayer.name = GameSparksManager.Instance().GetSessionInfo().GetPlayerList()[i].peerID.ToString();
                        //Set the players parent
                        //newPlayer.transform.SetParent(this.transform);

                        //if the current iteration is the player, set it up as the player.
                        if (GameSparksManager.Instance().GetSessionInfo().GetPlayerList()[i].peerID == GameSparksManager.Instance().GetRTSession().PeerId)
                            //newPlayer.GetComponent<Player_Master>().SetupTank(allSpawners[j].gameObject.transform, true, playerScoreHUDList[i]);
                        //else
                           // newPlayer.GetComponent<Player_Master>().SetupTank(allSpawners[j].gameObject.transform, false, playerScoreHUDList[i]);

                        // add the new tank object to the corresponding reference in the list
                        //playerList[i] = newPlayer.GetComponent<Player_Master>();
                        // set the HUD of this player to be the display name
                        playerNamesHUDList[i].text = GameSparksManager.Instance().GetSessionInfo().GetPlayerList()[i].displayName; 
                        break;
                    }
                }
            }
            #endregion

            for (int i = GameSparksManager.Instance().GetSessionInfo().GetPlayerList().Count; i < playerScoreHUDList.Length; i++) {
                playerScoreHUDList[i].text = playerNamesHUDList[i].text = string.Empty;
            }
        }

        public void UpdateOpponents(RTPacket _packet) {

        }

        public void RegisterOpponentCollision(RTPacket _packet) {

        }

        public void OnOpponentDisconnected(int _peerId) {

        }
    }
}

