using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class AdmobScript : MonoBehaviour, IUnityAdsInitializationListener
{

    public static AdmobScript instance;
	[SerializeField] string _androidGameId;
	[SerializeField] bool _testMode = true;
	public string BannerId;
	public string InterstitialId;

	private int ads;


	void Awake()
	{
		if(instance == null)
        {
			instance = this;
			InitializeAds();
        }
        else
        {
			Destroy(this.gameObject);
        }
		DontDestroyOnLoad(this.gameObject);
	}

    private void Start()
    {
		ads = PlayerPrefs.GetInt("ads");
		if (ads == 0)
        {
			// Set the banner position:
			Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
		}
		Debug.Log("ads value : " + ads);

	}

    private void Update()
    {
		ads = PlayerPrefs.GetInt("ads");
    }

    public void InitializeAds()
	{
		if(ads == 0)
        {
			Advertisement.Initialize(_androidGameId, _testMode, this);
		}
	}

	public void OnInitializationComplete()
	{
		if(ads == 0)
        {
			Debug.Log("Unity Ads initialization complete.");
			//Request Ads
			RequestBanner();
			RequestInterstitial();
		}
	}

	public void OnInitializationFailed(UnityAdsInitializationError error, string message)
	{
		Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
	}

	public void showInterstitialAd ()
	{
		if(ads == 0)
        {
			//Show Ad
			// Note that if the ad content wasn't previously loaded, this method will fail
			Debug.Log("Showing Ad: " + InterstitialId);
			Advertisement.Show(InterstitialId);
		}

	}

	private void RequestBanner ()
	{
		if(ads == 0)
        {
			// Load the Ad Unit with banner content:
			Advertisement.Banner.Load(BannerId);
			// Show the loaded Banner Ad Unit:
			Advertisement.Banner.Show(BannerId);
		}
	}

	// Implement a method to call when the Hide Banner button is clicked:
	public void HideBannerAd()
	{
		// Hide the banner:
		Advertisement.Banner.Hide();
	}

	private void RequestInterstitial ()
		{
		if(ads == 0)
        {
			// IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
			Debug.Log("Loading Ad: " + InterstitialId);
			Advertisement.Load(InterstitialId);
		}
	}

}