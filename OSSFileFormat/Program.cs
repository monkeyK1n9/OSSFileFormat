using OSSFileLibrary;
using static OSSFileLibrary.FileStructure;

namespace OSSFileFormat
{
    /// <summary>
    /// This is just a console app to showcase the file structure and file handler. You can view the CVCreator desktop app for more inside of how we use the file format.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            // create a brand new file following our format
            KingFile writeKingFile = FileHandler.CreateFile("Toto", 18, 2,
                new Job[]
                {
                    new Job
                    {
                        JobName = "Larveur d'assiettes",
                        JobYear = 2012
                    },
                    new Job
                    {
                        JobName = "Dev",
                        JobYear = 2015
                    }
                });

            byte[] writeBytes = FileHandler.FileToByte(writeKingFile);

            File.WriteAllBytes("Test.monkeyking", writeBytes);

            Console.WriteLine(writeKingFile.Name);

            Console.WriteLine("#####################################");
            Console.WriteLine("Reading the file in your pc");

            byte[] readBytes = File.ReadAllBytes("Test.monkeyking");
            KingFile readKingFile = FileHandler.ByteToFile(readBytes);

            Console.WriteLine($"The name of the person on this CV is :{readKingFile.Name}");
            Console.WriteLine("Yeah it was a success");

        }
    }
}
