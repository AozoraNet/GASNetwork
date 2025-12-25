using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAS.Demo
{
	public class ConsoleLog : MonoBehaviour
	{
		private string traceCache;
		private float lastTime;
		bool isException;

		private void Start()
		{
			lastTime = Time.realtimeSinceStartup;
		}

		void OnEnable()
		{
			Application.logMessageReceived += HandleLog;
		}

		void OnDisable()
		{
			Application.logMessageReceived -= HandleLog;
		}

		void HandleLog(string logString, string stackTrace, LogType type)
		{
			//if (type == LogType.Error) {
			//}
			if (type == LogType.Exception)
			{
				if (traceCache == stackTrace)
				{
					if (isException)
					{
						return;
					}

					if (Time.realtimeSinceStartup - lastTime < 1)
					{
						SnackBar.Instance.Show(logString);
						if (!isException)
						{
							isException = true;
							return;
						}
					}
				}
				else
				{
					traceCache = stackTrace;
					lastTime = Time.realtimeSinceStartup;
				}

				SnackBar.Instance.Show(logString, 1, 5);
			}
		}
	}
}
