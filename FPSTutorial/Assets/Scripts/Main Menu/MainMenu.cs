using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TBD {
    public class MainMenu : MonoBehaviour {
        
        public void PlayGame() {
            SceneManager.LoadScene(1);
        }

        public void ExitGame() {
            Application.Quit();
        }
    }
}

