using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

using System.Threading;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;

namespace RandomPicSpam
{
    class Program
    {
        static void Main(string[] args)
        {
            // Path to Start
            string path;

            // Get Start Folder
            string[] pathStart = { "" };
            var folderPathList = new List<string>();
            var filePathList = new List<string>();

            // Counter
            int foundFolders = 0;
            int errorFolderDeniedCounter = 0;
            int foundFiles = 0;
            int errorFile = 0;
            int savedFiles = 0;
            int otherFilesType = 0;
            int imagesUnsuccessfullyCopied = 0;
            int imageSuccessfulCopied = 0;

            int txtCounterSaved = 0;
            int htmlCounterSaved = 0;
            int cssCounterSaved = 0;
            int sqlCounterSaved = 0;
            int docxCounterSaved = 0;
            int pptsCounterSaved = 0;
            int xlsxCounterSaved = 0;
            int pngCounterSaved = 0;
            int jpgCounterSaved = 0;
            int gifCounterSaved = 0;
            int exeCounterSaved = 0;
            int mp3CounterSaved = 0;
            int mp4CounterSaved = 0;

            int txtCounterNotSaved = 0;
            int htmlCounterNotSaved = 0;
            int cssCounterNotSaved = 0;
            int sqlCounterNotSaved = 0;
            int docxCounterNotSaved = 0;
            int pptsCounterNotSaved = 0;
            int xlsxCounterNotSaved = 0;
            int pngCounterNotSaved = 0;
            int jpgCounterNotSaved = 0;
            int gifCounterNotSaved = 0;
            int exeCounterNotSaved = 0;
            int mp3CounterNotSaved = 0;
            int mp4CounterNotSaved = 0;

            // Random
            string randomDirectory;
            int randomInt;

            // User Input
            int select = 0;
            char areYouSure;

            // Loop
            bool backToTop;

            var settings = Json();

            UserInput();

            Scanner();

            if (settings.ShowFolderPathInConsole)
                ConsoleFolderPathOutput();
            if (settings.ShowFilePathInConsole)
                ConsoleFilesPathOutput();

            if (settings.Allow_SaveFolderPath)
                FolderPathStreamer();
            if (settings.Allow_SaveFilePath)
                FilePathStreamer();
            if (settings.GetRandomFilePath)
                GetRandomFile();
            if (settings.Allow_Spammer)
                FileSpammer();

            ShowFinalResults();

            Console.ReadKey();

            //----------------------------Methods--------------------------------------
            // User
            void Logo()
            {
                Console.WriteLine();
                Console.WriteLine("   ███████╗ ██████╗ ██╗     ██████╗ ███████╗██████╗     ███████╗ ██████╗ █████╗ ███╗   ██╗███╗   ██╗███████╗██████╗ ");
                Console.WriteLine("   ██╔════╝██╔═══██╗██║     ██╔══██╗██╔════╝██╔══██╗    ██╔════╝██╔════╝██╔══██╗████╗  ██║████╗  ██║██╔════╝██╔══██╗");
                Console.WriteLine("   █████╗  ██║   ██║██║     ██║  ██║█████╗  ██████╔╝    ███████╗██║     ███████║██╔██╗ ██║██╔██╗ ██║█████╗  ██████╔╝");
                Console.WriteLine("   ██╔══╝  ██║   ██║██║     ██║  ██║██╔══╝  ██╔══██╗    ╚════██║██║     ██╔══██║██║╚██╗██║██║╚██╗██║██╔══╝  ██╔══██╗");
                Console.WriteLine("   ██║     ╚██████╔╝███████╗██████╔╝███████╗██║  ██║    ███████║╚██████╗██║  ██║██║ ╚████║██║ ╚████║███████╗██║  ██║");
                Console.WriteLine("   ╚═╝      ╚═════╝ ╚══════╝╚═════╝ ╚══════╝╚═╝  ╚═╝    ╚══════╝ ╚═════╝╚═╝  ╚═╝╚═╝  ╚═══╝╚═╝  ╚═══╝╚══════╝╚═╝  ╚═╝");
                Console.WriteLine();
            }

            void UserInput()
            {
                ShowEnabledFeatures();

                do
                {
                    backToTop = false;

                    Logo();

                    Console.WriteLine("[1] Specific folder");
                    Console.WriteLine("[2] System Directory");
                    try
                    {
                        select = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        backToTop = true;
                        Console.Clear();
                    }

                    // Specific Folder
                    if (select == 1)
                    {
                        do
                        {
                            backToTop = false;

                            Console.WriteLine("Enter Path: ");
                            path = Console.ReadLine();

                            try
                            {
                                pathStart = Directory.GetDirectories(path);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Error! \nPath Not Found");
                                Console.ReadKey();
                                Console.Clear();
                                backToTop = true;
                            }
                        } while (backToTop == true);
                    }

                    // System Directory
                    else if (select == 2)
                    {
                        if (settings.Allow_CopyFiles)
                            Console.WriteLine("Copy Files is Enabled - it can take a lot of space on your hard disk");
                        if (settings.Allow_Spammer)
                        {
                            Console.WriteLine("!!! Spammer is Enabled - it can pack your pc with random files in random folders");
                            Console.WriteLine("He will spam " + settings.HowManyFilesSpamming + "files randomly");
                        }

                        Console.WriteLine("--- Are You Sure? [Y] ---");
                        areYouSure = Console.ReadKey().KeyChar;

                        if (areYouSure == 'Y' || areYouSure == 'y')
                        {
                            path = Path.GetPathRoot(Environment.SystemDirectory);
                            Console.WriteLine("Path is now: \n" + path);
                            pathStart = Directory.GetDirectories(path);
                        }
                        else
                        {
                            Console.Clear();
                            backToTop = true;
                        }
                    }

                    // Wrong Input
                    else
                    {
                        Console.Clear();
                        backToTop = true;
                    }
                } while (backToTop == true);
            }

            // Scanner
            void Scanner()
            {
                // Get Start Folders to the List
                for (int i = 0; i < pathStart.Length; i++)
                {
                    folderPathList.Add(pathStart[i]);
                    foundFolders++;
                }

                // Get Folders in the Start Folders
                for (int i = 0; i < folderPathList.Count; i++)
                {
                    // Files
                    try
                    {
                        // Get Files
                        string[] fileArray = Directory.GetFiles(folderPathList[i]);

                        if (fileArray is not null)
                        {
                            // Add Files to List
                            for (int fileNUM = 0; fileNUM < fileArray.Length; fileNUM++)
                            {
                                filePathList.Add(fileArray[fileNUM]);
                                foundFiles++;

                                if (settings.Allow_CopyFiles)
                                    CopyFilesToApp(fileArray[fileNUM]);
                            }
                        }
                    }
                    catch (IOException io)
                    {
                        Console.WriteLine("File Error: \n" + io);
                        errorFile++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("File Error: \n" + ex);
                        errorFile++;
                    }

                    // Folder
                    try
                    {
                        // Get Folder Direction 
                        string[] folderInside = Directory.GetDirectories(folderPathList[i]);

                        // Add Folders to List
                        for (int folderNUM = 0; folderNUM < folderInside.Length; folderNUM++)
                        {
                            folderPathList.Add(folderInside[folderNUM]);
                            foundFolders++;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Folder Error: \n" + e);
                        errorFolderDeniedCounter++;
                    }
                }
            }

            void GetRandomFile()
            {
                Random rnd = new();
                Console.WriteLine("Random File: " + filePathList[rnd.Next(filePathList.Count)]);
            }

            // Path Writer
            void FolderPathStreamer()
            {
                PathFolderExistCheck();

                StreamWriter streamWriter = new(@"PathList\FolderPath.txt");

                // Write all Folder Direction on a text document
                foreach (var item in folderPathList)
                {
                    streamWriter.WriteLine(item);
                }

                streamWriter.Close();
            }

            void FilePathStreamer()
            {
                PathFolderExistCheck();

                StreamWriter streamWriter = new StreamWriter(@"PathList\FilePath.txt");

                // Write all Folder Direction on a text document
                foreach (var item in filePathList)
                {
                    streamWriter.WriteLine(item);
                }

                streamWriter.Close();
            }

            void PathFolderExistCheck()
            {
                bool exists = Directory.Exists(Directory.GetCurrentDirectory() + @"\PathList\");
                if (!exists)
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\PathList");
            }

            // Json
            DataSettings Json()
            {
                var settings = new DataSettings
                {
                    Allow_SaveFolderPath = true,
                    Allow_SaveFilePath = true,
                    Allow_CopyFiles = false,
                    Allow_Spammer = false,
                    HowManyFilesSpamming = 5,
                    GetRandomFilePath = false,

                    Allow_txt = true,
                    Allow_html = false,
                    Allow_css = false,
                    Allow_sql = false,
                    Allow_docx = false,
                    Allow_ppts = false,
                    Allow_xlsx = false,
                    Allow_png = false,
                    Allow_jpg = false,
                    Allow_gif = false,
                    Allow_exe = false,
                    Allow_mp3 = false,
                    Allow_mp4 = false,

                    ShowFolderPathInConsole = true,
                    ShowFilePathInConsole = true
                };

                // Json Serialize
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(settings, options);

                // Read Settings or Create if not exists
                jsonString = JsonStreamReader(jsonString);

                // Json Deserialize
                DataSettings jsonData = JsonSerializer.Deserialize<DataSettings>(jsonString);

                return jsonData;
            }

            string JsonStreamReader(string jsonString)
            {
                JsonFileExitCheck(jsonString);

                StreamReader streamReader = new StreamReader(@"Settings\Settings.json");

                jsonString = streamReader.ReadToEnd();

                streamReader.Close();

                return jsonString;
            }

            void JsonStreamWriter(string jsonString)
            {
                JsonDirectoryExitCheck();

                StreamWriter streamWriter = new StreamWriter(@"Settings\Settings.json");

                streamWriter.WriteLine(jsonString);

                streamWriter.Close();

                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine("  Settings.json file successfully created in the Folder: ");
                Console.WriteLine("  " + Directory.GetCurrentDirectory() + @"\Settings\Settings.json");
                Console.WriteLine("  - Can be edited for more freedom");
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine();
            }

            void JsonFileExitCheck(string jsonString)
            {
                if (!File.Exists(@"Settings\Settings.json"))
                    JsonStreamWriter(jsonString);
            }

            void JsonDirectoryExitCheck()
            {
                bool exists = Directory.Exists(Directory.GetCurrentDirectory() + @"\Settings\");
                if (!exists)
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Settings");
            }

            // File Copy
            void ContentDirectoryExistCheck()
            {
                bool exists = Directory.Exists(Directory.GetCurrentDirectory() + @"\Files\");
                if (!exists)
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Files");
            }

            void CopyFilesToApp(string filePath)
            {
                ContentDirectoryExistCheck();

                // Copy only these
                switch (Path.GetExtension(filePath))
                {
                    case ".txt":
                        if (settings.Allow_txt)
                        {
                            CopyFile(filePath);
                            txtCounterSaved++;
                        }
                        else
                            txtCounterNotSaved++;
                        break;
                    case ".html":
                        if (settings.Allow_html)
                        {
                            CopyFile(filePath);
                            htmlCounterSaved++;
                        }
                        else
                            htmlCounterNotSaved++;
                        break;
                    case ".css":
                        if (settings.Allow_css)
                        {
                            CopyFile(filePath);
                            cssCounterSaved++;
                        }
                        else
                            cssCounterNotSaved++;
                        break;
                    case ".sql":
                        if (settings.Allow_sql)
                        {
                            CopyFile(filePath);
                            sqlCounterSaved++;
                        }
                        else
                            sqlCounterNotSaved++;
                        break;
                    case ".docx":
                        if (settings.Allow_docx)
                        {
                            CopyFile(filePath);
                            docxCounterSaved++;
                        }
                        else
                            docxCounterNotSaved++;
                        break;
                    case ".ppts":
                        if (settings.Allow_ppts)
                        {
                            CopyFile(filePath);
                            pptsCounterSaved++;
                        }
                        else
                            pptsCounterNotSaved++;
                        break;
                    case ".xlsx":
                        if (settings.Allow_xlsx)
                        {
                            CopyFile(filePath);
                            xlsxCounterSaved++;
                        }
                        else
                            xlsxCounterNotSaved++;
                        break;
                    case ".png":
                        if (settings.Allow_png)
                        {
                            CopyFile(filePath);
                            pngCounterSaved++;
                        }
                        else
                            pngCounterNotSaved++;
                        break;
                    case ".jpg":
                        if (settings.Allow_jpg)
                        {
                            CopyFile(filePath);
                            jpgCounterSaved++;
                        }
                        else
                            jpgCounterNotSaved++;
                        break;
                    case ".gif":
                        if (settings.Allow_gif)
                        {
                            CopyFile(filePath);
                            gifCounterSaved++;
                        }
                        else
                            gifCounterNotSaved++;
                        break;
                    case ".exe":
                        if (settings.Allow_exe)
                        {
                            CopyFile(filePath);
                            exeCounterSaved++;
                        }
                        else
                            exeCounterNotSaved++;
                        break;
                    case ".mp3":
                        if (settings.Allow_mp3)
                        {
                            CopyFile(filePath);
                            mp3CounterSaved++;
                        }
                        else
                            mp3CounterNotSaved++;
                        break;
                    case ".mp4":
                        if (settings.Allow_mp4)
                        {
                            CopyFile(filePath);
                            mp4CounterSaved++;
                        }
                        else
                            mp4CounterNotSaved++;
                        break;
                    default:
                        otherFilesType++;
                        break;
                }
            }

            void CopyFile(string filePath)
            {
                File.Copy(filePath, Directory.GetCurrentDirectory() + @"\Files\" + Path.GetFileName(filePath));
                savedFiles++;
            }

            // Console Output
            void ConsoleFilesPathOutput()
            {
                Console.WriteLine("--------------------");
                Console.WriteLine("File list: ");
                Console.WriteLine("--------------------");
                // Output all Files Direction
                foreach (var item in filePathList)
                {
                    Console.WriteLine(item);
                }
            }

            void ConsoleFolderPathOutput()
            {
                Console.WriteLine("--------------------");
                Console.WriteLine("Folder list: ");
                Console.WriteLine("--------------------");
                // Output all Folder Direction
                foreach (var item in folderPathList)
                {
                    Console.WriteLine(item);
                }
            }

            void ShowFinalResults()
            {
                Console.WriteLine("----------------------------------");
                Console.WriteLine("Found " + foundFolders + " folders");
                Console.WriteLine("Denied Folder: " + errorFolderDeniedCounter);
                Console.WriteLine("----------------------------------");
                Console.WriteLine("Found " + foundFiles + " files");
                Console.WriteLine(errorFile + " files failed");
                Console.WriteLine("----------------------------------");
                if (settings.Allow_CopyFiles)
                {
                    if (settings.Allow_txt)
                        Console.WriteLine($"\tSaved {txtCounterSaved} .txt files. Failed {txtCounterNotSaved}");
                    if (settings.Allow_html)
                        Console.WriteLine($"\tSaved {htmlCounterSaved} .html files. Failed {htmlCounterNotSaved}");
                    if (settings.Allow_css)
                        Console.WriteLine($"\tSaved {cssCounterSaved} .css files. Failed {cssCounterNotSaved}");
                    if (settings.Allow_sql)
                        Console.WriteLine($"\tSaved {sqlCounterSaved} .sql files. Failed {sqlCounterNotSaved}");
                    if (settings.Allow_docx)
                        Console.WriteLine($"\tSaved {docxCounterSaved} .docx files. Failed {docxCounterNotSaved}");
                    if (settings.Allow_ppts)
                        Console.WriteLine($"\tSaved {pptsCounterSaved} .ppts files. Failed {pptsCounterNotSaved}");
                    if (settings.Allow_xlsx)
                        Console.WriteLine($"\tSaved {xlsxCounterSaved} .xlsx files. Failed {xlsxCounterNotSaved}");
                    if (settings.Allow_png)
                        Console.WriteLine($"\tSaved {pngCounterSaved} .png files. Failed {pngCounterNotSaved}");
                    if (settings.Allow_jpg)
                        Console.WriteLine($"\tSaved {jpgCounterSaved} .jpg files. Failed {jpgCounterNotSaved}");
                    if (settings.Allow_gif)
                        Console.WriteLine($"\tSaved {gifCounterSaved} .gif files. Failed {gifCounterNotSaved}");
                    if (settings.Allow_exe)
                        Console.WriteLine($"\tSaved {exeCounterSaved} .exe files. Failed {exeCounterNotSaved}");
                    if (settings.Allow_mp3)
                        Console.WriteLine($"\tSaved {mp3CounterSaved} .mp3 files. Failed {mp3CounterNotSaved}");
                    if (settings.Allow_mp4)
                        Console.WriteLine($"\tSaved {mp4CounterSaved} .mp4 files. Failed {mp4CounterNotSaved}");
                    Console.WriteLine();

                    Console.WriteLine("Files found: ");
                    Console.WriteLine($"\t{txtCounterNotSaved + txtCounterSaved} .txt files");
                    Console.WriteLine($"\t{htmlCounterNotSaved + htmlCounterSaved} .html files");
                    Console.WriteLine($"\t{cssCounterNotSaved + cssCounterSaved} .css files");
                    Console.WriteLine($"\t{sqlCounterNotSaved + sqlCounterSaved} .sql files");
                    Console.WriteLine($"\t{docxCounterNotSaved + docxCounterSaved} .docx files");
                    Console.WriteLine($"\t{pptsCounterNotSaved + pptsCounterSaved} .ppts files");
                    Console.WriteLine($"\t{xlsxCounterNotSaved + xlsxCounterSaved} .xlsx files");
                    Console.WriteLine($"\t{pngCounterNotSaved + pngCounterSaved} .png files");
                    Console.WriteLine($"\t{jpgCounterNotSaved + jpgCounterSaved} .jpg files");
                    Console.WriteLine($"\t{gifCounterNotSaved + gifCounterSaved} .gif files");
                    Console.WriteLine($"\t{exeCounterNotSaved + exeCounterSaved} .exe files");
                    Console.WriteLine($"\t{mp3CounterNotSaved + mp3CounterSaved} .mp3 files");
                    Console.WriteLine($"\t{mp4CounterNotSaved + mp4CounterSaved} .mp4 files");

                    Console.WriteLine();
                    Console.WriteLine($"\tOther Files Type: {otherFilesType}");

                    Console.WriteLine("----------------------------------");
                }
                if (settings.Allow_Spammer)
                {
                    Console.WriteLine("Images Successful Spammed " + imageSuccessfulCopied);
                    Console.WriteLine("Spammer failed : " + imagesUnsuccessfullyCopied + " files");
                    Console.WriteLine("----------------------------------");
                }
            }

            void ShowEnabledFeatures()
            {
                Console.WriteLine("Enabled Features: ");
                Console.WriteLine("------------------");
                if (settings.Allow_CopyFiles)
                {
                    Console.WriteLine("- Copy Files is activated");
                    Console.WriteLine("All Enabled Data Types: ");
                    if (settings.Allow_txt)
                        Console.WriteLine("\t.txt");
                    if (settings.Allow_html)
                        Console.WriteLine("\t.html");
                    if (settings.Allow_css)
                        Console.WriteLine("\t.css");
                    if (settings.Allow_sql)
                        Console.WriteLine("\t.sql");
                    if (settings.Allow_docx)
                        Console.WriteLine("\t.docx");
                    if (settings.Allow_ppts)
                        Console.WriteLine("\t.ppts");
                    if (settings.Allow_xlsx)
                        Console.WriteLine("\t.xlsx");
                    if (settings.Allow_png)
                        Console.WriteLine("\t.png");
                    if (settings.Allow_jpg)
                        Console.WriteLine("\t.jpg");
                    if (settings.Allow_gif)
                        Console.WriteLine("\t.gif");
                    if (settings.Allow_exe)
                        Console.WriteLine("\t.exe");
                    if (settings.Allow_mp3)
                        Console.WriteLine("\t.mp3");
                    if (settings.Allow_mp4)
                        Console.WriteLine("\t.mp4");
                }
                if (settings.GetRandomFilePath)
                    Console.WriteLine("- Get Random File");
                if (settings.Allow_SaveFolderPath)
                    Console.WriteLine("- Save Folder Path in .txt File");
                if (settings.Allow_SaveFilePath)
                    Console.WriteLine("- Save File Path in .txt File");
                if (settings.Allow_Spammer)
                {
                    Console.WriteLine("- ! Spammer is enabled !");
                    Console.WriteLine("He will spam " + settings.HowManyFilesSpamming + " randomly");
                }
                if (settings.ShowFolderPathInConsole)
                    Console.WriteLine("- Console Output for Folder Path");
                if (settings.ShowFilePathInConsole)
                    Console.WriteLine("- Console Output for File Path");

                Console.WriteLine(@"For changes close the app and go to Settings\Settings.json");
                Console.WriteLine(@"------------------------------------------------------------");
            }

            // Spammer
            void FileSpammer()
            {
                // Random
                Random rnd = new();
                var SpammerPathList = new List<string>();

                // Get Image Path
                string filePath = Directory.GetCurrentDirectory() + @"\FileSpammer\";

                string[] filesName;  // Get Files from Image Folder

                int randomImageNUM;
                string finalImagePath;
                string finalSpammerPath;

                Console.WriteLine("-------------------------");
                Console.WriteLine("Spammer files Path: ");
                for (int i = 0; i < settings.HowManyFilesSpamming; i++)
                {
                    // Get Random Path
                    randomInt = rnd.Next(folderPathList.Count);
                    randomDirectory = folderPathList[randomInt]; // Final Path

                    // Get Image and Copy to Path
                    try
                    {
                        filesName = Directory.GetFiles(filePath); // Get Files from Image Folder
                        randomImageNUM = rnd.Next(filesName.Length); // Get Random File Array
                        finalImagePath = new DirectoryInfo(filesName[randomImageNUM]).FullName; // Get final Image + FullPath
                        finalSpammerPath = randomDirectory + $"/" + Path.GetFileName(finalImagePath);

                        File.Copy(finalImagePath, finalSpammerPath); // Copy Random Image to Ramdom Location

                        Console.WriteLine($"Image {Path.GetFileName(finalSpammerPath)} Successful Copied to {finalSpammerPath}");
                        SpammerPathList.Add(finalSpammerPath);
                        imageSuccessfulCopied++;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: \n" + e);
                        imagesUnsuccessfullyCopied++;
                    }
                }

                SpammerPathListStreamer(SpammerPathList); // Write Path to .txt
            }

            void SpammerPathListStreamer(List<string> SpammerPathList)
            {
                PathFolderExistCheck();

                StreamWriter streamWriter = new(@"PathList\SpammerPath.txt");

                // Write all Spammer Files Path
                foreach (var item in SpammerPathList)
                {
                    streamWriter.WriteLine(item);
                }

                streamWriter.Close();
            }
        }
    }
}
