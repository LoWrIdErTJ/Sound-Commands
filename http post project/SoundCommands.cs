using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using UBotPlugin;
using System.Linq;
using System.Windows;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Xml.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Security.Cryptography;
using System.Configuration;
using System.Media;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Net;
using System.Management;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Collections;
using System.Collections.Specialized;
using System.Data.SqlClient;
using Microsoft.VisualBasic;
using System.Reflection;
using System.Data.OleDb;

namespace CSVtoHTML
{

    // API KEY HERE
    public class PluginInfo
    {
        public static string HashCode { get { return "da19be5c4b99472a2b6404c3e07b77e8f308372a"; } }
    }

    // ---------------------------------------------------------------------------------------------------------- //
    //
    // ---------------------------------               COMMANDS               ----------------------------------- //
    //
    // ---------------------------------------------------------------------------------------------------------- //

    //
    //
    // PLAY SYSTEM SOUND
    //
    //
    public class PlaySystem_Sound : IUBotCommand
    {

        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public PlaySystem_Sound()
        {
            var xParameter = new UBotParameterDefinition("Play Sound", UBotType.String);
            xParameter.Options = new[] { "Asterisk", "Beep", "Exclamation", "Hand", "Question" };
            _parameters.Add(xParameter);

        }

        public string Category
        {
            get { return "Action Commands"; }
        }

        public string CommandName
        {
            get { return "play system sound"; }
        }


        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {

            string sound = parameters["Play Sound"];

            if (sound == "Asterisk")
            {
                System.Media.SystemSounds.Asterisk.Play();
            }
            else if (sound == "Beep")
            {
                System.Media.SystemSounds.Hand.Play();
            }
            else if (sound == "Exclamation")
            {
                System.Media.SystemSounds.Exclamation.Play();
            }
            else if (sound == "Hand")
            {
                System.Media.SystemSounds.Hand.Play();
            }
            else if (sound == "Question")
            {
                System.Media.SystemSounds.Question.Play();
            }
            else { }

        }


        public bool IsContainer
        {
            get { return false; }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public IEnumerable<string> Options
        {
            get;
            set;
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }


    //
    //
    // PLAY CUSTOM SOUND FILE
    // wav, mp3, aiff, wma
    //
    //
    public class PlayCustom_Sound : IUBotCommand
    {

        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public PlayCustom_Sound()
        {
            _parameters.Add(new UBotParameterDefinition("Path to audio file", UBotType.String)); 
            
        }

        public string Category
        {
            get { return "Action Commands"; }
        }

        public string CommandName
        {
            get { return "play custom sound"; }
        }

        [DllImport("winmm.dll")]
        private static extern long mciSendString(string Cmd, StringBuilder StrReturn, int ReturnLength, IntPtr HwndCallback);

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {

            string soundfile = parameters["Path to audio file"];

            string FileName = soundfile;

            StringBuilder sb = new StringBuilder(1024);
            GetShortPathName(FileName, sb, sb.Capacity);
            string shortFileName = sb.ToString();
 
            mciSendString("play " + "\"" + sb + "\"", null, 0, IntPtr.Zero);



        }

        // retun short path
        [DllImport("kernel32")]
        public static extern int GetShortPathName(
            string lpszLongPath, StringBuilder lpszShortPath, 
            int bufSize);
 
        // return long name
        [DllImport("kernel32")]
        public static extern int GetLongPathName(
            string lpszShortPath, StringBuilder lpszLongPath,
            int bufSize);
        


        public bool IsContainer
        {
            get { return false; }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }

}
