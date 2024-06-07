    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;

    public class LevelObject : MonoBehaviour
    {
        public Button levelButton;
        public Image[] stars;
        Score score;

        void Start()
        {
            score = FindObjectOfType<Score>();
            levelButton.GetComponent<Button>();
        }

        public void openLevel(int levelId)
        {
            string levelName = "Scene_Map " + levelId;
            score.ResetScore();
            SceneManager.LoadScene(levelName);
        }
    }
