using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameSparks.RT;
using UnityStandardAssets.Characters.FirstPerson;

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
                        GameObject newPlayer = Instantiate(playerPrefabs[i], allSpawners[j].gameObject.transform.position, allSpawners[j].gameObject.transform.rotation) as GameObject;

                        //Set the players name
                        newPlayer.name = GameSparksManager.Instance().GetSessionInfo().GetPlayerList()[i].peerID.ToString();
                        //Set the players parent
                        newPlayer.transform.SetParent(this.transform);

                        // if the current iteration is the player, set it up as the player.
                        if (GameSparksManager.Instance().GetSessionInfo().GetPlayerList()[i].peerID == GameSparksManager.Instance().GetRTSession().PeerId)
                            newPlayer.GetComponent<Player_Master>().Setup(allSpawners[j].gameObject.transform, true);
                        else {
                            newPlayer.GetComponent<Player_Master>().Setup(allSpawners[j].gameObject.transform, false);
                            newPlayer.GetComponent<FirstPersonController>().enabled = !enabled;
                            newPlayer.transform.GetChild(0).gameObject.SetActive(false);
                        }

                        // add the new tank object to the corresponding reference in the list
                        playerList[i] = newPlayer.GetComponent<Player_Master>();
                        // set the HUD of this player to be the display name
                        //playerNamesHUDList[i].text = GameSparksManager.Instance().GetSessionInfo().GetPlayerList()[i].displayName;
                        break;
                    }
                }
            }
            #endregion
        }

        public void UpdateOpponents(RTPacket _packet) {
            for (int i = 0; i < playerList.Length; i++) {
                if (playerList[i].name == _packet.Sender.ToString()) { // check the name of the tank matches the sender
                                                                            // we calculate the new position the tank should go to be the position they are at plus the velocity. That is, their position plus the distance they traveled according to their last speed
                    playerList[i].goToPos = (new Vector2(_packet.Data.GetVector4(1).Value.x, _packet.Data.GetVector4(1).Value.y)) + (new Vector2(_packet.Data.GetVector4(1).Value.z, _packet.Data.GetVector4(1).Value.w));
                    playerList[i].gotoRot = _packet.Data.GetFloat(2).Value;
                    break; // break, because we don’t need to update any other tanks.
                }
            }
        }

        public void RegisterOpponentCollision(RTPacket _packet) {

        }

        public void OnOpponentDisconnected(int _peerId) {

        }
    }
}

