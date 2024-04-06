using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSSFileLibrary
{
    /// <summary>
    /// This class holds the structures of the file of an application that will create Curriculum Vitae (see CVCreator project in this same repo).
    /// Each user creating the CV provides a name, age, and Jobs
    /// </summary>
    public class FileStructure
    {
        public struct KingFile
        {
            public char[] MagicNumber; // magic number, just for convention to recognize the file
            public UInt16 Version; // can be helpful to cross check if the file version corresponds to the version the software can read
            public UInt32 FileSize; // still part of the metadata
            public string Name; // name of user
            public UInt16 Age; // age of user
            public UInt16 NumberOfJobs; // provided to help use structure the file
            public UInt32[] JobOffset; // usefull to compute the overall file size. It answers the question "how many bytes is this file? it is the last offset value"
            public Job[] Jobs; // jobs provided by user
        }

        public struct Job
        {
            public string JobName;
            public UInt32 JobYear;
        }
    }
}
