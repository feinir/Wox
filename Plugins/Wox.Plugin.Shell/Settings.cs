using System;
using System.IO;
using System.Collections.Generic;

namespace Wox.Plugin.Shell
{
    public class Settings
    {
        public Shell Shell { get; set; } = Shell.Cmd;
        public bool ReplaceWinR { get; set; } = true;
        public bool LeaveShellOpen { get; set; }
        public bool RunAsAdministrator { get; set; } = true;

        public Dictionary<string, int> Count = new Dictionary<string, int>();
        public bool SupportWSL { get; private set; }

        public Settings()
        {
            try
            {
                //这里最好写成用户自定义配置，由用户设定wsl装在什么地方
                string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string wslRoot = localAppData + @"\lxss\rootfs";
                if (Directory.Exists(wslRoot))
                {
                    SupportWSL = true;
                }
                else if(Directory.Exists(@"C:\Linux\kali-linux\rootfs"))
                {
                    wslRoot = @"C:\Linux\kali-linux\rootfs";
                    SupportWSL = true;
                }
                else if (Directory.Exists(@"C:\Linux\ubuntu\rootfs"))
                {
                    wslRoot = @"C:\Linux\ubuntu\rootfs";
                    SupportWSL = true;
                }
                else if (Directory.Exists(@"C:\Linux\rootfs"))
                {
                    wslRoot = @"C:\Linux\rootfs";
                    SupportWSL = true;
                }
                else if (Directory.Exists(@"C:\ubuntu\rootfs"))
                {
                    wslRoot = @"C:\ubuntu\rootfs";
                    SupportWSL = true;
                }
                SupportWSL = false;
            }
            catch
            {
                SupportWSL = false;
            }
        }

        public void AddCmdHistory(string cmdName)
        {
            if (Count.ContainsKey(cmdName))
            {
                Count[cmdName] += 1;
            }
            else
            {
                Count.Add(cmdName, 1);
            }
        }
    }

    public enum Shell
    {
        Cmd = 0,
        Powershell = 1,
        ConEmu = 2,
        WSLBash = 3,
        RunCommand = 4,
        Bash = 5
    }
}
