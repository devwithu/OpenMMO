//by Fhiz
using System;
using System.Text;
using UnityEngine;
using Mirror;
using OpenMMO;
using OpenMMO.Network;

namespace OpenMMO {

	/// <summary>
	/// Abstract partial base class Syncable Component is the base class for all networked components. Provides caching and throttled update methods as well as basic properties.
	/// </summary>
	[System.Serializable]
	public abstract partial class SyncableComponent : BaseNetworkBehaviour
	{
	
		[Header("Caching")]
		[Tooltip("How often the manager itself is updated (and all of its data, in seconds)")]
		[Range(0.0f, 99)]
		public double managerUpdateInterval = 1f;
		[Tooltip("How long cached data is kept (in seconds) before its re-calculated")]
		[Range(0.0f, 99)]
		public double cacheUpdateInterval = 1f;
		
		double _timerManager = 0;
		
		protected DataCache cacheData;
		
		/// <summary>
		/// Static method that returns the local player object to prevent frequent GetComponent calls and checks. Can be called from anywhere.
		/// </summary>
		public static GameObject localPlayer => ClientScene.localPlayer != null ? ClientScene.localPlayer.gameObject : null;
		
		/// <summary>
		/// Server-side Start method to initialize the cache.
		/// </summary>
		[ServerCallback]
		protected virtual void Start() {
			cacheData = new DataCache(cacheUpdateInterval);
		}
		
		/// <summary>
		/// Retrieves the local player if it is this object to prevent frequent GetComponent calls and checks.
		/// </summary>
		public bool IsLocalPlayer
		{
			get {
				return localPlayer != null && localPlayer == this.gameObject;
			}
		}
		
		/// <summary>
		/// Used to throttle calls to "Update"
		/// </summary>
		protected bool CheckUpdateInterval => Time.time > _timerManager || managerUpdateInterval == 0;
		
		/// <summary>
		/// Updates the cache timer interval
		/// </summary>
		void RefreshUpdateInterval()
		{
			_timerManager = Time.time + managerUpdateInterval;
		}
		
		/// <summary>
		/// Updated every frame, private to enforce the use of UpdateServer/UpdateClient
		/// </summary>
		void Update()
		{
			if (CheckUpdateInterval)
			{
				if (isClient)
					UpdateClient();
				if (isServer)
					UpdateServer();
				
				RefreshUpdateInterval();
			}
		}
		
		/// <summary>
		/// Late Updated every frame, private to enforce the use of LateUpdateClient.
		/// </summary>
		void LateUpdate()
		{
			if (isClient)
				LateUpdateClient();
		}
		
		/// <summary>
		/// Private, frame-rate independent fixed update. Private to enforce the use of FixedUpdateClient / FixedUpdateServer.
		/// </summary>
		void FixedUpdate()
		{
			if (isClient)
				FixedUpdateClient();
		}
		
		/// <summary>
		/// Server-side throttled update, protected to allow derived classes to use it.
		/// </summary>
		[Server]
		protected abstract void UpdateServer();
		
		/// <summary>
		/// Client-side throttled update, protected to allow derived classes to use it.
		/// </summary>
		[Client]
		protected abstract void UpdateClient();
		
		/// <summary>
		/// Client-side late update, protected to allow derived classes to use it.
		/// </summary>
		[Client]
		protected abstract void LateUpdateClient();
		
		/// <summary>
		/// Client-side fixed update, protected to allow derived classes to use it.
		/// </summary>
		[Client]
		protected abstract void FixedUpdateClient();
		
	}

}