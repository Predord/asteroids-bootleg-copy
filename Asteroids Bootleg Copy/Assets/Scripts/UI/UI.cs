using AsteroidsCode.Core;
using TMPro;

namespace AsteroidsCode.UI
{
    public class UI : Singleton<UI>
    {
        public TMP_Text score;
        public TMP_Text xCoordText;
        public TMP_Text yCoordText;
        public TMP_Text angleText;
        public TMP_Text speedText;
        public TMP_Text laserChargesText;
        public TMP_Text laserRechargeText;

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

        public void ResetScore()
        {
            score.text = "Score: 0";
        }

        public void RefreshScore(int value)
        {
            score.text = "Score: " + value;
        }
    }
}
