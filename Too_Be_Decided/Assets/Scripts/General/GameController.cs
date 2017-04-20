﻿using System.Collections;
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
        public GameObject playerPrefab;
        public GameObject[] enemyPrefabs;
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
                        int tempPeerID = GameSparksManager.Instance().GetSessionInfo().GetPlayerList()[i].peerID;
                        // if the current iteration is the player, set it up as the player.
                        if (GameSparksManager.Instance().GetSessionInfo().GetPlayerList()[i].peerID == GameSparksManager.Instance().GetRTSession().PeerId) {
                            //Creates the player
                            GameObject newPlayer = Instantiate(playerPrefab, allSpawners[j].gameObject.transform.position, allSpawners[j].gameObject.transform.rotation) as GameObject;
                            
                            //Calls function to setup the player
                            newPlayer.GetComponent<Player_Master>().Setup(allSpawners[j].gameObject.transform, true, tempPeerID);
                            
                            //Ensures Cameras are active
                            newPlayer.transform.GetChild(0).gameObject.SetActive(true);
                            
                            //Set the players name
                            newPlayer.name = GameSparksManager.Instance().GetSessionInfo().GetPlayerList()[i].peerID.ToString();

                            //Set the players parent
                            newPlayer.transform.SetParent(this.transform);

                            // add the new tank object to the corresponding reference in the list
                            playerList[i] = newPlayer.GetComponent<Player_Master>();
                        } else {
                            //Creates the enemy player
                            GameObject enemyPlayer = Instantiate(enemyPrefabs[i], allSpawners[j].gameObject.transform.position, allSpawners[j].gameObject.transform.rotation) as GameObject;

                            //Calls function to setup the enemy player
                            enemyPlayer.GetComponent<Player_Master>().Setup(allSpawners[j].gameObject.transform, false, tempPeerID);

                            //Set the enemy's name
                            enemyPlayer.name = GameSparksManager.Instance().GetSessionInfo().GetPlayerList()[i].peerID.ToString();

                            //Set the enemy's parent
                            enemyPlayer.transform.SetParent(this.transform);

                            // add the new tank object to the corresponding reference in the list
                            playerList[i] = enemyPlayer.GetComponent<Player_Master>();
                        }

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
                if (playerList[i].name == _packet.Sender.ToString()) { // check the name of the player matches the sender
                    Vector3 position = new Vector3(_packet.Data.GetVector3(1).Value.x, _packet.Data.GetVector3(1).Value.y, _packet.Data.GetVector3(1).Value.z);
                    Vector3 velocity = new Vector3(_packet.Data.GetVector3(2).Value.x, _packet.Data.GetVector3(2).Value.y, _packet.Data.GetVector3(2).Value.z);
                    playerList[i].goToPos = position + velocity;
                    playerList[i].gotoRot = _packet.Data.GetFloat(3).Value;
                    break; // break, because we don’t need to update anyone else.
                }
            }
        }

        public void ReceiveHit(RTPacket _packet) {
            for (int i = 0; i < playerList.Length; i++) {
                if (playerList[i].GetPeerID() == _packet.Data.GetInt(2).Value) {
                    int damage = _packet.Data.GetInt(1).Value;
                    playerList[i].CallEventPlayerHealthReduce(damage);
                    break; // break, because we don’t need to update anyone else.
                }
            }
        }

        public void DeathAnimation(RTPacket _packet) {
            for (int i = 0; i < playerList.Length; i++) {
                if (playerList[i].name == _packet.Sender.ToString()) { // check the name of the player matches the sender
                    playerList[i].CallEventDeathAnimation();
                    break; // break, because we dont need to continue
                }
            }
        }

        public void OnOpponentDisconnected(int _peerId) {

        }
    }
}

