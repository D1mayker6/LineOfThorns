using UnityEngine;
using UnityEngine.Events;
using YandexMobileAds;
using YandexMobileAds.Base;

namespace Tools
{
    public class SimpleYandexAds : MonoBehaviour
    {
        [Header("ID блоков")]
        public string interstitialId = "demo-interstitial-yandex";
        public string rewardedId = "demo-rewarded-yandex";

        [Header("Когда игрок досмотрел rewarded")]
        public UnityEvent onRewardEarned;

        private RewardedAdLoader rewardedLoader;
        private RewardedAd rewardedAd;

        private InterstitialAdLoader interstitialLoader;
        private Interstitial interstitialAd;

        private void Awake()
        {
            rewardedLoader = new RewardedAdLoader();
            interstitialLoader = new InterstitialAdLoader();
        }

        private void Start()
        {
            RequestRewarded();
            RequestInterstitial();
        }

        public void RequestRewarded()
        {
            if (rewardedAd != null)
            {
                rewardedAd.Destroy();
                rewardedAd = null;
            }

            rewardedLoader.LoadAd(
                new AdRequest(rewardedId),
                onLoaded: (ad) =>
                {
                    rewardedAd = ad;
                    Debug.Log("Rewarded загружен");
                },
                onFailed: (args) =>
                {
                    Debug.LogWarning("Rewarded не загрузился: " + args.Message);
                }
            );
        }

        public void ShowRewarded()
        {
            if (rewardedAd == null)
            {
                Debug.LogWarning("Rewarded ещё не готов");
                RequestRewarded();
                return;
            }

            rewardedAd.OnRewarded += (sender, args) =>
            {
                onRewardEarned?.Invoke();
            };

            rewardedAd.OnAdDismissed += (sender, args) =>
            {
                rewardedAd.Destroy();
                rewardedAd = null;
                RequestRewarded(); 
            };

            rewardedAd.OnAdFailedToShow += (sender, args) =>
            {
                Debug.LogWarning("Не показал rewarded: " + args.Message);
                rewardedAd.Destroy();
                rewardedAd = null;
                RequestRewarded();
            };

            rewardedAd.Show();
        }

        public void RequestInterstitial()
        {
            if (interstitialAd != null)
            {
                interstitialAd.Destroy();
                interstitialAd = null;
            }

            interstitialLoader.LoadAd(
                new AdRequest(interstitialId),
                onLoaded: (ad) =>
                {
                    interstitialAd = ad;
                    Debug.Log("Interstitial загружен");
                },
                onFailed: (args) =>
                {
                    Debug.LogWarning("Interstitial не загрузился: " + args.Message);
                }
            );
        }

        public void ShowInterstitial()
        {
            if (interstitialAd == null)
            {
                Debug.LogWarning("Interstitial ещё не готов");
                RequestInterstitial();
                return;
            }

            interstitialAd.OnAdDismissed += (sender, args) =>
            {
                interstitialAd.Destroy();
                interstitialAd = null;
                RequestInterstitial();
            };

            interstitialAd.Show();
        }
    }
}