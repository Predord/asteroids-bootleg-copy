using UnityEngine;
using UnityEngine.UI;
using AsteroidsCode.Core;
using TMPro;

namespace AsteroidsCode.UI
{
    public class UIManager : Singleton<UIManager>
    {
        public TMP_Text score;
        public TMP_Text xCoordText;
        public TMP_Text yCoordText;
        public TMP_Text angleText;
        public TMP_Text speedText;
        public TMP_Text laserChargesText;
        public TMP_Text laserRechargeText;
        public TMP_Text gameOverText;

        public RectTransform statsPanel;
        public Button newGameButton;

        private float prevCoordx;
        private float prevCoordy;
        private float prevAngle;

        private void Awake()
        {
            if (!RegisterMe())
            {
                return;
            }
        }

        private void OnEnable()
        {
            SpaceMetrics.OnScoreChange += RefreshScore;
        }

        private void OnDisable()
        {
            SpaceMetrics.OnScoreChange += RefreshScore;
        }

        public void NewGame()
        {
            statsPanel.gameObject.SetActive(true);
            newGameButton.gameObject.SetActive(false);
            gameOverText.gameObject.SetActive(false);
        }

        public void GameOver()
        {
            statsPanel.gameObject.SetActive(false);
            newGameButton.gameObject.SetActive(true);
            gameOverText.gameObject.SetActive(true);
        }

        public void RefreshScore(int value)
        {
            score.text = "Score: " + value;
        }

        public void SetPlayerPosition(Vector2 position)
        {
            if(prevCoordx != position.x)
            {
                xCoordText.text = "X: " + Mathf.Round(position.x * 1000f) / 1000f;
                prevCoordx = position.x;
            }
            
            if(prevCoordy != position.y)
            {
                yCoordText.text = "Y: " + Mathf.Round(position.y * 1000f) / 1000f;
                prevCoordy = position.y;
            }                  
        }

        public void SetPlayerRotation(float angle)
        {
            if(angle != prevAngle)
            {
                angleText.text = "a: " + Mathf.Round(angle * 10f) / 10f;
                prevAngle = angle;
            }          
        }

        public void SetSpeed(float speed)
        {
            speedText.text = "V: " + Mathf.Round(speed * 1000f) / 1000f;
        }

        public void SetLaserCharges(int charges)
        {
            laserChargesText.text = "Laser Charges: " + charges;
        }

        public void SetLaserRecharge(int time)
        {
            laserRechargeText.text = "Laser Recharge Time: " + time;
        }
    }
}
