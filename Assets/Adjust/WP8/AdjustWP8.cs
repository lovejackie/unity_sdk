﻿#if UNITY_WP8
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using AdjustUnityWP;

namespace com.adjust.sdk {
	public class AdjustWP8 : IAdjust {
		private const string sdkPrefix = "unity4.10.0";

		public bool isEnabled() {
			return AdjustWP.IsEnabled();
		}

		public void onPause() {
			AdjustWP.ApplicationDeactivated();
		}

		public void onResume() {
			AdjustWP.ApplicationActivated();
		}

		public void setEnabled(bool enabled) {
			AdjustWP.SetEnabled(enabled);
		}

		public void setOfflineMode(bool offlineMode) {
			AdjustWP.SetOfflineMode(offlineMode);
		}

		public void start(AdjustConfig adjustConfig) {
			string logLevelString = null;
			string environment = adjustConfig.environment.lowercaseToString();
			Action<Dictionary<string, string>> attributionChangedDictionary = null;

			if (adjustConfig.logLevel != null) {
				logLevelString = adjustConfig.logLevel.lowercaseToString();
			}

			if (adjustConfig.attributionChangedDelegate != null) {
				attributionChangedDictionary = (attributionDictionary) => Adjust.runAttributionChangedDictionary(attributionDictionary);
			}

			AdjustWP.ApplicationLaunching(
				appToken:adjustConfig.appToken,
				logLevelString:logLevelString,
				environment:environment,
				defaultTracker:adjustConfig.defaultTracker,
				eventBufferingEnabled:adjustConfig.eventBufferingEnabled,
				sdkPrefix:sdkPrefix,
				attributionChangedDic:attributionChangedDictionary,
                logDelegate:adjustConfig.logDelegate
			);
		}

		public void trackEvent(AdjustEvent adjustEvent) {
			AdjustWP.TrackEvent(
				eventToken:adjustEvent.eventToken,
				revenue:adjustEvent.revenue,
				currency:adjustEvent.currency,
				callbackList:adjustEvent.callbackList,
				partnerList:adjustEvent.partnerList
			);
		}

        // iOS specific methods
        public void setDeviceToken(string deviceToken) {}

		public string getIdfa() {
			return null;
		}

		// Android specific methods
		public void setReferrer(string referrer) {}

		public void getGoogleAdId(Action<string> onDeviceIdsRead) {}
	}
}
#endif
