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
            var pid = p.Id;

            MMDevice device;
            MMDeviceEnumerator DevEnum = new MMDeviceEnumerator();
            device = DevEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
            AudioSessionManager sessionManager = device.AudioSessionManager;
            foreach (var item in sessionManager.Sessions)
            {
                if (item.ProcessID != (uint)pid)
                {
                    item.SimpleAudioVolume.MasterVolume = 1f;
                }                
            }
            //device.AudioEndpointVolume.MasterVolumeLevelScalar = ((float)10 / 100.0f);
        }
    }
}
