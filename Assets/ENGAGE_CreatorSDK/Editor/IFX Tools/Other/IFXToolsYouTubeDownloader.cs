using System.Collections;
using System.Collections.Generic;
using System.IO;
using Process = System.Diagnostics.Process;
using ProcessStartInfo = System.Diagnostics.ProcessStartInfo;
using UnityEngine;
using UnityEditor;

public class IFXToolsYouTubeDownloader : EditorWindow
{
    public string youtubeURL{get; set;}
    public string downloadPath {get; set;}
    string videoRes{get; set;}
    string youtubeDLPath;

    bool res720 = true;
    bool res1080;
    bool res1440;
    bool res4k;
    bool resCustom;

    string videoResCustom{get; set;}
    // Start is called before the first frame update
    
    // static void Init()
    //     {
            
    //         EditorWindow window = GetWindow(typeof(IFXToolsYouTubeDownloader));
    //         window.Show();
            

    //     }
    private void OnEnable()
    {
        youtubeDLPath = Application.dataPath+@"\ENGAGE_CreatorSDK\Editor\IFX Tools\Other\youtube-dl.exe";
        this.minSize= new Vector2(500,200);
        this.name="YouTube Downloader Window";
    }
        void OnGUI()
        {


            youtubeURL = EditorGUILayout.TextField("Video URL: ", youtubeURL);

            res720 = EditorGUILayout.Toggle( "720p", res720);
            res1080 = EditorGUILayout.Toggle( "1080p", res1080);
            res1440 = EditorGUILayout.Toggle( "1440p (360 recomm.)", res1440);
            res4k = EditorGUILayout.Toggle( "2160p (4k)", res4k);
            resCustom = EditorGUILayout.Toggle( "Custom Res", resCustom);
            
            videoResCustom = EditorGUILayout.TextField("Res: ", videoResCustom);
            EditorGUILayout.LabelField("Download Path: "+downloadPath);
            if (GUILayout.Button("Select Download folder - Browse"))
            {
                downloadPath = EditorUtility.OpenFolderPanel("Select Download folder", "", "");
                                    
            }
            if (GUILayout.Button("Download"))
            {
                string[] urls = youtubeURL.Split(' ');

                foreach (var url in urls)
                {
                    //Debug.Log(url);
                    if (checkOptions())
                    {
                        if (res720)
                        {                        
                            videoRes="720";
                            string batchFilePath = CreateBatchCMDSFile("youtubeDLBatch",CreateyouTubeDLBatchFile("%%(title)s"+"_720"));
                            RunBatchFile(batchFilePath);
                        }
                        if (res1080)
                        {                        
                            videoRes="1080";
                            string batchFilePath = CreateBatchCMDSFile("youtubeDLBatch",CreateyouTubeDLBatchFile("%%(title)s"+"_1080"));
                            RunBatchFile(batchFilePath);
                        }
                        if (res1440)
                        {                        
                            videoRes="1440";
                            string batchFilePath = CreateBatchCMDSFile("youtubeDLBatch",CreateyouTubeDLBatchFile("%%(title)s"+"_1440"));
                            RunBatchFile(batchFilePath);
                        }
                        if (res4k)
                        {                        
                            videoRes="2160";
                            string batchFilePath = CreateBatchCMDSFile("youtubeDLBatch",CreateyouTubeDLBatchFile("%%(title)s"+"_4k"));
                            RunBatchFile(batchFilePath);
                        }
                        if (resCustom)
                        {                        
                            videoRes=videoResCustom.Replace("p","").Replace("P","");
                            string batchFilePath = CreateBatchCMDSFile("youtubeDLBatch",CreateyouTubeDLBatchFile("%%(title)s"+"_"+videoResCustom));
                            RunBatchFile(batchFilePath);
                        }
                        if (videoRes == "")
                        {
                            EditorUtility.DisplayDialog("WARNING!", "Video resolution not selected", "OK", "Cancel");
                            
                        }
                        
                    }
                
                    
                }
                EditorUtility.DisplayDialog("Complete", "Videos downlaoded to:" +downloadPath, "OK", "Cancel");
                            
                Process.Start(downloadPath); 
                           
                                    
            }

        }
        bool checkOptions()
        {
            bool allgood=true;
            // if (videoRes == "")
            // {
            //     EditorUtility.DisplayDialog("WARNING!", "Video resolution not selected", "OK", "Cancel");
            //     allgood=false;
            // }
            
            if (downloadPath == "")
            {
                EditorUtility.DisplayDialog("WARNING!", "Download Path not chosen", "OK", "Cancel");
                allgood=false;
            }

            return allgood;
        }
        public List<string> CreateyouTubeDLBatchFile(string savedVideoFileName)
        {
            List<string> commands = new List<string>();
            commands.Add("\""+youtubeDLPath+"\"" +" -o "+ downloadPath+"\\"+savedVideoFileName+ " " + youtubeURL + " -f " + "\"bestvideo[height<="+videoRes+"][ext=mp4]+bestaudio[ext=m4a]\" --user-agent \"\" %*");
            //commands.Add("PAUSE");
            return commands;
        }
        public string CreateBatchCMDSFile(string fileNameforBatch,List<string> input,List<string> input2=null,List<string> input3=null)
        {

            List<string> commandsList = input;
            if (input2 != null)
            {
                commandsList.AddRange(input2);
            }
            if (input3 != null)
            {
                commandsList.AddRange(input3);
            }

            string TempCMDBatchPath = downloadPath+"/"+fileNameforBatch+".bat";
            //Write some text to the test.txt file
            StreamWriter writer = new StreamWriter(TempCMDBatchPath, false);
            foreach (string cmd in commandsList)
            {
                //string cmdIN = cmd.Replace("/","\\");
                writer.WriteLine(cmd);
            }
            writer.WriteLine("TIMEOUT 1");
            
            writer.Close();
            return TempCMDBatchPath;
        }
        void RunBatchFile(string path)
        {
            FileInfo info = new FileInfo(path);
            ProcessStartInfo startInfo = new ProcessStartInfo(info.FullName);            
            startInfo.CreateNoWindow =false;

            var process = new Process();
            process.StartInfo=startInfo;
            process.Start();
            process.WaitForExit();

            File.Delete(path);
            //Debug.Log("Done");
        }
        
}
