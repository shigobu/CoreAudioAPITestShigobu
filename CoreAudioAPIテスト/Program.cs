using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreAudioApi;

namespace CoreAudioAPIテスト
{
    class Program
    {
        static void Main(string[] args)
        {
            //自分自身のプロセスを取得する
            System.Diagnostics.Process p = System.Diagnostics.Process.GetCurrentProcess();
            int pid = p.Id;

            MMDevice device = null;
            try
            {
                using (MMDeviceEnumerator DevEnum = new MMDeviceEnumerator())
                {
                    device = DevEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
                }
				float MasterVolumeLevel = device.AudioEndpointVolume.MasterVolumeLevelScalar;
				AudioSessionManager sessionManager = device.AudioSessionManager;
                for (float i = 100; i >= 0; i -= 1f)
                {
                    foreach (var item in sessionManager.Sessions)
                    {
                        if (item.ProcessID != (uint)pid)
                        {
                            item.SimpleAudioVolume.MasterVolume = ((float)i / 100.0f);
                        }                
                    }
                    System.Threading.Thread.Sleep(50);
                }
                //元の音量に戻す
                foreach (var item in sessionManager.Sessions)
                {
                    if (item.ProcessID != (uint)pid)
                    {
                        item.SimpleAudioVolume.MasterVolume = ((float)100 / 100.0f);
                    }
                }
                device.AudioEndpointVolume.MasterVolumeLevelScalar = MasterVolumeLevel;
            }
            finally
            {
                if (device != null)
                {
                    device.Dispose();
                }
            }
        }
    }
}
